using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowLogic
{
    public class Board
    {
        public enum eTileStatus
        {
            Empty,
            PlayerA,
            PlayerB,
        }

        private readonly eTileStatus[,] r_Matrix;
        private readonly int[] r_NumOfTokensInColumn;
        private readonly int r_NumRows;
        private readonly int r_NumCols;

        public int NumOfRows
        {
            get { return r_NumRows; }
        }

        public int NumOfCols
        {
            get { return r_NumCols; }    
        }

        public Board(int i_NumRows, int i_NumCols)
        {
            r_Matrix = new eTileStatus[i_NumRows, i_NumCols];
            r_NumOfTokensInColumn = new int[i_NumCols];
            r_NumCols = i_NumCols;
            r_NumRows = i_NumRows;
        }

        public eTileStatus CheckTileStatus(int i_Row, int i_Col)
        {
            return r_Matrix[i_Row, i_Col];
        }

        public void SetTileStatus(int i_Row, int i_Col, eTileStatus i_NewStatus)
        {
            r_Matrix[i_Row, i_Col] = i_NewStatus;
        }

        public int GetNumOfTokensInCol(int i_Col)
        {
            return r_NumOfTokensInColumn[i_Col];
        }

        public void SetNumOfTokensInCol(int i_Col, int i_Value)
        {
            r_NumOfTokensInColumn[i_Col] = i_Value;
        }

        public bool IsThePlayerTokenOnTile(int i_row, int i_Column, eTileStatus i_player)
        {
            bool playerIsOnTile = false;

            if (TileIsValid(i_row, i_Column))
            {
                playerIsOnTile = CheckTileStatus(i_row, i_Column) == i_player;
            }

            return playerIsOnTile;
        }

        public bool TileIsValid(int i_Row, int i_Colomn)
        {
            bool isTileValid;

            isTileValid = i_Row < r_NumRows && i_Colomn < r_NumCols && i_Row >= 0 && i_Colomn >= 0;

            return isTileValid;
        }

        public bool IsBoardFull()
        {
            bool isBoardFull = true;

            for (int i = 0; i < r_NumCols && isBoardFull; i++)
            {
                isBoardFull = r_Matrix[0, i] != Board.eTileStatus.Empty;
            }

            return isBoardFull;
        }

        internal void Reset()
        {
            for (int i = 0; i < r_NumRows; i++)
            {
                for (int j = 0; j < r_NumCols; j++)
                {
                    r_Matrix[i, j] = eTileStatus.Empty;
                }
            }
          
            for (int i = 0; i < r_NumCols; i++)
            {
                r_NumOfTokensInColumn[i] = 0;
            }        
        }
    }
}

