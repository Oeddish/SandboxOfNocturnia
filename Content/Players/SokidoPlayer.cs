using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Players
{
    internal class SokidoPlayer : ModPlayer
    {
        public int playerDirection { get; set; }
        public bool attacking { get; set; }
        public bool projActive { get; set; } = false;

        public Vector2 mousePosition { get; set; }
    }
}
