using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.IO;
using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Items.Items;
using Terraria.Enums;
using FragmentsOfNocturnia.Content.Items.Accessories;

namespace FragmentsOfNocturnia.Content.System
{
    internal class ShopModifier : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            int type = shop.NpcType;
            int goldCost = NPC.downedMoonlord ? 16 : Main.hardMode ? 8 : 4;

            bool happy = Main.LocalPlayer.currentShoppingSettings.PriceAdjustment <= 0.9;

            if (type == NPCID.Clothier)
            {
                if (!Main.dayTime) { shop.Add(ModContent.ItemType<UmbralThread>()); }
            }

            if (type == NPCID.Dryad)
            {
                if (NPC.downedGoblins && Main.expertMode && !Main.dayTime) { shop.Add(ModContent.ItemType<NocturneBerry>()); }

                // Add worms and basic bait to Dryad's shop at all times
                // My Dryad is fishy!
                shop.Add(ItemID.Worm);
                shop.Add(ItemID.ApprenticeBait);
            }
        }
    }
}
