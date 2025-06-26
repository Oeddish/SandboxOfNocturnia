//height = 7
//width = 7
//frames = 3
using Microsoft.Build.Framework;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

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
            //SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            int grenade = Projectile.NewProjectile(
                Projectile.GetSource_Death(),
                Projectile.Center,
                Vector2.Zero,
                ProjectileID.Grenade, // or Explosion type from vanilla
                30,
                Projectile.knockBack,
                Projectile.owner
            );
            Main.projectile[grenade].timeLeft = 0;
            Main.projectile[grenade].netUpdate = true;
        }
    }
}