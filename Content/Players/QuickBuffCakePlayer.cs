using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace FragmentsOfNocturnia.Content.Players
{
	internal class QuickBuffCakePlayer : ModPlayer
	{
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			// Only run on the local client
			if (Player.whoAmI != Main.myPlayer)
				return;

			// Detect Quick-Buff key press
			if (PlayerInput.Triggers.JustPressed.QuickBuff)
			{
				// Find a SliceOfCake in the player's inventory (includes hotbar)
				int slot = -1;
				for (int i = 0; i < Player.inventory.Length; i++)
				{
					Item it = Player.inventory[i];
					if (it != null && it.type == ItemID.SliceOfCake && it.stack > 0)
					{
						slot = i;
						break;
					}
				}

				if (slot == -1)
					return; // no cake available

				// If Alt is held, let vanilla/place behavior happen
				if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt))
					return;

				// Apply the buff without consuming the item
				Player.AddBuff(BuffID.SugarRush, 120 * 60);
				SoundEngine.PlaySound(SoundID.Item3, Player.position);
			}
		}
	}
}