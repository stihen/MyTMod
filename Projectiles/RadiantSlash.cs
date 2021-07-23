using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTMod.Projectiles
{
    public class RadiantSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ragnell Slash");
            Main.projFrames[projectile.type] = 7; // 28;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 20;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.penetrate = 1; // How many times the projectile can hit something before it dissapears.
            projectile.timeLeft = 300; // Number of frames the projectile is gonna last.  300 = 5 sec as Terrari runs at 60 frames per secound)
            projectile.alpha = 0; //Transparency, 0 -> None, 255 -> full
            projectile.light = 1f;
            projectile.tileCollide = true;
            projectile.scale = 1.2f;
            drawOriginOffsetY = -25; // To make the center of the projectile correct
        }

        public override void AI()
        {
            Dust.CloneDust(56);

            // Rotating the projectile to always point in the direction it's moving.
            if (projectile.velocity != Vector2.Zero)
            {
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi;
            }
            projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation += MathHelper.Pi;
            }

            // Loop through the 7 animation frames, spending 3 ticks (frames) on each.
            int ticks = 2;
            if (++projectile.frameCounter >= ticks)
            {
                projectile.frameCounter = 0;
                if (projectile.frame < 6) // 0,1,2,3,4,5,6 = 7 frames
                {
                    projectile.frame++;
                }
            }

            // Very simple homing AI
            for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                if (!target.friendly)
                {
                    float projVel = projectile.velocity.Length(); // projectile velocity = weapon's shootSpeed

                    //Get the trajectory from the projectile to the target
                    float xDistance = target.position.X + target.width * 0.5f - projectile.Center.X;
                    float yDistance = target.position.Y - projectile.Center.Y;
                    float distance = (float)System.Math.Sqrt((double)(xDistance * xDistance + yDistance * yDistance));
                    
                    //If target within the range of 400 pixles, is not behind tiles and is active.
                    if (distance < 400f && !target.behindTiles && target.active && target.CanBeChasedBy(this))
                    {
                        projectile.velocity.X = projVel * xDistance / distance;
                        projectile.velocity.Y = projVel * yDistance / distance;
                    }
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Dust.CloneDust(56);
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            int healingAmount = 1;
            Player p = Main.player[projectile.owner];
            if (crit)
            {
                target.AddBuff(BuffID.Ichor, 120);  // -20 defense for 2 sec
                p.statLife += healingAmount;
                p.HealEffect(healingAmount, true); // Spawns a green numbes above the player showing value healed
            }
        }
    }
}
