using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace MyTMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class BeetleAegis : ModItem
	{
		public int timer;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beetle Aegis");
			Tooltip.SetDefault("A powerful shield providing superior protection\n20 defense\nDamage taken reduced by 10%\nLife regen increased by 4\nWhile equipped, you constantly generate a 40 life shield");
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 28;
			item.value = Item.sellPrice(platinum: 1);
			item.rare = ItemRarityID.Red;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//Shield life (Thorium) - How it works
			//The maxTimer increases with 1 every frame (Terraria is 60 frames per second
			//When the timer reaches 120 (takes 2 seconds) the shield life will increase by 1
			//By adding to the timer we can speed up how quickly the timer reaches 120

			player.statDefense += 20;
			player.GetThoriumPlayer().thoriumEndurance += 0.10f;      //10% damage reduction
			player.lifeRegen += 4;
			player.GetThoriumPlayer().metalShieldMax = 40;            //40 max shield health
			player.GetThoriumPlayer().metalShieldMaxTimer += 3;       //1+3 = 4, 120/4 = 30, 1 shield life per 30 frame -> 2 per sec

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);

			// Recipe for testing
			recipe.AddIngredient(ItemID.Wood, 1);

			// Actual recipe
			//recipe.AddIngredient(mod.ItemType("BeetleShield"), 1);
			//recipe.AddIngredient(ModLoader.GetMod("ThoriumMod"), "AstroBeetleHusk", 1);
			//recipe.AddIngredient(ModLoader.GetMod("ThoriumMod"), "WarpCore", 1); //The Omega Core, used to power the shield

			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}