using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utilities
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Entity player = new Entity(1, new List<Component>{ new Position(10, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Collidable() , new Health(25), new FollowedByCamera(), new BaseStats(15, 15)});
            Entity goblin = new Entity(2, new List<Component> { new Position(0, 0), new Renderable('g', Colors.TextHeading), new Collidable(), new Health(10), new BaseStats(10, 25) });
            Entity ogre = new Entity(3, new List<Component> { new Position(0, 0), new Renderable('O', Colors.TextHeading), new Collidable(), new Health(10), new BaseStats(20, 8) });

             
            SortedQueue<Entity> entities = new SortedQueue<Entity>(new List<Entity> { player, goblin, ogre },);
        }
    }
}
