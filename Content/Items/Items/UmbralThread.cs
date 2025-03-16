using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class UmbralThread : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28; // The item texture's width
            Item.height = 20; // The item texture's height

            Item.maxStack = Item.CommonMaxStack; // The item's max stack value
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Blue;
        }
    }
}
