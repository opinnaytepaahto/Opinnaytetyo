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
                        if (bounds.Intersects(e.Hitbox))
                        {
                            return false;
                        }
                    }
                    break;
                case MainGame.state.PLAY:
                    foreach (Entity e in Level1.collidables)
                    {
                        if (bounds.Intersects(e.Hitbox))
                        {
                            return false;
                        }
                    }
                    break;

                case MainGame.state.EXIT:
                    foreach (Entity e in GameOver.collidables)
                    {
                        if (bounds.Intersects(e.Hitbox))
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
            for (int i = 0; i < Level1.enemies.Count; i++)
            {
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (Level1.enemies[i].Hitbox.Intersects(bullets[j].Hitbox))
                    {
                        Level1.enemies[i].hit = true;
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
                    if (Player.playerRectangleStatic.Intersects(enemyBullets[i].Hitbox))
                    {
                        if (GameStage.CurrentLevel == GameStage.Level.LEVEL1)
                        {
                            Level1.player.hit = true;

                            if (enemyBullets[i].id == "soldier")
                            {
                                Level1.player.currentHit = Player.HitType.SOLDIER;
                            }

                            if (enemyBullets[i].id == "magic")
                            {
                                Level1.player.currentHit = Player.HitType.MAGIC;
                            }

                            if (enemyBullets[i].id == "reaper")
                            {
                                Level1.player.currentHit = Player.HitType.REAPER;
                            }

                            enemyBullets.RemoveAt(i);
                        }

                        if (GameStage.CurrentLevel == GameStage.Level.LEVEL2)
                        {
                            Level1.player.hit = true;

                            if (enemyBullets[i].id == "soldier")
                            {
                                Level1.player.currentHit = Player.HitType.SOLDIER;
                            }
                            if (enemyBullets[i].id == "magic")
                            {
                                Level1.player.currentHit = Player.HitType.MAGIC;
                            }
                            if (enemyBullets[i].id == "reaper")
                            {
                                Level1.player.currentHit = Player.HitType.REAPER;
                            }

                            enemyBullets.RemoveAt(i);
                        }
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
