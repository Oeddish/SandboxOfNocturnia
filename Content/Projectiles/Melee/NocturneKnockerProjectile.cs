using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Tile_Entities;
using FragmentsOfNocturnia.Content.Items.Weapons;
using FragmentsOfNocturnia.Content.Players;
using Terraria.Audio;

namespace FragmentsOfNocturnia.Content.Projectiles.Melee
{
    internal class NocturneKnockerProjectile : ModProjectile
    {
        private float returnSpeed = 10f;
        private bool orbiting = false;
        private bool isExtending = false;

        private float orbitRadius = 2f; // Current orbit radius
        private float orbitSpeed = 0.1f; // Speed of rotation

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 66;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 2;

            // Only hits enemies once, but doesn't give them iframes so sparks and explosions can hit.
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;

            Projectile.alpha = 0;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type != NPCID.TargetDummy)
            {
                Vector2 spawnPosition = target.Center;
                Vector2 velocity = new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
                int batDamage = 150;
                float beeKnockback = 2f;


                Projectile.NewProjectile(
                    Projectile.GetSource_OnHit(target),
                    spawnPosition,
                    velocity,
                    ModContent.ProjectileType<NocturneKnockerBat>(),
                    batDamage,
                    beeKnockback, // Bee was used as the template and I never changed it
                    Main.myPlayer); 

            }
            // Creates a flurry of particles
            int numDust = 10;
            for (int i = 0; i < numDust; ++i)
            {
                //int dustType = Main.rand.NextBool(3) ? 246 : 244;
                float scale = 0.8f + Main.rand.NextFloat(0.6f);
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
                Main.dust[idx].noGravity = true;
                Main.dust[idx].scale = scale * 2;
                Main.dust[idx].velocity *= 5f;
                Main.dust[idx].velocity += Projectile.velocity * 0.3f;
            }
            SoundEngine.PlaySound(SoundID.DD2_GoblinBomb);

        }

        // ai[0] = flail's current behavior state
        // 0 = flail is heading outwards
        // 1 = flail is coming back (Any other value also makes the flail return)
        // ai[1] = tick counter used to spawn projectiles and control transparency
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            var modPlayer = owner.GetModPlayer<NocturnePlayer>();

            // Destroy immediately if the owner is dead
            if (owner.dead)
            {
                //Main.NewText("/Projectile kill: 1", color: Color.Red);
                Projectile.Kill();
                return;
            }

            // Check for orbiting state
            bool retracting = !owner.controlUseItem || Main.mouseRight;
            //Main.NewText($"State: {Projectile.ai[0]}, Orbiting: {orbiting}, Extending: {isExtending}", color: Color.Yellow);
            // Prevent duplicates: Check if there's already an orbiting projectile
            if (orbiting && Projectile.ai[0] == 2f &&
    Main.projectile.Any(p => p.active && p.owner == Projectile.owner && p.type == Projectile.type &&
                             p.whoAmI != Projectile.whoAmI && p.ai[0] == 2f))
            {
                //Main.NewText("/Projectile kill: 2", color: Color.Red);
                Projectile.Kill();
                return;
            }

            // Orbiting behavior
            if (!retracting && Projectile.ai[0] != 1f && !isExtending)
            {
                orbiting = true;
                Projectile.ai[0] = 2f; // Set state for orbiting behavior

                // Expand or shrink the orbit radius
                if (orbitRadius < 100f && !Main.mouseRight)
                {
                    orbitRadius += 2f;
                    //Main.NewText("Orbit +", color: Color.Gray);
                }

                // Update orbit position
                Projectile.Center = owner.Center + new Vector2(
                    (float)Math.Cos(Projectile.ai[1]),
                    (float)Math.Sin(Projectile.ai[1])
                ) * orbitRadius;

                Projectile.ai[1] += orbitSpeed; // Increment rotation angle
                if (Projectile.ai[1] > MathHelper.TwoPi) // Keep angle within 0 to 2π
                    Projectile.ai[1] -= MathHelper.TwoPi;

                Projectile.velocity = Vector2.Zero; // Prevent movement
                Projectile.rotation += 0.2f;

                modPlayer.hasOrbitingProjectile = true;
            }
            else if (orbiting || Main.mouseRight && orbitRadius > 2f)
            {
                // Retract the orbit
                if (orbitRadius > 2f)
                {
                    //Main.NewText("Orbit --", color: Color.Gray);
                    orbitRadius -= 2f;

                    // Update position during retraction
                    Projectile.Center = owner.Center + new Vector2(
                        (float)Math.Cos(Projectile.ai[1]),
                        (float)Math.Sin(Projectile.ai[1])
                    ) * orbitRadius;

                    Projectile.ai[1] += orbitSpeed;
                    if (Projectile.ai[1] > MathHelper.TwoPi)
                        Projectile.ai[1] -= MathHelper.TwoPi;

                    Projectile.velocity = Vector2.Zero;

                    modPlayer.hasOrbitingProjectile = true;
                }
                else
                {
                    //Main.NewText("/Small orbit killed", color: Color.Orange);
                    // End orbit
                    orbitRadius = 2f;
                    orbiting = false;
                    modPlayer.hasOrbitingProjectile = false;
                    Projectile.Kill();
                }
            }
            else
            {

                // Default flail behavior
                Vector2 posDiff = owner.Center - Projectile.Center;
                isExtending = true;
                // Destroy if too far
                float dist = posDiff.Length();
                if (Projectile.ai[0] == 0f && dist > 800f)
                    Projectile.ai[0] = 1f;

                if (Projectile.ai[0] != 0f)
                {
                    if (dist > 1500f)
                    {
                        Main.NewText("/Shot too far", color: Color.Orange);
                        Projectile.Kill();
                        return;
                    }

                    Projectile.velocity = posDiff.SafeNormalize(Vector2.Zero) * returnSpeed;

                    if (posDiff.Length() < returnSpeed)
                    {
                        //Main.NewText("/Shooting finished", color: Color.Orange);
                        Projectile.Kill();
                        return;
                    }
                }
            }

            // Particle effects
            int numDust = 1;
            for (int i = 0; i < numDust; ++i)
            {
                float scale = 0.8f + Main.rand.NextFloat(0.6f);
                int idx = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
                Main.dust[idx].noGravity = true;
                Main.dust[idx].scale = scale;
                Main.dust[idx].velocity *= 2f;
                Main.dust[idx].velocity += Projectile.velocity * 0.3f;
            }
        }



        // PreDraw draws the flail's chain (underneath it, so it doesn't look weird)
        public override bool PreDraw(ref Color lightColor)
        {
            Player owner = Main.player[Projectile.owner];
            Vector2 mountedCenter = owner.MountedCenter;
            Color transparent = Color.Transparent;
            Texture2D chainTex = ModContent.Request<Texture2D>("FragmentsOfNocturnia/Content/Projectiles/Melee/NocturneKnockerChain").Value;
            Vector2 chainDrawPos = Projectile.Center;

            Rectangle? sourceRectangle = null;
            Vector2 origin = new Vector2(chainTex.Width * 0.5f, chainTex.Height * 0.5f);

            Vector2 posDiff = mountedCenter - chainDrawPos;
            float rot = (float)Math.Atan2(posDiff.Y, posDiff.X) - MathHelper.PiOver2;

            // If going right: Flip chain segments over so they face upwards
            if (posDiff.X < 0f)
                rot += MathHelper.Pi;

            // Just don't draw anything if there's NaNs to prevent crashes
            bool keepDrawing = true;
            if (chainDrawPos.HasNaNs() || posDiff.HasNaNs())
                keepDrawing = false;

            // Draw chain segments until you run out of space
            while (keepDrawing)
            {
                // Stop drawing when closer than 1 chain link away.
                if (posDiff.Length() < chainTex.Height + 1f)
                {
                    break;
                }

                Vector2 chainDirection = posDiff.SafeNormalize(Vector2.Zero);
                chainDrawPos += chainDirection * chainTex.Height;
                posDiff = mountedCenter - chainDrawPos;
                Color colorAtLoc = Lighting.GetColor((int)chainDrawPos.X / 16, (int)chainDrawPos.Y / 16);
                Main.spriteBatch.Draw(chainTex, chainDrawPos - Main.screenPosition, sourceRectangle, colorAtLoc, rot, origin, 1f, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}