using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FragmentsOfNocturnia.Content.Buffs;
using System.Media;
using Terraria.Audio;



namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    internal class CrescentBand : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Expert;
        }


        // This is where the effect is applied when the item is equipped
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!player.HasBuff<CrescentCharging>() && !player.HasBuff<CrescentRegeneration>() && player.statLife < player.statLifeMax2 / 2) { player.AddBuff(ModContent.BuffType<CrescentRegeneration>(), 120, false); SoundEngine.PlaySound(SoundID.Shatter); }
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ItemID.ManaRegenerationBand || slot != i && player.armor[i].type == ItemID.BandofRegeneration || slot != i && player.armor[i].type == ModContent.ItemType<DrainingBand>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
            public override void AddRecipes()
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<DrainingBand>(), 1);
                recipe.AddIngredient(ItemID.ShinyStone, 1);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.Register();
            }
        public class ManaRegenerationBand : GlobalItem
        {
            public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
            {
                if (item.type == ItemID.ManaRegenerationBand)
                {
                    if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
                    {
                        int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                        for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                        {
                            // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                            if (slot != i && player.armor[i].type == ModContent.ItemType<CrescentBand>() || slot != i && player.armor[i].type == ModContent.ItemType<DrainingBand>())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
        public class BandofRegeneration : GlobalItem
        {
            public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
            {
                if (item.type == ItemID.BandofRegeneration)
                {
                    if (slot < 10) // This allows the accessory to equip in Vanity slots with no reservations.
                    {
                        int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                        for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                        {
                            // We need "slot != i" because we don't care what is currently in the slot we will be replacing.
                            if (slot != i && player.armor[i].type == ModContent.ItemType<CrescentBand>() || slot != i && player.armor[i].type == ModContent.ItemType<DrainingBand>())
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}