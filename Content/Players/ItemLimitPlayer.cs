using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    public class ItemLimitPlayer : ModPlayer
    {
        // Set by UpdateAccessory/UpdateInventory or by scanning equips
        public bool showItemLimitHud;

        public override void ResetEffects()
        {
            showItemLimitHud = false;
        }

        // Run after equips are processed — scan equip + vanity + social slots for the item.
        public override void UpdateEquips()
        {
            // If already true (inventory/accessory) skip the scan
            if (showItemLimitHud)
                return;

            for (int i = 0; i < Player.armor.Length; i++)
            {
                var it = Player.armor[i];
                if (it is not null && it.type == ModContent.ItemType<Content.Items.Accessories.ItemLimitIndicatorAccessory>())
                {
                    showItemLimitHud = true;
                    break;
                }
            }
        }
    }
}