using System;
using SimpleEnemyFight.Domain.Enums;
using SimpleEnemyFight.Domain.Models;

namespace SimpleEnemyFight
{
    internal class Program
    {
        static RendererLegacy _renderer;
        static GameSim _gameSim;
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                _renderer = new RendererLegacy(100, 20);
                EnemyLegacy pl1 = new EnemyLegacy("Blud", 20, EWeapons.STICK, 50, ConsoleColor.Cyan);
                EnemyLegacy pl2 = new EnemyLegacy("JayJay", 20, EWeapons.STICK, 50, ConsoleColor.Red);
                _gameSim = new GameSim(_renderer, pl1, pl2, 0, 0, 800, 2);
                _gameSim.Start();

                Console.ReadLine();
            }
        }
    }
}
