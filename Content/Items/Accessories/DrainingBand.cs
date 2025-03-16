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
using FragmentsOfNocturnia.Content.Items.Items;
using FragmentsOfNocturnia.Content.Buffs;
using FragmentsOfNocturnia.Content.Players;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Humanizer;
using Terraria.ModLoader.Core;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    internal class DrainingBand : ModItem
    {
        int lastManaRegened = 0;
        int beforeRegened = 0;
        int tickCounter = 0;
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 34;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        // This is where the effect is applied when the item is equipped
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //Main.NewText("Mana Regen: " + player.manaRegen, Color.Red);
            lastManaRegened = player.statMana - beforeRegened;
            tickCounter++;
            if (lastManaRegened < 0) { lastManaRegened = 0; }
            if (player.statMana >= lastManaRegened && tickCounter >= 2) { player.statMana -= lastManaRegened; tickCounter = 0; }
            beforeRegened = player.statMana;
            player.lifeRegen += 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaRegenerationBand, 1);
            recipe.AddIngredient(ModContent.ItemType<StrangeConcoction>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}