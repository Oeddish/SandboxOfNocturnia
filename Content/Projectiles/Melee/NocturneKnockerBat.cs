using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace FragmentsOfNocturnia.Content.Projectiles.Melee
{
    public class NocturneKnockerBat : ModProjectile
    {
        public override void SetStaticDefaults()
        {

            Main.projFrames[Projectile.type] = 4; // Assuming 4 animation frames
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 36; // Mimics the AI of Terraria's bees
            Projectile.timeLeft = 300; // 5 seconds of flight
            Projectile.scale = 1f;
            Projectile.tileCollide = true; // Collides with tiles
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Gray;
        }

        public override void AI()
        {
            // Bee-like homing AI: Find the closest NPC and home in on it
            NPC closestNPC = null;
            float closestDistance = 500f; // Maximum homing range
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this) && !npc.friendly)
                {
                    float distanceToNPC = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distanceToNPC < closestDistance)
                    {
                        closestDistance = distanceToNPC;
                        closestNPC = npc;
                    }
                }
            }

            if (closestNPC != null)
            {
                Vector2 direction = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                float speed = 15f;
                Projectile.velocity = (Projectile.velocity * 20f + direction * speed) / 21f;
            }

            // Dust effect for visual flair
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[dust].velocity *= 0.5f;
                Main.dust[dust].scale = 1.2f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.
            for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
                dust.scale *= 0.9f;
            }
        }
    }
}
