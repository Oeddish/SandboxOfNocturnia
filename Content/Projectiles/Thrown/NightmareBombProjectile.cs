using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Projectiles.Thrown
{
    public class NightmareBombProjectile : ModProjectile
    {
        // Anything that breaks more than 400 tiles will cause tiles to be lost
        // due to the limit of 400 tiles that can be broken per frame.
        private static readonly int COUNT_BLAST_TILES_DOWN = 200;
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.StickyBomb;

        public override void SetStaticDefaults()
        {
            // Set display name in localization or here
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.StickyBomb);
            // AIType = ProjectileID.StickyBomb;
            Projectile.timeLeft = 100;      // Time in seconds: 1.67 seconds
            // Prevent the explosion from damaging players
            Projectile.friendly = true;   // Only damages enemies
            Projectile.hostile = false;   // Never damages players
            Projectile.tileCollide = true; // Ensure it collides with tiles
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override bool CanHitPlayer(Player target)
        {
            // Prevents this projectile from damaging any player
            return false;
        }

        private static int GetPlayerPickPower(Player player)
        {
            int bestPickPower = 0;
            for (int i = 0; i < player.inventory.Length; i++)
            {
                Item item = player.inventory[i];
                if (item != null && item.pick > bestPickPower)
                {
                    bestPickPower = item.pick;
                }
            }
            return bestPickPower;
        }

        private static int GetMinPickLevel(Tile tile)
        {
            switch (tile.TileType)
            {
                // Common blocks (no pick required)
                case TileID.Dirt:
                case TileID.Stone:
                case TileID.Sand:
                case TileID.ClayBlock:
                case TileID.Mud:
                case TileID.Silt:
                case TileID.Slush:
                    return 0;

                // Pre-hardmode ores
                case TileID.Copper:
                case TileID.Tin:
                    return 35;
                case TileID.Iron:
                case TileID.Lead:
                    return 40;
                case TileID.Silver:
                case TileID.Tungsten:
                    return 45;
                case TileID.Gold:
                case TileID.Platinum:
                    return 55;
                case TileID.Demonite:
                case TileID.Crimtane:
                    return 55;

                // Hardmode ores
                case TileID.Cobalt:
                    return 65;
                case TileID.Palladium:
                    return 100;
                case TileID.Mythril:
                    return 110;
                case TileID.Orichalcum:
                    return 110;
                case TileID.Adamantite:
                    return 150;
                case TileID.Titanium:
                    return 150;
                case TileID.Chlorophyte:
                    return 200;

                // Special blocks
                case TileID.Meteorite:
                    return 50;
                case TileID.Obsidian:
                    return 55;
                case TileID.Hellstone:
                    return 65;
                case TileID.Ebonstone:
                case TileID.Crimstone:
                case TileID.Pearlstone:
                    return 65;
                case TileID.LihzahrdBrick:
                    return 210; // Lihzahrd Bricks require Picksaw

                // Dungeon bricks (require Skeletron defeated and 0 pick, but you may want to block these)
                case TileID.BlueDungeonBrick:
                case TileID.GreenDungeonBrick:
                case TileID.PinkDungeonBrick:
                {
                    // Check if Skeletron is defeated
                    if (Main.hardMode || NPC.downedBoss3)
                    {
                        return 0; // Can break with any pick
                    }
                    else
                    {
                        return 210; // Require Picksaw or better
                    }
                }

                // Default for unknown tiles
                default:
                    // Check for modded tiles
                    ModTile modTile = ModContent.GetModTile(tile.TileType);
                    if (modTile != null)
                    {
                        return modTile.MinPick;
                    }
                    return 0;
            }
        }
        public override void Kill(int timeLeft)
        {
            // Get the player who threw the bomb
            Player player = Main.player[Projectile.owner];

            // Player's leftmost tile (feet)
            int playerTileX = (int) (player.position.X / 16f);
            int playerTileY = (int) ((player.position.Y + player.height) / 16f) - 1; // feet Y

            // Bomb impact tile
            int bombTileX = (int) (Projectile.Center.X / 16f);
            int bombTileY = (int) (Projectile.Center.Y / 16f);

            int playerPickPower = GetPlayerPickPower(player);

            // Decide which 2 tiles to blast
            int blastStartX;
            if (bombTileX <= playerTileX)
            {
                // Bomb landed at or to the left of player: blast player's 2 tiles (left and left+1)
                blastStartX = playerTileX;
            }
            else
            {
                // Bomb landed to the right: blast right 2 tiles (right-1 and right)
                blastStartX = playerTileX + 1;
            }

            // Boom sound! Does not play on multiplayer clients
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            // Now blast from blastStartX to blastStartX+1 (2 tiles wide)
            for (int x = blastStartX; x <= blastStartX + 1; x++)
            {
                for (int y = bombTileY; y < bombTileY + COUNT_BLAST_TILES_DOWN; y++)
                {
                    if (WorldGen.InWorld(x, y, 10))
                    {
                        Tile tile = Framing.GetTileSafely(x, y);
                        int requiredPickPower = GetMinPickLevel(tile);

                        if (tile.HasTile
                            && Main.tileSolid[tile.TileType]
                            && !TileID.Sets.BasicChest[tile.TileType]
                            && playerPickPower >= requiredPickPower)
                        {
                            WorldGen.KillTile(x, y, false, false, false);
                            if (Main.netMode == NetmodeID.Server)
                                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, y);
                        }
                    }
                }
            }
        }
    }
}