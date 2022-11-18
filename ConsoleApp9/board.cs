using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chessapp
{
    public class Board
    {
        public int turn = 3;
        public string[,] BoardLayout = new string[8, 8];

        public List<string> deadpieces { get; set; }

        public List<string> pieces { get; set; }

        public Board()
        {

            pieces = new List<string>();
            deadpieces = new List<string>();


        }



        string[] Player1pieces = { "r1", "n1", "b1", "k1", "q1", "b2", "n2", "r2" };
        string[] Player2pieces = { "R1", "N1", "B1", "Q1", "K1", "B2", "N2", "R2" };

        public void initialize()
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    if (i == 0) { BoardLayout[i, j] = Player1pieces[j]; pieces.Add(Player2pieces[j]); }

                    else if (i == 7) { BoardLayout[i, j] = Player2pieces[j]; pieces.Add(Player1pieces[j]); }

                    else if (i == 6) { BoardLayout[i, j] = $"P{j + 1}"; pieces.Add($"P{j + 1}"); }

                    else if (i == 1) { BoardLayout[i, j] = $"p{j + 1}"; pieces.Add($"p{j + 1}"); }

                    else { BoardLayout[i, j] = "  "; }


                }
            }
        }

        public void Print()
        {

            Console.WriteLine("R = Rook, N = Knight, P = Pawn, K = King, B = Bishop, Q = Queen");
            Console.WriteLine();

            int count = 1;
            int row = 1;

            foreach (string i in BoardLayout)
            {

                if (i.ToUpper() == i && !i.Contains(' ') && i != "XX")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (count == 8)
                    {


                        Console.WriteLine("|  {0}  |", i); count = 1;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{row}");
                        Console.ResetColor();
                        row++;

                        void printnum()
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{row - 1}");
                            Console.ResetColor();
                        }
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                        Console.Write($"-----------------------------------------------------------------");
                        Console.ResetColor();

                        printnum();

                    }
                    else
                    {
                        Console.Write("|  {0}  |", i);
                        count++;
                    }
                    Console.ResetColor();

                }
                else if (i == "XX" && count != 8)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("|  {0}  |", i); Console.ResetColor();
                    count++;
                }
                else if (i.ToUpper() != i && i != "XX")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    if (count == 8)
                    {


                        Console.WriteLine("|  {0}  |", i); count = 1;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{row}");
                        Console.ResetColor();
                        row++;

                        void printnum()
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{row - 1}");
                            Console.ResetColor();
                        }
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                        Console.Write($"-----------------------------------------------------------------");
                        Console.ResetColor();

                        printnum();

                    }
                    else
                    {
                        Console.Write("|  {0}  |", i);
                        count++;
                    }
                    Console.ResetColor();

                }
                else
                {
                    if (count == 8)
                    {

                        if (i == "XX")
                        {
                            Console.ForegroundColor = ConsoleColor.Red; count = 1;
                            Console.WriteLine("|  {0}  |", i); Console.ResetColor(); Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"{row}");
                            Console.ResetColor();
                            row++;
                        }




                        else
                        {
                            Console.WriteLine("|  {0}  |", i); count = 1;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"{row}");
                            Console.ResetColor();
                            row++;
                        }

                        void printnum()
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"{row - 1}");
                            Console.ResetColor();
                        }
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;

                        Console.Write($"-----------------------------------------------------------------");
                        Console.ResetColor();

                        printnum();

                    }


                    else
                    {
                        Console.Write("|  {0}  |", i);
                        count++;
                    }
                }

            }



            string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
            foreach (string letter in Columns)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.Write("   {0}    ", letter);
                Console.ResetColor();


            }



            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"List of Pieces Captured: {string.Join(", ", deadpieces)}");
            Console.ResetColor();

            Console.WriteLine("-------------------------------------------------------------------------");

            if (turn % 2 == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("It's Green's turn");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("It's Blue's turn");
                Console.ResetColor();
            }

            if (deadpieces.Contains("K1"))
            {
                Console.WriteLine("Game over! Blue wins!");
            }
            if (deadpieces.Contains("k1"))
            {
                Console.WriteLine("Game over! Green wins");
            }

        }

    }
}
