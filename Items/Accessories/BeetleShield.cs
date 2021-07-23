using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class BeetleShield : ModItem
	{

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Beetle Shield");
			Tooltip.SetDefault("A powerful shield\n10 defense\nDamage taken reduced by 10%\nLife regen increased by 2");
		}
		
		public override void SetDefaults() 
		{
			item.width = 24;
			item.height = 28;
			item.value = Item.sellPrice(gold: 20);
			item.rare = ItemRarityID.Yellow;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 10;
			player.endurance += 0.10f;
			player.lifeRegen += 2;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);

			// Testing Recipe
			recipe.AddIngredient(ItemID.Wood, 1);

			// Actual recipe
			//recipe.AddIngredient(ItemID.BeetleHusk, 10);
			//recipe.AddIngredient(ItemID.ShinyStone, 1);

			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}