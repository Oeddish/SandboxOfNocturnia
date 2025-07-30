using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.ItemDropRules;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FragmentsOfNocturnia.Content.System;

namespace FragmentsOfNocturnia.Content.Systems
{
    public class NocturniaSystem : GlobalNPC
    {

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (NpcType.IsBat(npc.type))
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.BatEssence>(), 3));
            }
            switch (npc.type)
            {
                case NPCID.Raven:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.RavenFeather>(), 3, 4, 6));
                    break;
                case NPCID.BoneSerpentHead:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.DragonBone>(), 3, 6, 8));
                    break;
                case NPCID.BloodZombie:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.BloodVial>(), 2));
                    break;
                case NPCID.CrimsonAxe:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.ViolentCatalyst>()));
                    break;
                case NPCID.CursedHammer:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.RottingCatalyst>()));
                    break;
                case NPCID.EnchantedSword:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Items.HolyCatalyst>()));
                    break;
                default:
                    break;
            }
        }
    }
}
