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
        static Renderer renderer;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            renderer = new Renderer(100, 25);
            renderer.Sprite(ESprites.RIGHT_HITS);
            renderer.Update();
            //Enemy e1 = new Enemy("Blud", 7, EWeapons.DAGGER, 100);
            //Console.WriteLine(e1);
            //Enemy e2 = new Enemy("Pepa", 2, EWeapons.STICK, 100);
            //Console.WriteLine(e2);
            Console.SetCursorPosition(0, 0);
            Console.ReadLine();
        }
    }
}
