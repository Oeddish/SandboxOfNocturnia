using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    // Small ModPlayer to hold cooldowns for UI/messages (keyed per message id)
    public class MessageCooldownPlayer : ModPlayer
    {
        // map of key -> ticks remaining
        private Dictionary<string, int> cooldowns = new();

        // Configurable default color for mod messages
        public static Color DefaultMessageColor { get; set; } = new Color(200, 100, 255);

        // Decrement timers each tick
        public override void PostUpdate()
        {
            if (cooldowns.Count == 0)
                return;

            // Make a copy of keys to avoid modifying while iterating
            foreach (var key in cooldowns.Keys.ToList())
            {
                if (--cooldowns[key] <= 0)
                    cooldowns.Remove(key);
            }
        }

        // Returns true and sets the cooldown if the key is not on cooldown.
        // Only the local player will show messages (prevents duplicates in multiplayer).
        public bool TryTriggerMessage(string key, int cooldownTicks, string message)
            => TryTriggerMessage(key, cooldownTicks, message, DefaultMessageColor);

        // Overload allowing custom color
        public bool TryTriggerMessage(string key, int cooldownTicks, string message, Color color)
        {
            if (Player.whoAmI != Main.myPlayer)
                return false;

            if (cooldowns.TryGetValue(key, out int remaining) && remaining > 0)
                return false;

            cooldowns[key] = cooldownTicks;
            Main.NewText(message, color.R, color.G, color.B);
            return true;
        }

        // Overload to trigger without showing text (useful if caller wants custom handling)
        public bool TryUseCooldown(string key, int cooldownTicks)
        {
            if (Player.whoAmI != Main.myPlayer)
                return false;

            if (cooldowns.TryGetValue(key, out int remaining) && remaining > 0)
                return false;

            cooldowns[key] = cooldownTicks;
            return true;
        }

        // Check cooldown state without changing it
        public bool IsOnCooldown(string key)
        {
            return cooldowns.TryGetValue(key, out int remaining) && remaining > 0;
        }
    }
}