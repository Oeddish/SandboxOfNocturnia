using FragmentsOfNocturnia.Content.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.System
{
    public class ItemLimitNotifierSystem : ModSystem
    {
        // Exposed so UI/accessories can read the current state.
        public static bool LimitReached { get; private set; }
        public static int ActiveItemCount { get; private set; }

        private bool wasFull;
        //private int lastItemCount = 0;

        public override void PostUpdateEverything()
        {
            // Don't attempt client UI messages from a dedicated server
            if (Main.netMode == NetmodeID.Server)
                return;

            int activeItemCount = 0;
            foreach (var item in Main.item)
            {
                if (item is null)
                    continue;

                if (item.active)
                    activeItemCount++;
            }

            ActiveItemCount = activeItemCount;
            int maxItemCount = Main.maxItems;
            // hard-coded test value
            // int maxItemCount = 40;

            // Spammy messages for debugging
            //if (lastItemCount != activeItemCount)
            //{
            //    Main.NewText("Item entity count: " + activeItemCount + maxItemCount);
            //    lastItemCount = ActiveItemCount;
            //}

            // LimitReached = activeItemCount >= maxItemCount;
            LimitReached = activeItemCount >= maxItemCount;
             
            if (activeItemCount >= maxItemCount)
            {
                if (!wasFull)
                {
                    Main.NewText($"WARNING: Item entity limit reached ({activeItemCount}/{maxItemCount}). Pick up or remove items to avoid lost drops.", 255, 50, 50);
                    wasFull = true;
                }
            }
            else
            {
                // Clear the flag so we notify again once the count drops and fills up later
                wasFull = false;
            }
        }
    }
}