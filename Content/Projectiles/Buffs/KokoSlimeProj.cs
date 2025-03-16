using System;
using FragmentsOfNocturnia.Content.Buffs.Pets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Buffs
{
	public class KokoSlimeProj : ModProjectile
	{
		//Animation variables
		private static double startTime = 0;
		private static double nextTime = Main.frameRate * 60 * 1;   //framerate * 60s = animation plays every minute, * 10 = every 10 minutes of idle
		private int onFrame = 11;   //Frame that starts the idle animation
		private bool animate = false;   //Are we trying to do the idle animation right now?
		private int counter = 0;    //frame counter
		private bool reverse = false;   //doing the idle animation in reverse so its more noticable

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 15;
			Main.projPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.QueenSlimePet);

			AIType = ProjectileID.QueenSlimePet;
			//AIType = 0;

			Projectile.width = 70;
			Projectile.height = 43;
		}

		public override bool PreAI()
		{
			//if the player no longer has the buff kill the projectile
			Player player = Main.player[Projectile.owner];
			if (player.dead || !player.HasBuff(ModContent.BuffType<KokoSlimeBuff>()))
			{
				Projectile.timeLeft = 0;
			}

			if (animate)
			{
				return false;
			}


            return base.PreAI();
		}

        public override void PostAI()
        {

            // Prevent crown gore from spawning by deleting it immediately
            for (int i = 0; i < Main.maxGore; i++)
            {
                if (Main.gore[i].type == 1261)
                {
                    //Main.gore[i].active && 
                    Main.gore[i].active = false; // Deactivate the crown gore
                }
            }
            base.PostAI();
        }


        public override void PostDraw(Color lightColor)
		{
            //NOTE: I have it playing twice because I thought it was more noticable and looked nicer
            
            if (animate)
			{   //if its time to do the animation
				startTime = 0; //reset startime so it only counts when animation is not playing
				int animationSpeed = 10;

				counter++;  //change animation frame every 10 frames
				if (counter > animationSpeed)
				{   //once time has passed reset counter, increment animation frame

					Projectile.frame = onFrame;
					if (reverse)
					{   //If the animation played and now is going backwards decrement
						onFrame--;
					}
					else
					{       //if the animation is playing normally then increment frame counter
						onFrame++;
					}
					counter = 0;
				}

				if (onFrame <= 11 && reverse)
				{   //if the animation has played and went back to flying frame then the full animation has played.
					animate = false;
					reverse = false;
					onFrame = 11;
					counter = 0;
				}
			}
			if (onFrame >= Main.projFrames[Projectile.type] - 1)
			{
				reverse = true;
			}

			if (Projectile.velocity.Length() < 0.5f)
			{   //I am considering the slime idle if it has extreamly low velocity total
				startTime++;
				if (startTime >= nextTime)
				{
					startTime = 0;
					animate = true;
				}
			}
			else
			{
				animate = false;
				reverse = false;
				onFrame = 11;
				counter = 0;
			}
		}
    }
}