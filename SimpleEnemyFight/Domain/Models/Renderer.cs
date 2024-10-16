using System;
using System.IO;
using SimpleEnemyFight.Domain.Enums;

namespace SimpleEnemyFight.Domain.Models
{

    public static class Renderer
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        private static Spot[,] buffer;
        private static string spriteSheet;
        private static string[] sprites;

        public static void Init()
        {
            Width = 100;
            Height = 20;
            buffer = new Spot[Height, Width];

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

        public static void Draw()
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

        public static void Update()
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
                    if (!buffer[i, j].ShouldUpdate) continue;
                    buffer[i, j].Char = ' ';
                    buffer[i, j].Color = ConsoleColor.White;
                }
            }
        }

        public static void ResetBufferShouldUpdate()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    buffer[i, j].ShouldUpdate = false;
                }
            }
        }

        public static void Rect(int x, int y, int width, int height)
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

        public static void Text(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            string[] rows = text.Split('\n');
            for (int i = 0; i < rows.Length; i++)
                for (int j = 0; j < rows[i].Length; j++)
                {
                    buffer[y + i, x + j].Char = rows[i][j];
                    buffer[y + i, x + j].Color = color;
                    buffer[y + i, x + j].ShouldUpdate = false;
                }
        }

        public static void Sprite(ESprites sprite, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            int ch = 0;
            for (int i = y; i < Height; i++)
            {
                for (int j = x; j < Width; j++)
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
        
        public static void Sprite(ECharState sprite, bool isLeft = true, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White)
        {
            Sprite(ConvertCharSprite(sprite, isLeft), x, y, color);
        }

        public static void HealthBar(int x, int y, int width, Entity entity, bool flip = false, ConsoleColor color = ConsoleColor.Green)
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
        
        public static ESprites ConvertCharSprite(ECharState state, bool isLeft)
        {
            switch (state)
            {
                case ECharState.STAND:
                    return isLeft ? ESprites.LEFT_STAND : ESprites.RIGHT_STAND;
                    break;
                case ECharState.DODGE:
                    return isLeft ? ESprites.LEFT_DODGE : ESprites.RIGHT_DODGE;
                    break;
                case ECharState.COLLISION:
                    return isLeft ? ESprites.LEFT_COLLISION : ESprites.RIGHT_COLLISION;
                    break;
                case ECharState.ATTACK:
                    return isLeft ? ESprites.LEFT_ATTACK : ESprites.RIGHT_ATTACK;
                    break;
                case ECharState.HEAL:
                    return isLeft ? ESprites.LEFT_HEAL : ESprites.RIGHT_HEAL;
                    break;
                case ECharState.HIT:
                    return isLeft ? ESprites.LEFT_HIT : ESprites.RIGHT_HIT;
                    break;
                case ECharState.DEAD:
                    return isLeft ? ESprites.LEFT_DEAD : ESprites.RIGHT_DEAD;
                    break;
                case ECharState.WIN:
                    return isLeft ? ESprites.LEFT_WIN : ESprites.RIGHT_WIN;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

    }
}
