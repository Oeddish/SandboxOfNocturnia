using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    public class StrangeConcoction : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 3;

            ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
                new Color(220, 100, 200),
                new Color(210, 70, 190),
                new Color(135, 70, 210)
                
            };
        }

        public override void SetDefaults()
        {
            //TooltipLine tt_strange = new TooltipLine(Mod, "ConjunctionText", "Advised not to drink");
            Item.width = 20;
            Item.height = 28;
            //Item.ToolTip = tt_strange;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            
            Item.useAnimation = 15;
            Item.useTime = 30;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.buffType = BuffID.Confused; // Specify an existing buff to be applied when used.
            Item.buffTime = 540; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
            
            
            Item.healMana = 2;
            Item.healLife = -10;
            Item.potion = true;
            
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HealingPotion)
                .AddIngredient(ItemID.ManaPotion)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}