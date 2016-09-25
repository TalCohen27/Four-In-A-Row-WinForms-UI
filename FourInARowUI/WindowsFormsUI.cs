using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FourInARowUI
{
    public class WindowsFormsUI
    {
        private readonly SettingsWindow r_SettingsWindow = new SettingsWindow();
      
        public void Start()
        {
            r_SettingsWindow.ShowDialog();
            r_SettingsWindow.Close();           
        }
    }    
}
