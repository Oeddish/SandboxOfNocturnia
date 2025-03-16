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
    internal class StaffOfCruelty : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 62;
            Item.damage = 27;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.knockBack = 2f;
            Item.mana = 15;
            Item.shootSpeed = 10f;
            Item.crit = 4;
            
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item34;
            Item.shoot = ModContent.ProjectileType<PinkEchoBoltProjectile>();
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(gold: 4, silver: 12);
            
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
