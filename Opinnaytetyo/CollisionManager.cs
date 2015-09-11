using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class CollisionManager
    {
        public static bool checkCollision(Rectangle bounds)
        {
            switch(MainGame.currentState)
            {
                case MainGame.state.MENU:
                    foreach (Entity e in MainMenu.collidables)
                    {
                        if (bounds.Intersects(e.getBounds()))
                        {
                            return false;
                        }
                    }
                    break;
                case MainGame.state.PLAY:
                    foreach (Entity e in GameStage.collidables)
                    {
                        if (bounds.Intersects(e.getBounds()))
                        {
                            return false;
                        }
                    }
                    break;
            }

            return true;
        }

        public static void bulletCollision(List<Projectile> bullets)
        {
            for (int i = 0; i < GameStage.enemies.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (GameStage.enemies[i].getBounds().Intersects(bullets[j].getBounds()))
                    {
                        GameStage.enemies[i].hit = true;
                        bullets.RemoveAt(j);
                    }
                }
            }
        }

        public static void enemyBulletCollision(List<Projectile> enemyBullets)
        {
            if (enemyBullets != null)
            {
                for (int i = 0; i < enemyBullets.Count; i++)
                {
                    if (Player.playerRectangleStatic.Intersects(enemyBullets[i].getBounds()))
                    {
                        GameStage.player.hit = true;

                        if (enemyBullets[i].id == "soldier")
                        {
                            GameStage.player.currentHit = Player.HitType.SOLDIER;
                        }

                        enemyBullets.RemoveAt(i);
                    }
                }
            }
        }

        //public static void checkButtonCollision(Player entity1, PlayerButton entity2)
        //{
        //    if (entity1.getBounds().Intersects(entity2.getBounds()))
        //    {
        //        if (entity1.velocity.X > 0 && entity1.velocity.Y == 0)
        //        {
        //            entity1.velocity.X = 0;
        //            entity1.position.X = entity2.getBounds().Left - entity1.getWidth();
        //        }
        //        if (entity1.velocity.X < 0 && entity1.velocity.Y == 0)
        //        {
        //            entity1.velocity.X = 0;
        //            entity1.position.X = entity2.getBounds().Right;
        //        }
        //        if (entity1.velocity.Y > 0)
        //        {
        //            entity1.velocity.Y = 0;
        //            entity1.position.Y = entity2.getBounds().Top - entity1.getHeight();
        //        }

        //        if (entity2.id == "play")
        //        {
        //            entity1.colPlay = true;
        //        }

        //        if (entity2.id == "exit")
        //        {
        //            entity1.colExit = true;
        //        }

        //        entity1.colliding = true;
        //    }
        //    else
        //    {
        //        entity1.colliding = false;
        //    }
        //}
    }
}
