using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FourInARowUI
{
    class SettingsWindow : Form
    {
        private NumericUpDown m_UpAndDownRows;
        private NumericUpDown m_UpAndDownCols;
        private CheckBox m_CheckBoxGameMode;
        private Button m_ButtonStart;
        private TextBox m_TextBoxPlayer2;
        private TextBox m_TextBoxPlayer1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private readonly GameWindow r_GameWindow = new GameWindow();

        public SettingsWindow()
        {
            this.InitializeComponent();
        }

        public void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.m_UpAndDownRows = new System.Windows.Forms.NumericUpDown();
            this.m_UpAndDownCols = new System.Windows.Forms.NumericUpDown();
            this.m_CheckBoxGameMode = new System.Windows.Forms.CheckBox();
            this.m_ButtonStart = new System.Windows.Forms.Button();
            this.m_TextBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.m_TextBoxPlayer1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_UpAndDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_UpAndDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player 1:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Player2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Board Size:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Rows:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Cols:";
            // 
            // m_UpAndDownRows
            // 
            this.m_UpAndDownRows.Location = new System.Drawing.Point(82, 159);
            this.m_UpAndDownRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_UpAndDownRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.m_UpAndDownRows.Name = "m_UpAndDownRows";
            this.m_UpAndDownRows.Size = new System.Drawing.Size(35, 20);
            this.m_UpAndDownRows.TabIndex = 7;
            this.m_UpAndDownRows.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // m_UpAndDownCols
            // 
            this.m_UpAndDownCols.Location = new System.Drawing.Point(189, 159);
            this.m_UpAndDownCols.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_UpAndDownCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.m_UpAndDownCols.Name = "m_UpAndDownCols";
            this.m_UpAndDownCols.Size = new System.Drawing.Size(35, 20);
            this.m_UpAndDownCols.TabIndex = 8;
            this.m_UpAndDownCols.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // m_CheckBoxGameMode
            // 
            this.m_CheckBoxGameMode.AutoSize = true;
            this.m_CheckBoxGameMode.Location = new System.Drawing.Point(29, 82);
            this.m_CheckBoxGameMode.Name = "m_CheckBoxGameMode";
            this.m_CheckBoxGameMode.Size = new System.Drawing.Size(15, 14);
            this.m_CheckBoxGameMode.TabIndex = 9;
            this.m_CheckBoxGameMode.UseVisualStyleBackColor = true;
            this.m_CheckBoxGameMode.CheckStateChanged += new System.EventHandler(this.m_CheckBoxGameMode_CheckStateChanged);
            // 
            // m_ButtonStart
            // 
            this.m_ButtonStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_ButtonStart.Location = new System.Drawing.Point(59, 192);
            this.m_ButtonStart.Name = "m_ButtonStart";
            this.m_ButtonStart.Size = new System.Drawing.Size(165, 23);
            this.m_ButtonStart.TabIndex = 11;
            this.m_ButtonStart.Text = "Start!";
            this.m_ButtonStart.UseVisualStyleBackColor = false;
            this.m_ButtonStart.Click += new System.EventHandler(this.m_ButtonStart_Click);
            // 
            // m_TextBoxPlayer2
            // 
            this.m_TextBoxPlayer2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_TextBoxPlayer2.Enabled = false;
            this.m_TextBoxPlayer2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.m_TextBoxPlayer2.Location = new System.Drawing.Point(104, 76);
            this.m_TextBoxPlayer2.Name = "m_TextBoxPlayer2";
            this.m_TextBoxPlayer2.Size = new System.Drawing.Size(100, 20);
            this.m_TextBoxPlayer2.TabIndex = 12;
            this.m_TextBoxPlayer2.Text = "[Computer]";
            this.m_TextBoxPlayer2.EnabledChanged += new System.EventHandler(this.r_TextBoxPlayer2_EnabledChanged);
            // 
            // m_TextBoxPlayer1
            // 
            this.m_TextBoxPlayer1.Location = new System.Drawing.Point(104, 42);
            this.m_TextBoxPlayer1.Name = "m_TextBoxPlayer1";
            this.m_TextBoxPlayer1.Size = new System.Drawing.Size(100, 20);
            this.m_TextBoxPlayer1.TabIndex = 13;
            // 
            // SettingsWindow
            // 
            this.AcceptButton = this.m_ButtonStart;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(248, 229);
            this.Controls.Add(this.m_TextBoxPlayer1);
            this.Controls.Add(this.m_TextBoxPlayer2);
            this.Controls.Add(this.m_ButtonStart);
            this.Controls.Add(this.m_CheckBoxGameMode);
            this.Controls.Add(this.m_UpAndDownCols);
            this.Controls.Add(this.m_UpAndDownRows);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            ((System.ComponentModel.ISupportInitialize)(this.m_UpAndDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_UpAndDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void m_CheckBoxGameMode_CheckStateChanged(object sender, EventArgs e)
        {
            m_TextBoxPlayer2.Enabled = ((sender as CheckBox).Checked);
        }

        private void r_TextBoxPlayer2_EnabledChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Enabled)
            {
                this.m_TextBoxPlayer2.BackColor = SystemColors.Window;
                this.m_TextBoxPlayer2.Enabled = true;
                this.m_TextBoxPlayer2.ForeColor = SystemColors.ControlText;
                this.m_TextBoxPlayer2.Text = "";
            }
            else
            {
                this.m_TextBoxPlayer2.BackColor = SystemColors.ButtonFace;
                this.m_TextBoxPlayer2.Enabled = false;
                this.m_TextBoxPlayer2.ForeColor = SystemColors.GrayText;
                this.m_TextBoxPlayer2.Text = "[Computer]";
            }
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            bool isTwoPlayersChecked = m_CheckBoxGameMode.Checked;

            r_GameWindow.Initialize(
            (int)m_UpAndDownRows.Value,
            (int)m_UpAndDownCols.Value,
            isTwoPlayersChecked,
            m_TextBoxPlayer1.Text,
            m_TextBoxPlayer2.Text);

            this.Hide();
            r_GameWindow.ShowDialog();
            r_GameWindow.Close();
        }
    }
}
