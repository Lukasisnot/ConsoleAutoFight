using System;
using System.IO;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{

    internal class RendererLegacy
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private Spot[,] buffer;
        private string spriteSheet;
        private string[] sprites;

        public RendererLegacy(int width = 100, int height = 30)
        {
            this.Width = width;
            this.Height = height;
            buffer = new Spot[height, width];

            // init buffer
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    buffer[i, j] = new Spot();
                }
            }

            // init and format SpriteSheet
            // spriteSheet = File.ReadAllText("C:\\dev\\SimpleEnemyFight\\SimpleEnemyFight\\Domain\\Sprites\\SpriteSheet.txt");
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;              
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"..\..\Domain\Sprites\SpriteSheet.txt");  
            string sFilePath = Path.GetFullPath(sFile);  

            spriteSheet = File.ReadAllText(sFilePath);
            sprites = spriteSheet.Split('#');
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = sprites[i].Remove(0, sprites[i].IndexOf(';') + 1);
            }
        }

        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.ForegroundColor = buffer[i, j].Color;
                    Console.Write(buffer[i, j].Char);
                }
                Console.WriteLine();
            }
        }

        public void Update()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Draw();
            Console.SetCursorPosition(0, 0);
            Console.SetWindowPosition(0 , 0);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    buffer[i, j].Char = ' ';
                    buffer[i, j].Color = ConsoleColor.White;
                }
            }
        }

        public void Rect(int x, int y, int width, int height)
        {
            for (int i = y; i < y + height; i++)
            {
                for (int j = x; j < x + width; j++)
                {
                    buffer[i, j].Char = '█';
                    buffer[i, j].Color = ConsoleColor.White;
                }
            }
        }

        public void Sprite(ESprites sprite, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            int ch = 0;
            for (int i = y; i < this.Height; i++)
            {
                for (int j = x; j < this.Width; j++)
                {
                    // char currentChar = sprites[(int)sprite][Math.Min(sprites[(int)sprite].Length - 1, ch++)];
                    char currentChar = sprites[(int)sprite][ch++];
                    if(currentChar == '!') return;
                    if(currentChar == '\n') break;
                    if(currentChar == '\r' || currentChar == ' ') continue;
                    
                    buffer[i, j].Char = currentChar;
                    buffer[i, j].Color = color;
                }
            }
        }

        public void HealthBar(int x, int y, int width, EnemyLegacy entity,bool flip = false, ConsoleColor color = ConsoleColor.Green)
        {
            float hpPerChar = (float)entity.MaxHp / width;
            int hpChars = (int)Math.Ceiling(entity.Hp / hpPerChar);
            if (!entity.IsAlive) hpChars = 0;
            
            for (int j = flip ? x + width - entity.Name.Length : x; j < (flip ? width + x : entity.Name.Length) ; j++)
            {
                buffer[y, j].Char = entity.Name[j - (flip ? x + width - entity.Name.Length : x)];
                buffer[y, j].Color = entity.Color;
            }
            
            for (int j = x; j < x + width; j++)
            {
                if (flip) buffer[y + 1, j].Char = j - x < Math.Abs(hpChars - width) ? '\u2591' : '\u2588';
                else buffer[y + 1, j].Char = j - x < hpChars ? '\u2588' : '\u2591'; // █ ░
                
                buffer[y + 1, j].Color = color;
            }
        }

    }
}
