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
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DrainingBand>(), 1);
            recipe.AddIngredient(ItemID.ShinyStone, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}