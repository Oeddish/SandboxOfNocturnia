using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.Buffs.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FragmentsOfNocturnia.Content.Projectiles.Buffs
{
    internal class WraithedProjectile : ModProjectile
    {
        //MOVEMENT VARIABLES
        float speed = 4f;
        float inertia = 16f;
        int state = 0;
        // 0 = spawning
        // 1 = chasing
        // 2 = dying

        private NPC HomingTarget
        {
            get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
            set
            {
                Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1;
            }
        }
        public ref float DelayTimer => ref Projectile.ai[1];
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            Main.projPet[Projectile.type] = false;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 30;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft = 15 * 60;
            Projectile.friendly = false;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.damage = 200;

            Projectile.alpha = 255;
        }

        public override void AI()
        {
            switch (state)
            {
                // SPAWNING
                case 0:
                    Projectile.alpha -= 7;
                    if(Projectile.alpha <= 0) { state = 1; Projectile.friendly = true; }
                    break;

                // CHASING
                case 1:
                    // See Glutton staff projectile for explenation of homing ai
                    float maxDetectRadius = 400f;

                    if (DelayTimer < 10)
                    {
                        DelayTimer += 1;
                        return;
                    }

                    if (HomingTarget == null)
                    {
                        HomingTarget = FindClosestNPC(maxDetectRadius);
                    }

                    if (HomingTarget != null && !IsValidTarget(HomingTarget))
                    {
                        HomingTarget = null;
                    }

                    if (HomingTarget != null)
                    {
                        Movement(HomingTarget);
                    }


                    Animate();
                    break;

                // DYING
                case 2:
                    Projectile.alpha += 20;
                    Projectile.velocity *= 0.1f;
                    Dust.NewDust(Projectile.position, Main.rand.Next(1, 3), Main.rand.Next(1, 3), DustID.Wraith, Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f,2f), Projectile.alpha, Color.DarkGreen, 0.7f);
                    if (Projectile.alpha >= 255) { Projectile.Kill(); }
                    break;

                default:
                    Main.NewText("ERROR - WraithedProjectile: state value unknown.", Color.Red);
                    break;
            }
            return;
        }

        private void Movement(NPC target)
        {
            Vector2 VectorToTarget = target.Center - Projectile.Center;
            float distanceToTarget = VectorToTarget.Length();

            if (distanceToTarget > 50f)
            {
                speed = 8f;
            }
            else
            {
                speed = 14f;
            }

            //Flips the pet sprite depending on direction it is moving
            if (Projectile.velocity.X <= 0.1f && Projectile.velocity.X >= -0.1f)
            {
                Projectile.spriteDirection = target.direction;
            }
            else if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.spriteDirection = 1;
            }

            VectorToTarget.Normalize();
            VectorToTarget *= speed;

            Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToTarget) / inertia;
        }

        private void Animate()
        {
            //Make the pet rotate slightly while moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            int animationSpeed = 12;

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

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all NPCs
            foreach (var target in Main.ActiveNPCs)
            {
                // Check if NPC able to be targeted. 
                if (IsValidTarget(target))
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        public bool IsValidTarget(NPC target)
        {
            // This method checks that the NPC is:
            // 1. active (alive)
            // 2. chaseable (e.g. not a cultist archer)
            // 3. max life bigger than 5 (e.g. not a critter)
            // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            // 5. hostile (!friendly)
            // 6. not immortal (e.g. not a target dummy)
            // 7. doesn't have solid tiles blocking a line of sight between the projectile and NPC
            return target.CanBeChasedBy() && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.friendly = false;
            state = 2;
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
