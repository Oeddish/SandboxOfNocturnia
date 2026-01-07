using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria.ID;

namespace FragmentsOfNocturnia.Content.TileEntities
{
    internal class NightmareChestTileEntity : ModTileEntity
    {
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            // Adjust i and j to the top-left corner of the multi-tile if necessary
            // For a 2x2 chest, the origin is usually at (0, 1) meaning i,j might be bottom-left
            // You already have logic in place to handle this in ModTile.RightClick
            // Make sure the i, j passed to Place and Chest.CreateChest is the top-left.
            // If TileObjectData.newTile.Origin = new Point16(0, 1) is used, then the clicked tile
            // (i,j) will be the bottom-left. Adjust to top-left.
            if (Main.tile[i, j].TileFrameX % (TileObjectData.newTile.Width * 18) != 0)
            { // Check if not top-left frame
                i--; // Adjust to top-left
            }
            if (Main.tile[i, j].TileFrameY != 0)
            { // Check if not top-left frame
                j--; // Adjust to top-left
            }

            int placedEntity = Place(i, j); // This places and registers the TileEntity

            int chestIndex = Chest.CreateChest(i, j); 
            if (chestIndex != -1)
            {
                // Set the default name using localization
                string defaultName = Language.GetTextValue("Mods.FragmentsOfNocturnia.Items.NightmareChest.DisplayName");
                Main.chest[chestIndex].name = defaultName;

                // Important for multiplayer: Tell other clients about the name change.
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.ChestName, -1, -1, null, i, j, defaultName.Length > 0 ? 1 : 0, 0f, 0, 0, 0); 
                }
            }

            return placedEntity;
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.TileType == ModContent.TileType<Tiles.Furniture.NightmareChestTile>();
        }

        public override void OnKill()
        {
            // Placeholder: Can add logic here to handle what happens when the chest tile is broken,
            // like dropping custom items or performing other actions.
        }
    }
}
