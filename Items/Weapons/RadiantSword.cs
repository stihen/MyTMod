using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTMod.Items.Weapons
{
    public class RadiantSword : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radiant Sword"); // or Radiant Blade
            Tooltip.SetDefault("A blade once wielded by the Radiant Hero\nGrants its wielder 5 defense\nCritical hits triggers Aether");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.BeamSword);
            item.melee = true;
            item.damage = 180;
            item.scale = 1.09f; // if scale + Legendary Modifyer >= 1.20f, WeaponsOut will put the weapon on the back and not on the side
            item.useTime = 18; // Default: 20, lower -> faster
            item.shoot = mod.ProjectileType("RadiantSlash");
            item.shootSpeed = 22; // projectile velocity
            item.rare = ItemRarityID.Red;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            int healingAmount = 2;
            if (crit)
            {
                target.AddBuff(BuffID.Ichor, 120); // -20 defense for 2 sec
                //target.AddBuff(BuffID.BrokenArmor, 120); // defense halved for 2 sec
                player.statLife += healingAmount;
                player.HealEffect(healingAmount, true); // This spawns the green numbers above the player showing the heal value
            }
        }

        public override void HoldItem(Player player)
        {
            player.statDefense += 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            // Testing Recipe
            recipe.AddIngredient(ItemID.Wood, 1);

            // Actual Recipe
            //recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 12); // for the blade
            //recipe.AddIngredient(ItemID.DarkShard, 5); // for the handle
            //recipe.AddIngredient(ItemID.Emerald, 1);
            //recipe.AddIngredient(ItemID.SoulofMight, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
