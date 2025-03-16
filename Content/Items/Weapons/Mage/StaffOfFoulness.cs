using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Projectiles.Mage;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace FragmentsOfNocturnia.Content.Items.Weapons.Mage
{
    internal class StaffOfFoulness : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.damage = 22;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.knockBack = 1.5f;
            Item.mana = 15;
            Item.shootSpeed = 10f;
            
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.shoot = ModContent.ProjectileType<PinkEchoBoltProjectile>();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 3, silver: 40);
            
            Item.noMelee = true;
            Item.autoReuse = true;
        }
        public override Vector2? HoldoutOffset()
        {
            Vector2? offset = new Vector2(-10f, 0f);
            return offset;
        }
    }
}
