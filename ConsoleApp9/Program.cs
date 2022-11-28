using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using WebSocketSharp;
namespace Chessapp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebSocket ws = new WebSocket("ws://localhost:5000");
            ws.Connect();
            //new game board
            Board board = new Board();
            //board to show possible moves

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
                ws.OnMessage += movereceived;


                void movereceived(object sender, MessageEventArgs e)
                    {


                    string data = e.Data;
                    string[,] newlayout = new string[8, 8];
                    data = data.Replace("-", "");
                    string result = string.Concat(Enumerable
                        .Range(0, data.Length / 2)
                        .Select(i => (char)int.Parse(data.Substring(2 * i, 2), NumberStyles.HexNumber)));

                    String[] strlist = result.Split(',');
                    int index = 1;

                    for (int i = 0; i <= 7; i++)
                        {


                        for (int j = 0; j <= 7; j++)

                            {
                            
                            
                                newlayout[i, j] = strlist[index];
                            
                            index++;

                            }

                        }

                         board.BoardLayout = newlayout;

                    board.turn = Convert.ToInt32(strlist[0]);
                    Console.Clear();

                    board.Print();

                }
                //if king is captured end the game
                if (board.deadpieces.Contains("K1") || board.deadpieces.Contains("k1"))
                {
                    Console.Clear();
                    board.Print();
                    break;
                }
                Console.WriteLine();

                Console.Clear();

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


                            break;
                        }

                    }
                }

                //select piece
                pieceselect();

                //shows possible moves
                void ShowPossibleMoves(int[] Indexpiece)
                {
                    Board possiblemoves = new Board();
                    possiblemoves.deadpieces = board.deadpieces;
                    List<object> potentialcaptures = new List<object>();
                    foreach (string i in TileAddresses)
                    {
                        int[] arr = indexOfAddress(i);
                        if (CheckMoves.IsValidMove(SelectedPiece, PieceIndex, arr, board.BoardLayout, board.pieces)) { possiblemoves.BoardLayout[arr[0], arr[1]] = "XX"; }
                        else possiblemoves.BoardLayout[arr[0], arr[1]] = board.BoardLayout[arr[0], arr[1]];
                        if (CheckMoves.IsValidMove(SelectedPiece, PieceIndex, arr, board.BoardLayout, board.pieces) && board.BoardLayout[arr[0],arr[1]]!="  ") { potentialcaptures.Add(arr); }


                    }
                    possiblemoves.turn = board.turn;
                    Console.Clear();
                    possiblemoves.Print();

                    if(potentialcaptures.Count > 0)
                    {
                        Thread.Sleep(1000);
                        foreach (int[] i in potentialcaptures)
                        {
                            possiblemoves.BoardLayout[i[0], i[1]] = board.BoardLayout[i[0], i[1]];
                        }
                        Console.Clear();

                        possiblemoves.Print();

                    }



                }

                //move select, escapes when valid move is chosen 
                while (true)
                {


                    ShowPossibleMoves(PieceIndex);

                    void picktilemessage()
                    {
                        if (SelectedPiece == SelectedPiece.ToUpper())
                        {
                            Console.Write("Selected Piece: ");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{SelectedPiece}");
                            Console.ResetColor();
                            Console.WriteLine("Pick a tile to move piece to by typing the address e.g 'a5' or 'A5' or type 'back' to pick another piece.");

                        }
                        else
                        {
                            Console.Write("Selected Piece: ");

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{SelectedPiece}");
                            Console.ResetColor();
                            Console.WriteLine("Pick a tile to move piece to by typing the address e.g 'a5' or 'A5' or type 'back' to pick another piece.");

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
                string message = Convert.ToString(board.turn);
                foreach (string tile in board.BoardLayout) {  message = message+ ',' + tile;  }
                ws.Send(message);



                
            }
        }
    }
}