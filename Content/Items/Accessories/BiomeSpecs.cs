using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    internal class BiomeSpecs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 0, 22);
            Item.rare = ItemRarityID.Pink;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.BiomeSight, 5, true);
        }
    }
}
