using System;

namespace SimpleEnemyFight.Domain.Models
{
    public class InputHandler
    {
        ConsoleKeyInfo cki;
        public void ReadInput() {
            cki = Console.ReadKey(true);
            Console.WriteLine("You pressed the '{0}' key.", cki.Key);
        }
    }
}