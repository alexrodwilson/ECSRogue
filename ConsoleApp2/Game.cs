using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleApp2.Core;
using ConsoleApp2.Systems;
using ConsoleApp2.Components;
using RogueSharp.Random;
using ConsoleApp2.Map_Generators;
using ConsoleApp2.MapUtils;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework.Input;
using ConsoleApp2.Utilities;
using ConsoleApp2.Behaviours;

namespace ECSRogue
{
    class Game
    {
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 25;
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;
        private static readonly int _statWidth = 20;
        private static readonly int _statHeight = 70;
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;
        

        //private static RLRootConsole _rootConsole;
        //private static RLConsole _mapConsole;
        //private static RLConsole _messageConsole;
        //private static RLConsole _statConsole;
        //private static RLConsole _inventoryConsole;
        private static Console _rootConsole;
        private static Console _debugConsole;
        private static Console _mapConsole;
        private static Command currentPlayerCommand;
        private static IEnumerable<Command> _commands;


        private static IContext  _context;
        public static IRandom Random { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        // Temporary member variable just to show our MessageLog is working
        //private static int _steps = 0;


        static void Main(string[] args)
        {
            //string fontFileName = "terminal8x8.png";
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            // The title will appear at the top of the console window 
            // also include the seed used to generate the level
            string consoleTitle = $"RougeSharp V3 Tutorial - Level 1 - Seed {seed}";

            // Setup the engine and creat the main window.
            SadConsole.Game.Create("IBM.font", _mapWidth, _mapHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();

            SadConsole.Game.Instance.Dispose();
  
        }

        private static void Init()
        {
 
            _mapConsole = new Console(_mapWidth, _mapHeight);
            _debugConsole = new Console(_mapConsole.Width, _mapConsole.Height);
            _debugConsole.TextSurface.DefaultBackground = Color.Transparent;
           // _mapConsole.TextSurface = new SadConsole.Surfaces.BasicSurface(_mapWidth, _mapHeight);
            _commands = GeneralUtils.ProvideBasicCommands();



            // Set our new console as the "thing" to render and process
            _debugConsole.Parent = _mapConsole;
            SadConsole.Global.CurrentScreen = _mapConsole;
            
            Entity entity1 = new Entity(1, new List<Component>{ new Position(10, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Collidable() , new Health(25), new FollowedByCamera(), new Schedulable(), new BaseStats(15, 15, 100)});
            Entity entity2 = new Entity(2, new List<Component> { new Position(0, 0),
                new Renderable('K', Colors.KoboldColor), new Collidable(),
                new Health(10), new BaseStats(10, 10, 100),
                new Sentient(new BeingConfused()), new Schedulable() });
            Entity entity3 = new Entity(3, new List<Component>{ new Position(15, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Health(25)});
            Entity entity4 = new Entity(2, new List<Component> { new Position(0, 0),
                new Renderable('K', Colors.KoboldColor), new Collidable(),
                new Health(10), new BaseStats(10, 10, 100),
                new Sentient(new BeingConfused()), new Schedulable() });
            List<Entity> testEntities = new List<Entity> {entity1, entity2, entity4 };
            GameMap map = MapGenerators.BasicRooms( _mapWidth * 2, _mapHeight * 2, 40, 13, 7, Random);
            _mapConsole.TextSurface = new SadConsole.Surfaces.BasicSurface(map.GetWidth(), map.GetHeight());
            _mapConsole.TextSurface.RenderArea = new Rectangle(0, 0, _mapWidth, _mapHeight);
            
            _context = new Pool(map, testEntities);
            //_view = new View(_mapWidth, _mapHeight);
            TestPlacer testPlacer = new TestPlacer();
            testPlacer.Place(testEntities, Random, map);

            UpdatePlayerFov.Execute(_context);
            UpdateView.Execute(_mapConsole, _context);
            RenderMap.Execute(_mapConsole, _context);
            RenderEntities.Execute(_mapConsole, _context);
        }

        private static void Update(GameTime delta)
        {
            DebugInfo.Execute(_mapConsole, _debugConsole, _context);
            if ((currentPlayerCommand = HandleInput.Execute(_commands)) != null)
            {
                ResetTimeUnits.Execute(_context);
                PlayTurn.Execute(currentPlayerCommand, _context);
                UpdatePlayerFov.Execute(_context);
                UpdateView.Execute(_mapConsole, _context);
                RenderMap.Execute(_mapConsole, _context);
                RenderEntities.Execute(_mapConsole, _context);   
            }

        }
       
    }


}
