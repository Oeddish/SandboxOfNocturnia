using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Items.Accessories
{
    internal class NocturneBerry : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.accessory = true;
            Item.value = Item.sellPrice(0, 20);
            Item.rare = ItemRarityID.Expert;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.npcTypeNoAggro[49] = true;
            player.npcTypeNoAggro[51] = true;
            player.npcTypeNoAggro[60] = true;
            player.npcTypeNoAggro[93] = true;
            player.npcTypeNoAggro[137] = true;
            player.npcTypeNoAggro[150] = true;
            player.npcTypeNoAggro[151] = true;
            player.npcTypeNoAggro[634] = true;
            base.UpdateAccessory(player, hideVisual);
        }
    }
}
