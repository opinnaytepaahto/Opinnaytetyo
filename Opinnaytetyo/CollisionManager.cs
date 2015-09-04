using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opinnaytetyo
{
    class CollisionManager
    {
        public static void checkCollision(Player entity1, Entity entity2)
        {
            if (entity1.getBounds().Intersects(entity2.getBounds()))
            {
                if (entity1.velocity.X > 0 && entity1.velocity.Y == 0)
                {
                    entity1.velocity.X = 0;
                    entity1.position.X = entity2.getBounds().Left - entity1.getWidth();
                }
                if (entity1.velocity.X < 0 && entity1.velocity.Y == 0)
                {
                    entity1.velocity.X = 0;
                    entity1.position.X = entity2.getBounds().Right;
                }
                if (entity1.velocity.Y > 0)
                {
                    entity1.velocity.Y = 0;
                    entity1.position.Y = entity2.getBounds().Top - entity1.getHeight() + 1.0f;
                }
            }
        }

        public static void checkButtonCollision(Player entity1, PlayerButton entity2)
        {
            if (entity1.getBounds().Intersects(entity2.getBounds()))
            {
                if (entity1.velocity.X > 0 && entity1.velocity.Y == 0)
                {
                    entity1.velocity.X = 0;
                    entity1.position.X = entity2.getBounds().Left - entity1.getWidth();
                }
                if (entity1.velocity.X < 0 && entity1.velocity.Y == 0)
                {
                    entity1.velocity.X = 0;
                    entity1.position.X = entity2.getBounds().Right;
                }
                if (entity1.velocity.Y > 0)
                {
                    entity1.velocity.Y = 0;
                    entity1.position.Y = entity2.getBounds().Top - entity1.getHeight() + 1.0f;
                }

                if (entity2.id == "play")
                {
                    entity1.colPlay = true;
                }

                if (entity2.id == "exit")
                {
                    entity1.colExit = true;
                }

                entity1.colliding = true;
            }
            else
            {
                entity1.colliding = false;
            }
        }
    }
}
