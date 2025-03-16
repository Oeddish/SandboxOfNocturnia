using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;


namespace FragmentsOfNocturnia.Content.Projectiles.Ranged
{
    internal class AbyssalResonatorScope : ModProjectile
    {
        public static float BaseMaxCharge { get; internal set; }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 8; // The width of projectile hitbox
            Projectile.height = 8; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

            

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override void AI()
        {
            //base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(1f);

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -5; // These values match the values in SetDefaults
                DrawOriginOffsetY = -25;
                DrawOriginOffsetX = 0;
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = 10; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -25; // doesn't change
                DrawOriginOffsetX = 0; // Math works out that this is negative of the other value.
            }

            

            if (Projectile.timeLeft == 500)
            {
                //Main.NewText("/time stamp active", color: Color.Orange);
                Projectile.Kill();
            }
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            // Draws an afterimage trail. See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#afterimage-trail for more information.

            /*Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }*/

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            float speedX = Projectile.velocity.X * 100000;
            float speedY = Projectile.velocity.Y * 100000;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ModContent.ProjectileType<AbyssalResonatorWave>(), (int)(500), 0f, Projectile.owner, 0f, 0f);
            ModContent.GetInstance<Content.System.ScreenShaker>().TriggerScreenShake(30, 6f);
            for (int i = 0; i <= 50; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.TeleportationPotion, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
            }
        }
    }
}

