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
                if (entity1.position.X < entity2.getBounds().Right)
                {
                    if (entity1.position.X > entity2.getBounds().Left)
                    {
                        entity1.velocity.X = 0.0f;
                        entity1.position.X = entity2.getBounds().Right;
                    }
                }
                if (entity1.position.X > entity2.getBounds().Left - entity1.getWidth())
                {
                    if (entity1.position.X < entity2.getBounds().Right)
                    {
                        entity1.velocity.X = 0.0f;
                        entity1.position.X = entity2.getBounds().Left - entity1.getWidth();
                    }
                }
            }
        }
    }
}
