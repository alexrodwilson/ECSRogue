using ConsoleApp2.Components;
using ConsoleApp2.Core;
using ConsoleApp2.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace ConsoleApp2.Utilities
{
     class TimeUnitsThenDexComparer : IComparer<Entity>
    {
        public int Compare(Entity e1, Entity e2)
        {
            int timeUnitsLeft1 = ((Schedulable)e1.GetComponent("Schedulable")).TimeUnits;
            int timeUnitsLeft2 = ((Schedulable)e2.GetComponent("Schedulable")).TimeUnits;
            int difference = timeUnitsLeft2 - timeUnitsLeft1;
            if (difference != 0)
            {
                return difference;
            }
            else
            {
                return ((BaseStats)e2.GetComponent("BaseStats")).Dexterity
                      - ((BaseStats)e1.GetComponent("BaseStats")).Dexterity;
            }
        }
    }
    [TestClass]
     public class Test
    {
        [TestMethod]
        public void TimeUnitsThenDexComparerWorks()
        {
            Entity player = new Entity(1, new List<Component>{ new Position(10, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Collidable() , new Health(25), new FollowedByCamera(),
                new Schedulable(), new BaseStats(15, 15)});
            Entity goblin = new Entity(2, new List<Component> { new Position(0, 0),
                new Renderable('g', Colors.TextHeading), new Collidable(), new Health(10),
                new Schedulable(), new BaseStats(8, 20) });
            Entity ogre = new Entity(3, new List<Component> { new Position(0, 0),
                new Renderable('O', Colors.TextHeading), new Collidable(), new Health(10),
                new Schedulable(), new BaseStats(25, 8) });
            SortedQueue<Entity> entities = new SortedQueue<Entity>(new List<Entity> { player, goblin, ogre }, new TimeUnitsThenDexComparer());
            Console.WriteLine(entities);
            
            goblin = entities.Dequeue();
            ((Schedulable)goblin.GetComponent("Schedulable")).TimeUnits -= 50;
            entities.Enqueue(goblin);
            //Console.WriteLine(entities);
            
            player = entities.Dequeue();
            ((BaseStats)player.GetComponent("BaseStats")).Dexterity -= 50;
            entities.Enqueue(player);
            //Console.WriteLine(entities);
            //Console.ReadLine();
         
            Assert.AreEqual(entities.Dequeue(), new Entity(3, new List<Component>()));

        }
    }
}
