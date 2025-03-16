using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FragmentsOfNocturnia.Content.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using FragmentsOfNocturnia.Content.Players;
using rail;
using FragmentsOfNocturnia.Content.System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;

namespace FragmentsOfNocturnia.Content.Projectiles.Melee
{
    internal class SokidoRavenProjectile : Raven
    {
        //MOVEMENT VARIABLES
        float speed = 4f;
        float inertia = 16f;
        float idleRange = 8f;
        bool shouldAttackPlay = true;
        private NPC HomingTarget
        {
            get => Projectile.ai[0] == 0 ? null : Main.npc[(int)Projectile.ai[0] - 1];
            set
            {
                Projectile.ai[0] = value == null ? 0 : value.whoAmI + 1;
            }
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.ZephyrFish);
            Projectile.width = 54;
            Projectile.height = 50;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.usesLocalNPCImmunity = true;
            //Projectile.localNPCHitCooldown = 0;

            //Projectile.hide = true;

            state = 0;
            ravenCooldown = 400;
        }

        public override void AI()
        {
            if (ravenCooldown > 0) { ravenCooldown--; }

            Player player = Main.player[Projectile.owner];
            var modPlayer = Main.LocalPlayer.GetModPlayer<SokidoPlayer>();

            switch (state)
            {
                case 0:
                    // INACTIVE
                    shouldAttackPlay = true;
                    modPlayer.attacking = false;
                    Projectile.damage = 0;
                    Projectile.alpha = 255;
                    if (player.direction == 1) { Projectile.position = player.position + new Vector2(-100f, -100f); }
                    else { Projectile.position = player.position + new Vector2(100f, -100f); }
                    if (ravenCooldown > 0) { ravenCooldown--; }
                    else { state = 1;  }
                    break;
                case 1:
                    // FADING IN
                    if (Projectile.alpha > 0) { Projectile.alpha -= 10; }
                    else { state = 2; }
                    break; 
                case 2:
                    // FLYING
                    Movement(player);
                    if (modPlayer.attacking) { state = 4; }
                    break;
                case 3:
                    // SITTING
                    if (modPlayer.attacking) { state = 4; }
                    break;
                case 4:
                    // HOMING
                    Homing(player);
                    break;
                case 5:
                    // ATTACKING
                    Projectile.damage = 800;
                    Attack(player);
                    //state = 6;
                    break;
                case 6:
                    // FADING OUT
                    modPlayer.attacking = false;
                    Projectile.damage = 0;
                    if (Projectile.alpha < 255) { Projectile.alpha += 10; }
                    else { state = 0; ravenCooldown = 400; modPlayer.attacking = false; }
                    break;
                default:
                    Main.NewText("//State error", Color.Red);
                    break;
            }
            Lighting.AddLight(Projectile.Center, new Vector3(0.03f, 0.255f, 0.039f));
            
            Animate();
        }

        private void Movement(Player player)
        {
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 16f;


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
            if (Main.myPlayer == player.whoAmI && distanceToIdle < 10f) { state = 3;  }

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

            //This check will change the projectile velocity so the pet tries to stay within the idle range
            if (distanceToIdle > idleRange)
            {
                //idle = false;
                VectorToIdle.Normalize();
                VectorToIdle *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToIdle) / inertia;
            }
        }

        private void Homing(Player player)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<SokidoPlayer>();

            Vector2 idlePosition = modPlayer.mousePosition;
            idlePosition.Y -= 100f;
            idlePosition.X -= 100f * -modPlayer.playerDirection;


            //Calculate how far away the pet is from the base idle position
            Vector2 VectorToIdle = idlePosition - Projectile.Center;
            float distanceToIdle = VectorToIdle.Length();

            speed = 20f;
            if (Main.myPlayer == player.whoAmI && distanceToIdle < 10f) { state = 5; Projectile.velocity.X = 0f; Projectile.velocity.Y = 0f; }

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

            //This check will change the projectile velocity so the pet tries to stay within the idle range
            if (distanceToIdle > idleRange)
            {
                //idle = false;
                VectorToIdle.Normalize();
                VectorToIdle *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToIdle) / inertia;
            }
        }

        public void Attack(Player player)
        {
            var modPlayer = Main.LocalPlayer.GetModPlayer<SokidoPlayer>();

            Vector2 idlePosition = modPlayer.mousePosition;
            idlePosition.Y += 100f;
            idlePosition.X += 100f * -modPlayer.playerDirection;
            if (shouldAttackPlay) { SoundEngine.PlaySound(SoundID.Item116); shouldAttackPlay = false; }


            //Calculate how far away the pet is from the base idle position
            Vector2 VectorToIdle = idlePosition - Projectile.Center;
            float distanceToIdle = VectorToIdle.Length();

            speed = 30f;
            if (Main.myPlayer == player.whoAmI && distanceToIdle < 15f) { state = 6; Projectile.velocity.X *= 0.08f; Projectile.velocity.Y *= 0.08f; }

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

            //This check will change the projectile velocity so the pet tries to stay within the idle range
            if (distanceToIdle > idleRange)
            {
                //idle = false;
                VectorToIdle.Normalize();
                VectorToIdle *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + VectorToIdle) / inertia;
            }
        }

        private void Animate()
        {
            Player player = Main.player[Projectile.owner];
            var modPlayer = Main.LocalPlayer.GetModPlayer<SokidoPlayer>();
            switch (state)
            {
                case 1:
                    Projectile.frame = 1;
                    break;
                case 2:
                    if (++Projectile.frameCounter >= 20)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame++;
                        if (++Projectile.frame >= 5) { Projectile.frame = 1; }
                    }
                    break;
                case 3:
                    Projectile.frame = 0;
                    Vector2 sittingPosition = player.Center;
                    Projectile.spriteDirection = player.direction;
                    Projectile.velocity *= 0;
                    sittingPosition.Y -= 45f;
                    if (player.direction == 1) { sittingPosition.X -= 22f; }
                    if(player.direction == -1) { sittingPosition.X -= 32f; }
                    Projectile.position = sittingPosition;
                    break;
                case 4:
                    if (++Projectile.frameCounter >= 20)
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame++;
                        if (++Projectile.frame >= 5) { Projectile.frame = 1; }
                    }
                    break;
                case 5:
                    Vector2 particlePosition = Projectile.position;
                    Projectile.frame = 5;
                    Vector2 directionToCursor = modPlayer.mousePosition - Projectile.Center;
                    directionToCursor.Normalize();
                    Projectile.rotation = directionToCursor.ToRotation();
                    if(modPlayer.playerDirection == 1) { particlePosition += new Vector2(30f, 0f);  }
                    Dust.NewDust(particlePosition, Main.rand.Next(1, 5), Main.rand.Next(1, 5), DustID.GemEmerald, Projectile.velocity.X * -0.25f, Projectile.velocity.Y * -0.25f, 170, Color.LimeGreen, 0.5f);
                    break;
                case 6:
                    Projectile.frame = 4;
                    Projectile.rotation = 0;
                    break;
                default:
                    Projectile.frame = 0;
                    Projectile.rotation = 0;
                    break;
            }
            Projectile.frameCounter++;
        }

        /*public override void PostDraw(Color lightColor)
        {
            Texture2D glowTexture = ModContent.Request<Texture2D>("FragmentsOfNocturnia/Content/Projectiles/Melee/SokidoRavenGlow").Value;
            Main.EntitySpriteDraw(glowTexture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, glowTexture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
        }*/
    }
}
