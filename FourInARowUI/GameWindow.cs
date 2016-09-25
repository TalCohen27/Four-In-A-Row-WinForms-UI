using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms; 
using FourInARowLogic;
using System.Drawing;

namespace FourInARowUI
{
    class GameWindow : Form
    {
        private readonly FourInARow r_GameInstance = new FourInARow();
        private const int k_SpaceBetweenButtons = 45;
        private const int k_ColumnButtonsPos = 15;
        private const int k_LeftMargin = 7;
        private Label label1;
        private Label label2;

        public GameWindow()
        {
            this.InitializeComponent();
        }

        public void Initialize(int i_NumRows, int i_NumCols, bool i_IsTwoPlayers, params string[] i_PlayersNames)
        {
            r_GameInstance.ChangedTileStatus +=
                new FourInARow.ChangedTileStatusEventHandler(r_GameInstance_ChangedTileStatus);
            r_GameInstance.ColumnBecameFull +=
                new FourInARow.ColumnBecameFullEventHandler(r_GameInstance_ColumnBecameFull);
            r_GameInstance.TurnEnded += new FourInARow.TurnEndedEventHandler(r_GameInstance_TurnEnded);

            r_GameInstance.GameEnded += new FourInARow.GameEndedEventHandler(r_GameInstance_GameEnded);

            r_GameInstance.InitializeBoard(i_NumRows, i_NumCols);
            if (i_IsTwoPlayers)
            {
                label2.Text = i_PlayersNames[1] + ": 0";
                r_GameInstance.Mode = FourInARow.eGameMode.TwoPlayers;
            }
            else
            {
                label2.Text = "Computer: 0";
                r_GameInstance.Mode = FourInARow.eGameMode.Solo;
                i_PlayersNames[1] = "Computer";
            }

            label1.Text = i_PlayersNames[0] + ": 0";
            r_GameInstance.PlayersNames = i_PlayersNames;
            initButtonMatrix();
            this.ClientSize =
             new System.Drawing.Size(
                 Controls[Controls.Count - 1].Location.X + k_SpaceBetweenButtons,
                 Controls[Controls.Count - 1].Location.Y + k_SpaceBetweenButtons);

            int bottomScreen = Controls[Controls.Count - 1].Location.Y + k_SpaceBetweenButtons;
            Point labelALoc = new Point(label1.Location.X, bottomScreen);
            Point labelBLoc = new Point(label2.Location.X, bottomScreen);
            label1.Location = labelALoc;
            label2.Location = labelBLoc;
        }

        void r_GameInstance_GameEnded(object sender, GameEndedEventArgs e)
        {
            string endOfGameMsg = null;
            string caption = null;
            updateScoreStats();

            switch (e.EndOfGameReason)
            {
                case FourInARow.eEndOfGameReason.Tie: endOfGameMsg = string.Format(
@"Tie!!
Another Round?");
                    caption = "A Tie!";
                    break;
                case FourInARow.eEndOfGameReason.Win: endOfGameMsg = string.Format(
@"{0} Won!!
Another Round?",
e.Winner.Name);
                    caption = "A Win!";
                    break;
            }

            handleEndOfRoundMessageBox(endOfGameMsg, caption);
        }

        void r_GameInstance_TurnEnded(object sender)
        {
            Player.ePlayerType CurrPlayerType = r_GameInstance.CurrentPlayerType;

            switch (CurrPlayerType)
            {
                case Player.ePlayerType.Human: this.Enabled = true;
                    break;
                case Player.ePlayerType.Computer: this.Enabled = false;
                    r_GameInstance.AttemptAMove(int.MinValue);
                    break;
            }
        }

        void r_GameInstance_ColumnBecameFull(object sender, ColumnBecameFullEventArgs e)
        {
            Button columnButton = (Button)Controls[Controls.IndexOfKey(e.ColumnIndex.ToString())];
            columnButton.Enabled = false;

            if (r_GameInstance.CurrentPlayerType == Player.ePlayerType.Computer)
            {
                r_GameInstance.AttemptAMove(int.MinValue);
            }
        }

        private void refreshWindow()
        {
            foreach (Control control in Controls)
            {
                control.Enabled = true;
                if (control is Button)
                {
                    control.ResetText();
                }
            }

            this.Enabled = true;
        }

        private void handleEndOfRoundMessageBox(string i_Text, string i_Caption)
        {
            DialogResult dialogResult = MessageBox.Show(
              this,
              i_Text,
              i_Caption,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Information,
              MessageBoxDefaultButton.Button1);

            switch (dialogResult)
            {
                case DialogResult.No: this.Close();                  
                    break;
                case DialogResult.Yes: r_GameInstance.StartNewRound();
                    refreshWindow();
                    break;
            }
        }

        private void updateScoreStats()
        {
            string[] names = r_GameInstance.PlayersNames;
            int[] scores = r_GameInstance.PlayersScore;
            label1.Text = string.Format("{0}: {1}", names[0], scores[0]);
            label2.Text = string.Format("{0}: {1}", names[1], scores[1]);
        }

        void r_GameInstance_ChangedTileStatus(object sender, ChangedTileStatusEventArgs e)
        {
            int tileRow = e.TileRow;
            int tileCol = e.TileCol;
            Board.eTileStatus newStatus = e.NewTileStatus;          
            Button buttonSlot = (Button)Controls[Controls.IndexOfKey(tileRow.ToString() + tileCol.ToString())];

            switch(newStatus)
            {
                case Board.eTileStatus.PlayerA: buttonSlot.Text = "O";
                    break;
                case Board.eTileStatus.PlayerB: buttonSlot.Text = "X";
                    break;
            }
        }

        private void initButtonMatrix()
        {
            Size colButtonSize = new Size(37, 18);
            Size slotButtonSize = new Size(37, 32);
            System.Drawing.Point currentPoint = new System.Drawing.Point();

            currentPoint.Y = this.Location.Y + k_ColumnButtonsPos;
            for (int i = 0; i < r_GameInstance.BoardNumCols; i++)
            {
                currentPoint.X = i * k_SpaceBetweenButtons + k_LeftMargin;
                ButtonColumn newColButton = new ButtonColumn(i + 1);
                newColButton.Location = currentPoint;
                newColButton.Click += new EventHandler(colButton_Click);
                newColButton.Size = colButtonSize;
                newColButton.FlatStyle = FlatStyle.Popup;
                Controls.Add(newColButton);
            }

            for (int i = 0; i < r_GameInstance.BoardNumRows; i++)
            {
                for (int j = 0; j < r_GameInstance.BoardNumCols; j++)
                {
                    currentPoint.X = j * k_SpaceBetweenButtons + k_LeftMargin;
                    currentPoint.Y = (i + 1) * k_SpaceBetweenButtons;
                    ButtonSlot newSlotButton = new ButtonSlot(i, j);
                    newSlotButton.Location = currentPoint;
                    newSlotButton.Size = slotButtonSize;
                    newSlotButton.FlatStyle = FlatStyle.Flat;
                    Controls.Add(newSlotButton);
                }
            }
        }

        void colButton_Click(object sender, EventArgs e)
        {
            Button chosenButton = sender as Button;
            int chosenCol = int.Parse(chosenButton.Text);
            bool isColNotFull = r_GameInstance.AttemptAMove(--chosenCol);
        }

        public void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player2:";
            // 
            // GameWindow
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(297, 259);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GameWindow";
            this.Text = "4 in a Raw !!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}