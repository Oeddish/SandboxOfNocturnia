using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;

namespace FragmentsOfNocturnia
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class FragmentsOfNocturnia : Mod
	{
        public override void AddRecipes()/* tModPorter Note: Removed. Use ModSystem.AddRecipes */
        {
            /*Recipe recipeObsRose = Recipe.Create(ItemID.ObsidianRose);
            recipeObsRose.AddIngredient(ItemID.Obsidian, 10);
            recipeObsRose.AddIngredient(ItemID.HellstoneBar, 5);
            recipeObsRose.AddTile(TileID.Anvils); // Example crafting station
            recipeObsRose.Register();*/

            /*Recipe recipeCrimtane = Recipe.Create(ItemID.CrimtaneOre);
            recipeCrimtane.AddCustomShimmerResult(ItemID.DemoniteOre, 1);
            recipeCrimtane.Register();

            Recipe recipeDemonite = Recipe.Create(ItemID.DemoniteOre);
            recipeDemonite.AddCustomShimmerResult(ItemID.CrimtaneOre, 1);
            recipeDemonite.Register();*/
        }
    }
}
