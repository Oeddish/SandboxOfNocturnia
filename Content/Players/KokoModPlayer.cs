using FragmentsOfNocturnia.Content.Buffs.Consumables;
using FragmentsOfNocturnia.Content.Items.Weapons.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players;

public class KokoModPlayer : ModPlayer
{
	public bool slimePet;
	public bool Cartridge_Buff;
	public bool Absorption_Buff;
	public int kikiPairID = 1;

	public int gunCritBonus = 0;

	public int getNextKikiID()
	{
		return kikiPairID++;
	}

	public override void Initialize()
	{
		kikiPairID = 1;
	}

	public override void OnEnterWorld()
	{
		kikiPairID = 1;
	}

	public override void ResetEffects()
	{
		Cartridge_Buff = false;
		Absorption_Buff = false;
		gunCritBonus = 0;
	}

	public override void PostUpdateBuffs()
	{
		if (Player.HasBuff(ModContent.BuffType<Cartridge_Potion_Buff>()))
		{
			Cartridge_Buff = true;
			if (Player.HasBuff(BuffID.AmmoReservation))
			{
				Player.ClearBuff(BuffID.AmmoReservation);
			}
		}

		if (Player.HasBuff(ModContent.BuffType<Absorption_Potion_Buff>()))
		{
			Absorption_Buff = true;
			if (Player.HasBuff(BuffID.Heartreach))
			{
				Player.ClearBuff(BuffID.Heartreach);
			}
		}
	}

	public override bool CanConsumeAmmo(Item weapon, Item ammo)
	{
		if (Cartridge_Buff && Main.rand.NextFloat() < 0.30f) //30% chance to not consume ammo
		{
			return false;
		}
		return true;
	}
	
	public override void ModifyWeaponCrit(Item item, ref float crit)
    {
        if (item.DamageType == DamageClass.Ranged && (item.useAmmo == AmmoID.Bullet || item.useAmmo == AmmoID.CandyCorn || item.useAmmo == AmmoID.Coin ))
        {
            crit += gunCritBonus;
        }
    }

}