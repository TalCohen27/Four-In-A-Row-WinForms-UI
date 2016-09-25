using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FourInARowUI
{
    class ButtonSlot : Button
    {
        public ButtonSlot(int i_Row, int i_Col)
        {
            Name = i_Row.ToString() + i_Col.ToString();     
        }
    }
}
