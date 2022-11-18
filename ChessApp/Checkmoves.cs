using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChessAppRewrite
{
    public class CheckMoves
    {
        public static bool IsValidMove(string select, int[] selectindex, int[] tileindex, string[,] BoardLayout, List<string> pieces)
        {

            if (select.Contains('P') || select.Contains('p'))
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];
                int selectj = selectindex[1];
                int tilej = tileindex[1];

                char player = BoardLayout[selecti, selectj][0];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (char.ToUpper(player) == player)
                {
                    if (selecti - tilei == 2 && selecti == 6 && selectj - tilej == 0 && BoardLayout[tilei, tilej].Contains(' ') && BoardLayout[tilei + 1, tilej].Contains(' '))
                    {
                        return true;
                    }
                    if (selecti - tilei == 1 && tilej == selectj && BoardLayout[tilei, tilej].Contains(' ')) { return true; }
                    if (selecti - tilei == 1 && Math.Abs(selectj - tilej) == 1 && pieces.Contains(BoardLayout[tilei, tilej])) { return true; }
                    else return false;

                }
                else
                {
                    if (selecti - tilei == -2 && selecti == 1 && selectj - tilej == 0 && BoardLayout[tilei, tilej].Contains(' ') && BoardLayout[tilei - 1, tilej].Contains(' '))
                    {
                        return true;
                    }
                    if (selecti - tilei == -2 && selecti == 1 && selectj - tilej == 0 && BoardLayout[tilei, tilej].Contains(' ') && BoardLayout[tilei - 1, tilej].Contains('X'))
                    {
                        return true;
                    }
                    if (selecti - tilei == -1 && tilej == selectj && BoardLayout[tilei, tilej].Contains(' ')) { return true; }
                    if (selecti - tilei == -1 && Math.Abs(selectj - tilej) == 1 && pieces.Contains(BoardLayout[tilei, tilej])) { return true; }
                    else return false;
                }
            }
            else if (select.Contains('q') || select.Contains('Q'))
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                int diff = Math.Abs(selecti - tilei) - Math.Abs(selectj - tilej);
                if (!(Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej)) && Math.Abs(diff) != Math.Abs(selecti - tilei) && Math.Abs(diff) != Math.Abs(selectj - tilej)) { return false; }
                if (Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))
                {
                    for (int i = selecti, j = selectj; j > 0 || j < 7 || i > 0 || i < 7;)
                    {
                        if (i == tilei && j == tilej) { break; }
                        if (selecti > tilei) { i--; }
                        else { i++; }
                        if (selectj > tilej) { j--; }
                        else { j++; }
                        if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[i, j] != BoardLayout[tilei, tilej]) { return false; }
                    }
                    return true;
                }
                else if (Math.Abs(diff) == Math.Abs(selecti - tilei) || Math.Abs(diff) == Math.Abs(selectj - tilej))
                {
                    if (0 != Math.Abs(selecti - tilei) && 0 != Math.Abs(selectj - tilej)) { return false; }
                    if (Math.Abs(selecti - tilei) != 0)
                    {
                        for (int i = selecti, j = selectj; i > 0 || i < 7;)
                        {
                            if (i == tilei) { break; }

                            if (selecti > tilei) { i--; }
                            else { i++; }


                            if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[i, tilej] != BoardLayout[tilei, tilej]) { return false; }
                        }
                    }
                    if (Math.Abs(selectj - tilej) != 0)
                    {
                        for (int i = selecti, j = selectj; j > 0 || j < 7;)
                        {
                            if (j == tilej) { break; }
                            if (selectj > tilej) { j--; }
                            else { j++; }
                            if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[tilei, j] != BoardLayout[tilei, tilej]) { return false; }
                        }
                    }
                    return true;
                }
                else return false;
            }
            else if (select.Contains('n') || select.Contains('N'))
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];

                char player = BoardLayout[selecti, selectj][0];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))
                {
                    return false;
                }
                if (Math.Abs(selecti - tilei) != 1 && Math.Abs(selectj - tilej) != 1) { return false; }
                if (Math.Abs(selectj - tilej) != 2 && Math.Abs(selecti - tilei) != 2) { return false; }
                return true;
            }
            else if (select.Contains('B') || select.Contains('b'))
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (!(Math.Abs(selecti - tilei) == Math.Abs(selectj - tilej))) { return false; }
                for (int i = selecti, j = selectj; j > 0 || j < 7 || i > 0 || i < 7;)
                {
                    if (i == tilei && j == tilej) { break; }
                    if (selecti > tilei) { i--; }
                    else { i++; }
                    if (selectj > tilej) { j--; }
                    else { j++; }
                    if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[i, j] != BoardLayout[tilei, tilej]) { return false; }
                }
                return true;
            }
            else if (select.Contains('r') || select.Contains('R'))
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                int diff = Math.Abs(selecti - tilei) - Math.Abs(selectj - tilej);
                if (Math.Abs(diff) != Math.Abs(selecti - tilei) && Math.Abs(diff) != Math.Abs(selectj - tilej)) { return false; }
                if (0 != Math.Abs(selecti - tilei) && 0 != Math.Abs(selectj - tilej)) { return false; }
                if (Math.Abs(selecti - tilei) != 0)
                {
                    for (int i = selecti, j = selectj; i > 0 || i < 7;)
                    {
                        if (i == tilei) { break; }
                        if (selecti > tilei) { i--; }
                        else { i++; }
                        if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[i, tilej] != BoardLayout[tilei, tilej]) { return false; }
                    }
                }
                if (Math.Abs(selectj - tilej) != 0)
                {
                    for (int i = selecti, j = selectj; j > 0 || j < 7;)
                    {
                        if (j == tilej) { break; }
                        if (selectj > tilej) { j--; }
                        else { j++; }
                        if (pieces.Contains(BoardLayout[i, j]) && BoardLayout[tilei, j] != BoardLayout[tilei, tilej]) { return false; }
                    }
                }
                return true;
            }
            else  
            {
                int selecti = selectindex[0];
                int tilei = tileindex[0];

                int selectj = selectindex[1];
                int tilej = tileindex[1];
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToUpper() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToUpper() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (BoardLayout[tilei, tilej] == BoardLayout[tilei, tilej].ToLower() && BoardLayout[selecti, selectj] == BoardLayout[selecti, selectj].ToLower() && !BoardLayout[tilei, tilej].Contains(' ')) { return false; }
                if (Math.Abs(selecti - tilei) != 1 && Math.Abs(selectj - tilej) != 1) { return false; }
                else if (Math.Abs(selecti - tilei) != 1 && Math.Abs(selecti - tilei) != 0) { return false; }
                else if (Math.Abs(selectj - tilej) != 0 && Math.Abs(selectj - tilej) != 1) { return false; }
                else return true;
            }






        }


 






    }
}