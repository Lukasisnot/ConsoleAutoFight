using System;
using System.Threading;
using SimpleEnemyFight.Domain.Enums;
using SimpleEnemyFight.Domain.Models;

namespace SimpleEnemyFight
{
    internal class Program
    {
        static GameSim _gameSim;
        static Dungeon _dungeon;
        static InputHandler _inputHandler;
        static Player _player;
        
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Renderer.Init();
            _player = new Player("Lukas", ECharState.STAND, true, ConsoleColor.Cyan, 100, 10, EWeapons.STICK);
            
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                
                _player.Update();
                Renderer.Update();
                
                // Thread.Sleep(20);
                // Console.ReadLine();
            }
            
            // while (true)
            // {
            //     // Console.SetCursorPosition(0, 0);
            //     // Console.CursorVisible = false;
            //     //
            //     // _renderer = new RendererLegacy(100, 20);
            //     // EnemyLegacy pl1 = new EnemyLegacy("Blud", 20, EWeapons.STICK, 50, ConsoleColor.Cyan);
            //     // EnemyLegacy pl2 = new EnemyLegacy("JayJay", 20, EWeapons.STICK, 50, ConsoleColor.Red);
            //     // _gameSim = new GameSim(_renderer, pl1, pl2, 0, 0, 800, 2);
            //     // _gameSim.Start();
            //
            //     Console.ReadLine();
            // }
            
            // _dungeon = new Dungeon(12);
            // Console.WriteLine("Done!");
            // Console.ReadLine();
        }
    }
}
