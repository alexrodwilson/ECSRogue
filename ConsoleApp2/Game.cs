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

namespace ECSRogue
{
    class Game
    {
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;
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
        private static Console _mapConsole;
        private static Command currentPlayerCommand;
        private static IEnumerable<Command> _commands;


        private static IContext  _context;
        public static IRandom Random { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        // Temporary member variable just to show our MessageLog is working
        //private static int _steps = 0;
        private static View _view;
        static int tempX;
        static int tempY;

        static void Main(string[] args)
        {
            //string fontFileName = "terminal8x8.png";
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            // The title will appear at the top of the console window 
            // also include the seed used to generate the level
            string consoleTitle = $"RougeSharp V3 Tutorial - Level 1 - Seed {seed}";

            // Setup the engine and creat the main window.
            SadConsole.Game.Create("IBM.font", _screenWidth, _screenHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Game.OnUpdate = Update;

            // Start the game.
            SadConsole.Game.Instance.Run();

            //
            // Code here will not run until the game window closes.
            //

            SadConsole.Game.Instance.Dispose();

            //_rootConsole = new RLRootConsole(fontFileName, _screenWidth, _screenHeight, 8, 8, 1f, consoleTitle);
            //_mapConsole = new RLConsole(_mapWidth, _mapHeight);
            //_messageConsole = new RLConsole(_messageWidth, _messageHeight);
            //_statConsole = new RLConsole(_statWidth, _statHeight);
            //_inventoryConsole = new RLConsole(_inventoryWidth, _inventoryHeight);
            //_rootConsole.Update += OnRootConsoleUpdate;
            //_rootConsole.Render += OnRootConsoleRender;

            //MessageLog = new MessageLog();
            //MessageLog.Add("The rogue arrives on level 1");
            //MessageLog.Add($"Level created with seed '{seed}'");
            //PlaceEntityInMiddleOfRoom(entity1, _context);
            //PlaceEntityInMiddleOfRoom(entity3, _context);
  
        }


        internal static void PlaceEntityInMiddleOfRoom(Entity e, IContext context)
        {
            int x = context.GetCurrentMap().Rooms[0].Center.X;
            int y = context.GetCurrentMap().Rooms[0].Center.Y;
            context.GetCurrentMap().PlaceEntity(e, context.GetCurrentMap().GetCell(x,y));
        }

        private static void Init()
        {
 
            _mapConsole = new Console(_mapWidth, _mapHeight);
            _mapConsole.TextSurface = new SadConsole.Surfaces.BasicSurface(_mapWidth, _mapHeight);
            _commands = GeneralUtils.ProvideBasicCommands();

           

            // Set our new console as the "thing" to render and process
            SadConsole.Global.CurrentScreen = _mapConsole;
            
            Entity entity1 = new Entity(1, new List<Component>{ new Position(10, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Collidable() , new Health(25), new FollowedByCamera(), new Schedulable(), new BaseStats(15, 15, 100)});
            Entity entity2 = new Entity(2, new List<Component> { new Position(0, 0), new Renderable('K', Colors.KoboldColor), new Collidable(), new Health(10) });
            Entity entity3 = new Entity(3, new List<Component>{ new Position(15, 10), new Renderable('@', Colors.Player),
                new UnderControl(), new Health(25)});
            Entity entity4 = new Entity(4, new List<Component> { new Position(0, 0), new Renderable('K', Colors.KoboldColor), new Collidable(), new Health(10) });
            List<Entity> testEntities = new List<Entity> {entity1 };
            GameMap map = MapGenerators.BasicRooms(2* _mapWidth, 2 *_mapHeight, 40, 13, 7, Random);
            _mapConsole.TextSurface = new SadConsole.Surfaces.BasicSurface(map.GetWidth(), map.GetHeight());
            
            _context = new Pool(map, testEntities);
            //_view = new View(_mapWidth, _mapHeight);
            TestPlacer testPlacer = new TestPlacer();
            testPlacer.Place(testEntities, Random, map);

            UpdatePlayerFov.Act(_context);
            UpdateView.Act(_mapConsole, _context);
            RenderMap.Act(_mapConsole, _context);
            RenderEntities.Act(_mapConsole, _context);
        }

        private static void Update(GameTime delta)
        {

            //_mapConsole.SetBackColor(0, 0, _mapWidth, _mapHeight, RLColor.Black);
            // _mapConsole.Print(1, 1, "Map", RLColor.White);

            //_statConsole.SetBackColor(0, 0, _statWidth, _statHeight, Color.Brown);
            //_statConsole.Print(1, 1, "Stats", Color.White);

            //_inventoryConsole.SetBackColor(0, 0, _inventoryWidth, _inventoryHeight, Color.Cyan);
            //_inventoryConsole.Print(1, 1, "Inventory", Color.White);


            //if (MovePlayer.Act(_rootConsole.Keyboard, _context))
            //{
            //    CountSteps.Increment();
            //    MessageLog.Add($"Step # {CountSteps.Get()}");
            //}
            // MovePlayer.Act( _context);
            //if (SadConsole.Global.KeyboardState.KeysPressed.
            if ((currentPlayerCommand = HandleInput.Execute(_commands)) != null)
            {
                UpdatePlayerFov.Act(_context);
                ResetTimeUnits.Execute(_context);
                PlayTurn.Execute(currentPlayerCommand, _context);
                UpdateView.Act(_mapConsole, _context);
                RenderMap.Act(_mapConsole, _context);
                RenderEntities.Act(_mapConsole, _context);   
            }

            
            //_mapConsole.SetGlyph(tempX, tempY, '$', Color.Azure);


        }
        //      private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        //   {
        //         RLConsole.Blit(_mapConsole, 0, 0, _mapWidth, _mapHeight,
        //_rootConsole, 0, _inventoryHeight);
        //         RLConsole.Blit(_statConsole, 0, 0, _statWidth, _statHeight,
        //           _rootConsole, _mapWidth, 0);
        //         RLConsole.Blit(_messageConsole, 0, 0, _messageWidth, _messageHeight,
        //           _rootConsole, 0, _screenHeight - _messageHeight);
        //         RLConsole.Blit(_inventoryConsole, 0, 0, _inventoryWidth, _inventoryHeight,
        //           _rootConsole, 0, 0);

        //MessageLog.Draw(_messageConsole);
        //_rootConsole.Draw();
        //     }


    }


}
