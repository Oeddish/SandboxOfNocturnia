using System;
using System.Reflection;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameInput;

namespace FragmentsOfNocturnia.Content.System
{
    // Client-side only: reads mouse wheel and adjusts the in-game camera zoom target.
    internal class MouseWheelZoomSystem : ModSystem
    {
        // Tweakable values
        private const float ZoomStep = 0.08f;      // change per wheel notch
        private const float ZoomMin = 0.5f;        // most zoomed-in (smaller = closer)
        private const float ZoomMax = 2.0f;        // most zoomed-out
        private const bool RequireModifier = true; // avoid stealing hotbar scroll by default
        private static readonly Keys ModifierKey = Keys.LeftControl; // hold to zoom

        private int _prevScroll;

        // Cached reflection targets
        private FieldInfo? _zoomField;
        private FieldInfo[] _playerInputScrollFields = Array.Empty<FieldInfo>();
        private FieldInfo[] _mainScrollFields = Array.Empty<FieldInfo>();

        public override void Load()
        {
            // Initialize prev scroll to current hardware value
            _prevScroll = Mouse.GetState().ScrollWheelValue;

            // Cache zoom field once
            string[] candidates = { "GameZoomTarget", "gameZoomTarget", "_gameZoomTarget", "zoomTarget", "GameZoom", "gameZoom" };
            foreach (var name in candidates)
            {
                var f = typeof(Main).GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (f != null && f.FieldType == typeof(float))
                {
                    _zoomField = f;
                    break;
                }
            }

            // Cache likely PlayerInput scroll/wheel integer fields (if present)
            _playerInputScrollFields = typeof(PlayerInput)
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => f.FieldType == typeof(int) && (f.Name.IndexOf("scroll", StringComparison.OrdinalIgnoreCase) >= 0 || f.Name.IndexOf("wheel", StringComparison.OrdinalIgnoreCase) >= 0))
                .ToArray();

            // Cache likely Main scroll integer fields (some tML versions store wheel state here)
            _mainScrollFields = typeof(Main)
                .GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => f.FieldType == typeof(int) && (f.Name.IndexOf("scroll", StringComparison.OrdinalIgnoreCase) >= 0 || f.Name.IndexOf("wheel", StringComparison.OrdinalIgnoreCase) >= 0))
                .ToArray();
        }

        public override void PostUpdateEverything()
        {
            // Only run on the client (camera is client-side)
            if (Main.dedServ)
                return;

            // Only affect the local player
            if (Main.myPlayer < 0 || Main.player.Length <= Main.myPlayer)
                return;

            // Read mouse wheel
            MouseState state = Mouse.GetState();
            int curScroll = state.ScrollWheelValue;
            int delta = curScroll - _prevScroll;
            if (delta == 0)
                return;

            // Optional: require a modifier so vanilla hotbar scroll still works normally
            if (RequireModifier && !Keyboard.GetState().IsKeyDown(ModifierKey))
            {
                _prevScroll = curScroll; // still update baseline so we don't get a jump later
                return;
            }

            // Each wheel notch is typically 120 units. Convert to number of notches
            float notches = delta / 120f;
            float change = notches * ZoomStep;

            if (_zoomField != null)
            {
                try
                {
                    float current = (float)_zoomField.GetValue(null)!;
                    // Positive notches interpreted as wheel-up => zoom in (decrease value).
                    float next = Math.Clamp(current - change, ZoomMin, ZoomMax);
                    _zoomField.SetValue(null, next);
                }
                catch
                {
                    // If something goes wrong with reflection at runtime, silently ignore and bail out.
                    _zoomField = null;
                }
            }

            // Prevent vanilla hotbar/next/previous from seeing the same wheel delta
            ConsumeVanillaWheel(curScroll);

            // Update baseline
            _prevScroll = curScroll;
        }

        private void ConsumeVanillaWheel(int curScroll)
        {
            foreach (var f in _playerInputScrollFields)
            {
                try { f.SetValue(null, curScroll); }
                catch { /* ignore individual failures */ }
            }

            foreach (var f in _mainScrollFields)
            {
                try { f.SetValue(null, curScroll); }
                catch { /* ignore individual failures */ }
            }
        }

        public override void Unload()
        {
            // Clear cached reflection info
            _zoomField = null;
            _playerInputScrollFields = Array.Empty<FieldInfo>();
            _mainScrollFields = Array.Empty<FieldInfo>();
            _prevScroll = 0;
        }
    }
}