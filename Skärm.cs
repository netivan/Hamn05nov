using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Threading;
using System.Linq;

namespace Hamnen
{
    class Skärm : Mother
    {
        //const int xPosKaj = 9, yPosKaj = 4;
        public static int yPositionUt = yPosUt + 1;
        public static int yPositionIn = yPosIn + 1;

        public static void Titel()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(40, 1);
            Console.Write("Stockholms Hamn");
            SetCursorPosition(1, yPosKaj - 1);
            Write("KAJ -> |");
            SetCursorPosition(xPosKaj, yPosKaj - 1);
            Write(" 0.........10........20........30.........40........50........60...|");

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(xPosIn, yPosIn);
            Console.Write("   Inkommande Båtar    ");

            Console.SetCursorPosition(xPosUt, yPosUt);
            Console.Write("    Utgående Båtar    ");

            Console.SetCursorPosition(xPosHmReg, yPosHmReg);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("       H A M N R E G I S T R E T       ");
            Console.BackgroundColor = ConsoleColor.Black;

            
        }

        public static void skrivaDay(int day)
        {
            Console.SetCursorPosition(11, 1);

            Console.WriteLine($"Dag {day}");      // pågående dag

            Console.SetCursorPosition(80, 1);
            Console.WriteLine($"Dag {day}");

        }
        public static void skrivaIngående(string s)
        {
            SetCursorPosition(xPosIn, yPositionIn++);
            Write(s);
        }

        public static void skrivautgående(string s)
        {
            Console.SetCursorPosition(xPosUt, yPositionUt++);
            Console.Write(s);
        }
        public static void cancelday()
        {
            string space = new string(' ', 30);
            for (int i = yPosIn + 1; i < yPosIn + 6; i++)
            {
                SetCursorPosition(xPosIn, i);
                Write(space);
            }

            for (int i = yPosUt + 1; i < (yPositionUt + 1); i++)
            {
                SetCursorPosition(xPosUt, i);
                Write(space);
            }

            yPositionUt = yPosUt + 1;
            yPositionIn = yPosIn + 1;
        }

        public static bool pausa(int delay)
        {
            for (int i = 1; i < delay; i++)
            {
                Thread.Sleep(1);   // ny dag
                if (Console.KeyAvailable == true)
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == ' ')
                        Console.ReadKey(true);
                    else
                        return true;
                }
            }
            return false;

        }
    }
}
