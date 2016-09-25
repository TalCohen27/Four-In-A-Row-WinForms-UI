using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowLogic
{
    public class ColumnBecameFullEventArgs : EventArgs
    {
       private readonly int r_ColumnIndex;

        public ColumnBecameFullEventArgs(int i_ColumnIndex)
        {
            r_ColumnIndex = i_ColumnIndex;
        }

        public int ColumnIndex
        {
            get { return r_ColumnIndex; }
        }
    }
}
