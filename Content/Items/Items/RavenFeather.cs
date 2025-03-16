using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Items
{
    internal class RavenFeather : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26; // The item texture's width
            Item.height = 28; // The item texture's height

            Item.maxStack = Item.CommonMaxStack; // The item's max stack value
            Item.value = Item.buyPrice(gold: 1); // The value of the item in copper coins. Item.buyPrice & Item.sellPrice are helper methods that returns costs in copper coins based on platinum/gold/silver/copper arguments provided to it. 
        }
    }
}
