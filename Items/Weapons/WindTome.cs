using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace MyTMod.Items.Weapons
{
    public class WindTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Tome"); //Wind Tome
            Tooltip.SetDefault("Summons a homing wind slash that will tear down your enemies\nEffective against Flying Units.\nCauses Dryad's Bane on hit");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.DemonScythe);
            item.magic = true;
            item.damage = 12;
            item.useTime = 30; // Higher means higher use-time
            item.mana = 6;
            item.shoot = mod.ProjectileType("WindSlashProj");
            item.rare = ItemRarityID.Red;
            item.noMelee = true;
            item.shootSpeed = 19; // Higher means higher projectile velocity
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            // Testing recipe
            recipe.AddIngredient(ItemID.Wood, 1);

            // Actual recipe
            //recipe.AddIngredient(ItemID.SpellTome, 1);
            //recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            //recipe.AddIngredient(ItemID.Emerald, 1);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
