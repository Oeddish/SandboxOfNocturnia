using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FragmentsOfNocturnia.Content.Systems
{
	public class RecipeGroups : ModSystem
	{
		public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => "Any Silver or Tungsten Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });

            RecipeGroup.RegisterGroup("FragmentsOfNocturnia:SilverOrTungsten", group);
        }

	}
}