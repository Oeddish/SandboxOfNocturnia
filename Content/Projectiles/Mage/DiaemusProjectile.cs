using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Mono.Cecil;
using static System.Net.Mime.MediaTypeNames;
using FragmentsOfNocturnia.Content.Projectiles.Melee;
using Terraria.GameContent.ItemDropRules;
using System.Diagnostics;

namespace FragmentsOfNocturnia.Content.Projectiles.Mage
{
    internal class DiaemusProjectile : ModProjectile
    {
        private int ttl = 140;
        private bool canHit = true;
        public override void SetDefaults()
        {
            Projectile.width = 20; // The width of projectile hitbox
            Projectile.height = 20; // The height of projectile hitbox

            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = true; // Can the projectile collide with tiles?
            Projectile.penetrate = 1; // Look at comments ExamplePiercingProjectile

            Projectile.friendly = true;

            Projectile.alpha = 0; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
        }

        public override void AI()
        {
            ttl--;
            if(ttl <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                float time = Main.GlobalTimeWrappedHourly * 4f;
                float scaleTracker = (float)Math.Sin(time);

                if (scaleTracker < 0) { scaleTracker *= -1; } // Always keep the value positive
                if (scaleTracker < 0.5) { scaleTracker = 1 - scaleTracker ; } // Gives it that thump

                // Sound & Spray attack
                if (scaleTracker >= 0.99)
                {
                    //Main.NewText("thump", Color.Red);
                    SoundEngine.PlaySound(SoundID.Item8);

                    Vector2 thing = Main.rand.NextVector2CircularEdge(5f, 5f);
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2,
                        thing,
                        ModContent.ProjectileType<DiaemusSpikeProjectile>(),
                        Projectile.damage / 2,
                        Projectile.knockBack,
                        Main.myPlayer);
                    for (int i = 0; i < 3; i++)
                    {
                    Dust.NewDust(
                        Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2,
                        1, 1,
                        DustID.Blood,
                        Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5),
                        0,
                        Color.Red,
                        1f);
                    }


                }

                //Main.NewText(scaleTracker);
                Projectile.scale = scaleTracker;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for(int i = 0; i < 15; i++)
            {
                Vector2 thing = Main.rand.NextVector2CircularEdge(5f, 5f);
                    Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2,
                    thing,
                    ModContent.ProjectileType<DiaemusSpikeProjectile>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Main.myPlayer);
            for (int j = 0; j < 30; j++)
                {
                    Dust.NewDust(
                        Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2,
                        1, 1,
                        DustID.Blood,
                        Main.rand.NextFloat(-5, 5), Main.rand.NextFloat(-5, 5),
                        0,
                        Color.Red,
                        1f);
                }
            }
            base.OnKill(timeLeft);
        }
    }
}
