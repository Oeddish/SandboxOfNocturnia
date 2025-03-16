using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using FragmentsOfNocturnia.Content.Projectiles.Melee;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class StaffOfTheGluttonHeadProjectile : ModProjectile
    {
        private int ttl = 200;
        private bool firstFrame = true;
        private bool firstHit = true;

        // Store the target NPC using Projectile.ai[0]
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
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }
        
        public override void SetDefaults()
        {
            Projectile.width = 50; // The width of projectile hitbox
            Projectile.height = 50; // The height of projectile hitbox

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile

            Projectile.alpha = 255; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
        }

        public override void AI()
        {
            if (firstFrame)
            {
                // Get the cursor position in the world
                Vector2 cursorPosition = Main.MouseWorld;

                // Calculate the direction from the projectile to the cursor
                Vector2 directionToCursor = cursorPosition - Projectile.Center;

                // Normalize the direction vector to get only the direction
                directionToCursor.Normalize();

                // Calculate the rotation from the direction vector
                Projectile.rotation = directionToCursor.ToRotation();

                // Optional: Set the velocity to match the direction
                Projectile.velocity = directionToCursor * Projectile.velocity.Length();

                for (int i = 0; i < 70; i++)
                {
                    Dust.NewDust(Projectile.position, Main.rand.Next(1, 5), Main.rand.Next(1, 5), DustID.Cloud, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 170, Color.Purple, 0.7f);
                }
            }
            firstFrame = false;
            base.AI();
            ttl--;
            if (ttl < 0) { Projectile.Kill(); }
            if (Projectile.alpha > 10) { Projectile.alpha = Projectile.alpha - 20; }

            Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 1.5f);
            if (ttl < 180) { Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Clentaminator_Purple, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f); }

            Dust.NewDust(Main.MouseWorld, Main.rand.Next(1, 5), Main.rand.Next(1, 5), DustID.Cloud, 0, 0, 170, Color.Purple, 0.7f);

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                // Or more compactly Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }


            //HOMING AI

            float maxDetectRadius = 400f;

            // A short delay to homing behavior after being fired
            if (DelayTimer < 10)
            {
                DelayTimer += 1;
                return;
            }

            // First, we find a homing target if we don't have one
            if (HomingTarget == null)
            {
                HomingTarget = FindClosestNPC(maxDetectRadius);
            }

            // If we have a homing target, make sure it is still valid. If the NPC dies or moves away, we'll want to find a new target
            if (HomingTarget != null && !IsValidTarget(HomingTarget))
            {
                HomingTarget = null;
            }

            // If we don't have a target, don't adjust trajectory
            if (HomingTarget == null || !firstHit || ttl < 100)
                return;


            // If found, we rotate the projectile velocity in the direction of the target.
            // We only rotate by 3 degrees an update to give it a smooth trajectory. Increase the rotation speed here to make tighter turns
            float length = Projectile.velocity.Length();
            float targetAngle = Projectile.AngleTo(HomingTarget.Center);
            Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(2)).ToRotationVector2() * length;
            Projectile.rotation = Projectile.velocity.ToRotation();
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

        public override void PostDraw(Color lightColor)
        {
            Texture2D glowTexture = ModContent.Request<Texture2D>("FragmentsOfNocturnia/Content/Projectiles/Mage/StaffOfTheGluttonHeadProjectileGlow").Value;
            Main.EntitySpriteDraw(glowTexture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, glowTexture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            if (firstHit)
            {
                Vector2 spawnPosition = target.Center + new Vector2(Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5));
                Vector2 velocity = new Vector2(0, 0);
                int biteDamage = Projectile.damage + 90;
                float biteKnockback = 2f;

                Projectile.NewProjectile(
                        Projectile.GetSource_OnHit(target),
                        spawnPosition,
                        velocity,
                        ModContent.ProjectileType<StaffOfTheGluttonBiteProjectile>(),
                        biteDamage,
                        biteKnockback,
                        Main.myPlayer);
                firstHit = false;
            }
        }
    }
}
