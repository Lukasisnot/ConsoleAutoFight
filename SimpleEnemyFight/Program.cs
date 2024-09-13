using BasicConsoleRenderer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleEnemyFight
{
    internal class Program
    {
        static Renderer _renderer;
        static GameSim _gameSim;
        
        static void Main(string[] args)
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;

                _renderer = new Renderer(100, 20);
                Enemy pl1 = new Enemy("Blud", 20, EWeapons.STICK, 50, ConsoleColor.Cyan);
                Enemy pl2 = new Enemy("JayJay", 20, EWeapons.STICK, 50, ConsoleColor.Red);
                _gameSim = new GameSim(_renderer, pl1, pl2, 0, 0, 800);
                _gameSim.Start();

                Console.ReadLine();
            }
        }
    }
}
