using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Utilities;

namespace MyTMod.Items.Weapons
{
    public class WindStorm : ModItem
    {
        //public int timer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Storm");
            Tooltip.SetDefault("Summons a destructive windstorm that will tear down your enemies\nCauses Dryad's Bane on hit");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.DemonScythe);
            item.magic = true;
            item.damage = 40;
            item.useTime = 30; // Higher means lower use time
            item.mana = 8;
            item.shoot = mod.ProjectileType("WindSlashProjMult");
            item.rare = ItemRarityID.Red;
            item.noMelee = true;
            item.shootSpeed = 19; // Projectile velocity
        }

        /*
        public override void HoldItem(Player player)
        {
            //Funker!
            player.AddBuff(BuffID.DryadsWard, 1, true);
        }
        */
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            // Testing Recipe
            recipe.AddIngredient(ItemID.Wood, 1);

            // Actual Recipe
            //recipe.AddIngredient(ItemID.SpellTome, 1);
            //recipe.AddIngredient(ItemID.CloudinaBottle, 1);
            //recipe.AddIngredient(ItemID.SoulofMight, 5);
            //recipe.AddIngredient(ItemID.SoulofSight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
