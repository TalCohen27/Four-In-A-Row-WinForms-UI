using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowLogic
{
    public class ChangedTileStatusEventArgs : EventArgs
    {
        private readonly Board.eTileStatus r_NewTileStatus;
        private readonly int r_TileRow;
        private readonly int r_TileCol;

        public ChangedTileStatusEventArgs(Board.eTileStatus i_NewTileStatus, int i_TileRow, int i_TileCol)
        {
            r_NewTileStatus = i_NewTileStatus;
            r_TileRow = i_TileRow;
            r_TileCol = i_TileCol;
        }

        public Board.eTileStatus NewTileStatus
        {
            get { return r_NewTileStatus; }
        }

        public int TileRow
        {
            get { return r_TileRow; }
        }

        public int TileCol
        {
            get { return r_TileCol; }
        }
    }
}