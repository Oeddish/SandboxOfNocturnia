using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Buffs.Pets;

namespace FragmentsOfNocturnia.Content.Projectiles.Buffs
{

    public class PottedCompanionProjectile : ModProjectile
    {
        //MOVEMENT VARIABLES
        float speed = 4f;
        float inertia = 16f;
        float idleRange = 8f;
        bool idle = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.LightPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            Projectile.width = 30;
            Projectile.height = 50;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.Opacity = 0.85f;
        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];

            // If the player is no longer active (online) - deactivate (remove) the projectile.
            if (!player.active)
            {
                Projectile.active = false;
                return;
            }

            // Keep the projectile disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<PottedCompanion>()))
            {
                Projectile.timeLeft = 2;
            }

            //Function for the pet AI based on player position
            Movement(player);

            Animate();

            Lighting.AddLight(Projectile.Center, new Vector3(1.2f, 0.65882353f, 1.38039216f));

        }

        private void Movement(Player player)
        {
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 36f;
            idlePosition.X += 36f * -player.direction;

            //Calculate how far away the pet is from the base idle position
            Vector2 VectorToIdle = idlePosition - Projectile.Center;
            float distanceToIdle = VectorToIdle.Length();

            //If the pet is too far away then it will teleport to the base idle position
            if (Main.myPlayer == player.whoAmI && distanceToIdle > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity = Vector2.Zero;
                Projectile.netUpdate = true;
            }
            if (Main.myPlayer == player.whoAmI && distanceToIdle > 200f)
            {
                speed = 12f;
            }
            else { speed = 4f; }

            //Flips the pet sprite depending on direction it is moving
            if (Projectile.velocity.X <= 0.1f && Projectile.velocity.X >= -0.1f)
            {
                Projectile.spriteDirection = player.direction;
            }
            else if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.spriteDirection = 1;
            }

            //If the pet is close to idle position then it will move slowly, so once it is slow enough we can consider it idle.
            if (Projectile.velocity.Length() < idleRange)
            {
                idle = true;
            }
            else
            {
                idle = false;
            }

            //This check will change the projectile velocity so the pet tries to stay within the idle range
            if (distanceToIdle > idleRange)
            {
                idle = false;
                VectorToIdle.Normalize();
                VectorToIdle *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToIdle) / inertia;
            }
            //If the minion is close to the base idle position it will slow down
            else if (!idle && distanceToIdle < idleRange)
            {
                Projectile.velocity *= 0.75f;
            }


            //If the pet is idle we dont need to make sure it slows movement in a smooth way
            if (idle)
            {
                Projectile.velocity.X *= 0.9f;
            }
            if (idle && Projectile.velocity.Y > .5)
            {
                Projectile.velocity.Y *= 0.9f;
            }
            //If the pet is idle it will slowely bob up and down
            if (idle && VectorToIdle.Y < idleRange / 10)
            {
                Projectile.velocity.Y -= .01f;
            }
            else if (idle)
            {
                Projectile.velocity.Y += .01f;
            }
        }

        private void Animate()
        {
            //Make the pet rotate slightly while moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            int animationSpeed = 7;

            // Animate all frames from top to bottom, going back to the first
            Projectile.frameCounter++;
            if (Projectile.frameCounter > animationSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }

    }
}