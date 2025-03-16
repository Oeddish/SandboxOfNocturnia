using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.System
{
    internal class ScreenShaker : ModSystem
    {
        private int screenShakeTime = 0;
        private float shakeIntensity = 0f;

        // Trigger the screen shake (e.g., during a boss attack)
        public void TriggerScreenShake(int duration, float intensity)
        {
            screenShakeTime = duration;
            shakeIntensity = intensity;
        }

        public override void ModifyScreenPosition()
        {
            if (screenShakeTime > 0)
            {
                screenShakeTime--;

                // Random displacement to simulate shaking
                Main.screenPosition += new Vector2(
                    Main.rand.NextFloat(-shakeIntensity, shakeIntensity),
                    Main.rand.NextFloat(-shakeIntensity, shakeIntensity)
                );

                // Gradually reduce intensity over time for a smooth finish
                shakeIntensity *= 0.9f;
            }
        }
    }
}
