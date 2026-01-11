using System;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

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

        public override void Load()
        {
            // Initialize prev scroll to current hardware value
            _prevScroll = Mouse.GetState().ScrollWheelValue;
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

            // Try to find a Main.* zoom target field via reflection to be resilient across versions
            // Common names: GameZoomTarget, gameZoomTarget, _gameZoomTarget, zoomTarget
            FieldInfo zoomField = null;
            string[] candidates = { "GameZoomTarget", "gameZoomTarget", "_gameZoomTarget", "zoomTarget", "GameZoom", "gameZoom" };
            foreach (var name in candidates)
            {
                zoomField = typeof(Main).GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (zoomField != null && zoomField.FieldType == typeof(float))
                    break;
                zoomField = null;
            }

            if (zoomField != null)
            {
                float current = (float)zoomField.GetValue(null);
                // Decide direction: positive wheel typically means up -> zoom in. 
                // Here we interpret positive notches as zooming in (decrease value). Adjust sign if your game uses opposite convention.
                float next = Math.Clamp(current - change, ZoomMin, ZoomMax);
                zoomField.SetValue(null, next);
            }
            else
            {
                // Fallback: try a known property if reflection fails.
                // If this runs in your tML version, replace with the exact public API field/property for zoom.
                // As a last resort, do nothing to avoid runtime errors.
            }

            // Update baseline
            _prevScroll = curScroll;
        }

        public override void Unload()
        {
            // Clear any static refs (none to clear here) and reset prev scroll
            _prevScroll = 0;
        }
    }
}