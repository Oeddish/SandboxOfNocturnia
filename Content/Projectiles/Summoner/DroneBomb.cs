//height = 7
//width = 7
//frames = 3
using Microsoft.Build.Framework;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.NPC;

namespace FragmentsOfNocturnia.Content.Projectiles.Summoner
{
    public class DroneBomb : ModProjectile
    {
        public NPC target;
        private int rotateSpd = 2;
        private int framesToRotate = 0;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 7;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 3600;
            Projectile.light = 0.2f;
        }

        public override bool PreAI()
        {
            target = Main.npc[(int)Projectile.ai[0]];
            return true;
        }

        public override void AI()
        {
            //if (target != null)
            //    Main.NewText("TargetID : " + target);
            //else
            //    Main.NewText("No target");
            framesToRotate++;
            if (target != null && target.active && framesToRotate == rotateSpd)
            {
                float length = Projectile.velocity.Length();
                float targetAngle = Projectile.AngleTo(target.Center);
                Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(1)).ToRotationVector2() * length;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }

            if (framesToRotate >= rotateSpd)
            {
                framesToRotate = 0;
            }
            Animate();
        }

        private void Animate()
        {
            int frameSpeed = 5;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            const float radius = 48f * 2f; // explosion radius in pixels (adjust as needed)
            Vector2 center = Projectile.Center;

            // Choose explosion damage: use Projectile.damage if it's set sensibly,
            // otherwise fall back to vanilla grenade value (30).
            int explosionDamage = Projectile.damage > 1 ? Projectile.damage : 30;

            // Server-side: apply damage to NPCs only
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc == null || !npc.active || npc.friendly || npc.life <= 0)
                        continue;

                    // skip town NPCs and immortal or non-damageable NPCs
                    if (npc.townNPC || npc.lifeMax <= 5)
                        continue;

                    float dist = Vector2.Distance(npc.Center, center);
                    if (dist <= radius)
                    {
                        HitInfo hitInfo = new HitInfo();
                        hitInfo.Knockback = Projectile.knockBack;
                        hitInfo.Damage = explosionDamage;
                        hitInfo.Crit = false;

                        int direction = npc.position.X + (npc.width / 2) < center.X ? 1 : -1;
                        // Apply damage and knockback; this runs on server so it will sync to clients
                        npc.StrikeNPC(hitInfo);
                        npc.netUpdate = true;
                    }
                }
            }

            // Visuals & sound for everyone
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, center);
            const int dustCount = 30;
            for (int i = 0; i < dustCount; i++)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
                Dust.NewDustDirect(center - new Vector2(8, 8), 16, 16, DustID.Smoke, vel.X, vel.Y, 100, default, 2f).noGravity = true;
            }
        }
    }
}