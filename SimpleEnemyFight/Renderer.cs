using SimpleEnemyFight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleRenderer
{
    internal class Renderer
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private char[,] buffer;
        private string spriteSheet;
        private string[] sprites;

        public Renderer(int width = 100, int height = 30)
        {
            this.Width = width;
            this.Height = height;
            buffer = new char[height, width];

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    buffer[i, j] = ' ';
                }
            }

            spriteSheet = File.ReadAllText("C:/dev/SimpleEnemyFight/SimpleEnemyFight/SpriteSheet.txt");
            sprites = spriteSheet.Split('#');
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = sprites[i].Remove(0, sprites[i].IndexOf(';') + 1);
            }
            //Console.WriteLine(sprites[0]);
            //Console.WriteLine(sprites[0].IndexOf('\n', 4));
        }

        public void Draw()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    //if (buffer[i, j] != ' ') Console.ForegroundColor = ConsoleColor.Red;
                    //else Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(buffer[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void Update()
        {
            Console.Clear();
            Draw();
            Console.SetCursorPosition(0, 0);
        }

        public void Rect(int x, int y, int width, int height)
        {
            for (int i = y; i < y + height; i++)
            {
                for (int j = x; j < x + width; j++)
                {
                    buffer[i, j] = '█';
                }
            }
        }

        public void Sprite(ESprites sprite, int x = 0, int y = 0, int width = 23, int height = 5)
        {
            for (int i = y; i < y + height; i++)
            {
                for (int j = x; j < x + width; j++)
                {
                    if(sprites[(int)sprite][(i - y) * width + (j - x)] != '\n') buffer[i, j] = sprites[(int)sprite][(i - y) * width + (j - x)];
                }
            }
        }

    }
}
