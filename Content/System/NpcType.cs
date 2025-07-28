using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.System
{
    internal class NpcType
    {
        public static bool IsBat(int npcType)
        {
            return npcType == NPCID.CaveBat ||
                   npcType == NPCID.GiantBat ||
                   npcType == NPCID.IceBat ||
                   npcType == NPCID.Lavabat ||
                   npcType == NPCID.VampireBat ||
                   npcType == NPCID.JungleBat ||
                   npcType == NPCID.SporeBat ||
                   npcType == NPCID.Hellbat;        // Hellbat was not in original DropModifier, but is a bat type
        }
    }
}
