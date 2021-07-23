using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyTMod.Projectiles
{
    public class WindSlashProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wind Slash Single");
            Main.projFrames[projectile.type] = 28;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 50;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.timeLeft = 300;
            projectile.penetrate = 1;
            projectile.damage = 1; //Har ikke noe aa si
            projectile.alpha = 255; //Transparency, 0 -> None, 255 -> full
            projectile.light = 1.0f;
            projectile.tileCollide = false; // (!)
            projectile.scale = 0.8f;
        }

        public override void AI()
        {

            if (projectile.velocity != Vector2.Zero) // Rotating it to always point in the direction it's facing.
            {
                projectile.rotation = projectile.velocity.ToRotation() + 2*MathHelper.PiOver4;
            }
            projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            if (projectile.spriteDirection == -1)
            {
                projectile.rotation += MathHelper.Pi;
            }

            // Fade in
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 15;
                //projectile.alpha -= 51; // 1, 3, 5, 15, 17, 51, 85, and 255.
            }

            // Allows projectile to pass through tiles until they are more visible
            if (projectile.alpha <= 135 && projectile.tileCollide == false)
            {
                projectile.tileCollide = true;
            }

            // Loop through the 28 animation frames, spending 1 ticks on each.
            if (++projectile.frameCounter >= 1)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 28)
                {
                    projectile.frame = 0;
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
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //target.AddBuff(BuffID.Poisoned, 120);
            target.AddBuff(BuffID.DryadsWardDebuff, 120);
        }

    }
}
