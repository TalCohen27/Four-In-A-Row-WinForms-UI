using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FourInARowUI
{
    class ButtonColumn : Button
    {
        public ButtonColumn(int i_ColNumber)         
        {
            Text = i_ColNumber.ToString();
            Name = (i_ColNumber - 1).ToString();
        }

        public sealed override void ResetText()
        {
        }
    }
}
