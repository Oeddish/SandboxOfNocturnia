using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace FragmentsOfNocturnia.Content.Projectiles.Ranged
{
    internal class AbyssalResonatorWave : ModProjectile
    {
        // Store the original screen position
        Vector2 originalScreenPosition = Main.screenPosition;

        private int endFizzle = 50;
        private bool fizzling = false;
        public override void SetDefaults()
        {
            Projectile.width = 70; // The width of projectile hitbox
            Projectile.height = 116; // The height of projectile hitbox
            Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.hostile = false; // Can the projectile deal damage to the player?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.timeLeft = 250; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.alpha = 0; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.light = 0.5f; // How much light emit around the projectile
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame

            

            AIType = ProjectileID.Bullet; // Act exactly like default Bullet

            // Apply a screen shake
            Main.screenPosition += new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
        }

        public override void AI()
        {
            base.AI();

            Projectile.timeLeft--;
            if(Projectile.timeLeft <= 0 || endFizzle <= 0)   { Projectile.Kill(); }
            if(fizzling) { endFizzle--; }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(1f);

            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);

            if (Projectile.spriteDirection == 1) // facing right
            {
                DrawOffsetX = -5; // These values match the values in SetDefaults
                DrawOriginOffsetY = -50;
                DrawOriginOffsetX = 0;
            }
            else
            {
                // Facing left.
                // You can figure these values out if you flip the sprite in your drawing program.
                DrawOffsetX = 10; // 0 since now the top left corner of the hitbox is on the far left pixel.
                DrawOriginOffsetY = -50; // doesn't change
                DrawOriginOffsetX = 0; // Math works out that this is negative of the other value.
            }

            //if (!fizzling) { Projectile.velocity = Projectile.velocity * 1.025f; }
            //else { Projectile.velocity = Projectile.velocity * 0.95f; }
            Projectile.velocity = Projectile.velocity * 1.025f;

            if (Projectile.timeLeft == 230)
            {
                // Reset screen position after the shake effect
                Main.screenPosition = originalScreenPosition;
            }
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Main.rand.Next(68, 70), Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            //Projectile.friendly = false;
            fizzling = true;
        }
    }
}
