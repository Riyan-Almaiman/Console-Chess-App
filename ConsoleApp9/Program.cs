﻿using System.Net;
using System.Threading.Channels;

namespace ChessAppRewrite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //new game board
            Board board = new Board();
            //board to show possible moves
            Board possiblemoves = new Board();

            //create a board of tile addresses for comparisons 
            string[,] TileAddresses = new string[8, 8];
            List<string> TileNames = new List<string>();
            string[] Rows = { "1", "2", "3", "4", "5", "6", "7", "8" };
            string[] Columns = { "A", "B", "C", "D", "E", "F", "G", "H" };
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    TileAddresses[i, j] = Columns[j] + Rows[i]; TileNames.Add(TileAddresses[i, j]);

                }
            }


            //fill board with initial pieces
            board.initialize();

            //return the index of the selectedpiece
            int[] indexOfPiece(string PieceSelect)
            {
                int[] array = new int[2];
                for (int i = 0; i <= 7; i++)
                {

                    for (int j = 0; j <= 7; j++)
                    {


                        if (board.BoardLayout[i, j] == PieceSelect) { array[0] = i; array[1] = j; }

                    }

                }
                return array;
            }

            //return the index of the selected board address
            int[] indexOfAddress(string AddressSelect)
            {
                int[] array = new int[2];
                for (int i = 0; i <= 7; i++)
                {

                    for (int j = 0; j <= 7; j++)
                    {


                        if (TileAddresses[i, j] == AddressSelect) { array[0] = i; array[1] = j; }

                    }

                }
                return array;
            }

            //start game
            while (true)
            {
                //if king is captured end the game
                if (board.deadpieces.Contains("K1") || board.deadpieces.Contains("k1"))
                {
                    Console.Clear();
                    board.Print();
                    break;
                }
                Console.WriteLine();

                board.Print();


                string SelectedPiece = "";
                string SelectedAddress = "";

                //index of the selected piece within the game board, and the index of the selected address within the tileaddresses board for comparison and move logic
                int[] PieceIndex;
                int[] AddressIndex;

                //selectpiece function
                void pieceselect()
                {
                    while (true)
                    {



                        Console.WriteLine("select piece to move");

                        SelectedPiece = Console.ReadLine();

                        //when you select a piece itll select the right one regardless of lowercase or capital 
                        if (board.turn % 2 == 0) { SelectedPiece = SelectedPiece.ToUpper(); }
                        if (board.turn % 2 != 0) { SelectedPiece = SelectedPiece.ToLower(); }

                        if (!board.pieces.Contains(SelectedPiece))
                        {
                            Console.Clear();
                            board.Print();
                            Console.WriteLine("Piece does not exist");
                        }

                        else if (board.deadpieces.Contains(SelectedPiece))
                        {
                            Console.Clear();
                            board.Print();
                            Console.WriteLine("Piece does not exist on the board");
                        }

                        else
                        {
                            PieceIndex = indexOfPiece(SelectedPiece);

                            ShowPossibleMoves(PieceIndex);

                            break;
                        }

                    }
                }

                //select piece
                pieceselect();

                //shows possible moves
                void ShowPossibleMoves(int[] Indexpiece)
                {
                    foreach (string i in TileAddresses)
                    {
                        int[] arr = indexOfAddress(i);
                        if (CheckMoves.IsValidMove(SelectedPiece, PieceIndex, arr, board.BoardLayout, board.pieces)) { possiblemoves.BoardLayout[arr[0], arr[1]] = "XX"; }
                        else possiblemoves.BoardLayout[arr[0], arr[1]] = board.BoardLayout[arr[0], arr[1]];







                    }
                    possiblemoves.turn = board.turn;
                    Console.Clear();
                    possiblemoves.Print();
                    Thread.Sleep(2000);


                    Console.Clear();
                    board.Print();


                }

                //move select, escapes when valid move is chosen 
                while (true)
                {

                    Console.Clear();
                    board.Print();

                    void picktilemessage()
                    {
                        if (SelectedPiece == SelectedPiece.ToUpper())
                        {
                            Console.Write("Selected Piece: ");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{SelectedPiece}");
                            Console.ResetColor();
                            Console.WriteLine("Pick a tile to move to or type 'back' to pick another piece");

                        }
                        else
                        {
                            Console.Write("Selected Piece: ");

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{SelectedPiece}");
                            Console.ResetColor();
                            Console.WriteLine("Pick a tile to move to or type 'back' to pick another piece");

                        }
                    }
                    picktilemessage();
                    SelectedAddress = Console.ReadLine();
                    SelectedAddress = SelectedAddress.ToUpper();
                    AddressIndex = indexOfAddress(SelectedAddress);


                    if (SelectedAddress == "BACK")
                    {
                        Console.Clear();
                        board.Print();
                        pieceselect();
                    }


                    else if (!TileNames.Contains(SelectedAddress))
                    {
                        Console.Clear();
                        board.Print();
                        Console.WriteLine("Please input correct tile address (Example: A5)");
                    }

                    else if (!CheckMoves.IsValidMove(SelectedPiece, PieceIndex, AddressIndex, board.BoardLayout, board.pieces))
                    {

                        Console.Clear();
                        board.Print();

                        Console.WriteLine("Invalid Move");

                    }

                    else { break; }

                }




                //move the selected piece
                for (int i = 0; i <= 7; i++)
                {

                    for (int j = 0; j <= 7; j++)
                    {


                        if (SelectedAddress == TileAddresses[i, j]) { if (board.BoardLayout[i, j] != "  ") { board.deadpieces.Add(board.BoardLayout[i, j]); } board.BoardLayout[i, j] = SelectedPiece; }
                        else if (SelectedPiece == board.BoardLayout[i, j]) { board.BoardLayout[i, j] = "  "; }

                    }






                }

                //print new board and change turns
                board.turn++;
                Console.Clear();



            }
        }
    }
}