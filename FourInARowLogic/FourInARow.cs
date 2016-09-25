using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowLogic
{
    public class FourInARow
    {
        public delegate void ChangedTileStatusEventHandler(object sender, ChangedTileStatusEventArgs e);
        public delegate void ColumnBecameFullEventHandler(object sender, ColumnBecameFullEventArgs e);
        public delegate void TurnEndedEventHandler(object sender);
        public delegate void GameEndedEventHandler(object sender, GameEndedEventArgs e);

        public enum eTileStatus
        {
            Empty,
            PlayerA,
            PlayerB,
        }

        public enum eGameMode
        {
            Solo,
            TwoPlayers
        }

        public enum eEndOfGameReason
        {
            Win,
            Tie
        }

        public event ColumnBecameFullEventHandler ColumnBecameFull;
        public event ChangedTileStatusEventHandler ChangedTileStatus;
        public event TurnEndedEventHandler TurnEnded;
        public event GameEndedEventHandler GameEnded;

        public const int k_MaxDimention = 8;
        public const int k_MinDimention = 4;
        private const int k_NumOfPlayers = 2;
        private const int k_FirstPlayer = 0;
        private const int k_SecondPlayer = 1;
        private const int k_FirstIndex = 0;
        private const int k_HowManyInARowToWin = 4;

        private readonly Random r_RandomColNumber = new Random();
        private readonly Player[] r_Players = new Player[k_NumOfPlayers] { new Player(), new Player() };
        private string m_Winner = null;
        private eGameMode? m_Mode = null;
        private Board m_GameBoard;
        private bool m_AskForAnotherRound;
        private bool m_EndOfGame = false;
        private int m_TurnsCounter;

        public FourInARow()
        {
            r_Players[k_FirstPlayer].Type = Player.ePlayerType.Human;
            r_Players[k_FirstPlayer].Order = Board.eTileStatus.PlayerA;
            r_Players[k_SecondPlayer].Order = Board.eTileStatus.PlayerB;
        }

        public int BoardNumRows
        {
            get { return m_GameBoard.NumOfRows; }
        }

        public int BoardNumCols
        {
            get { return m_GameBoard.NumOfCols; }
        }
        
        public eGameMode? Mode
        {
            get { return m_Mode; }
            set
            {
                m_Mode = value;
                setPlayersTypes();
            }
        }

        public Board.eTileStatus GetTileStatus(int i_Row, int i_Col)
        {
            return m_GameBoard.CheckTileStatus(i_Row, i_Col);
        }

        public bool AttemptAMove(int i_ColIndex)
        {
            bool columnIsFree = false;
            Player.ePlayerType playerType = CurrentPlayerType;

            if (playerType == Player.ePlayerType.Computer)
            {
                i_ColIndex =
                    r_RandomColNumber.Next(k_FirstIndex, m_GameBoard.NumOfCols);
            }

            columnIsFree = m_GameBoard.CheckTileStatus(k_FirstIndex, i_ColIndex) == Board.eTileStatus.Empty;

            if (columnIsFree)
            {
                makeAMove(i_ColIndex);
            }

            columnIsFree = m_GameBoard.CheckTileStatus(k_FirstIndex, i_ColIndex) == Board.eTileStatus.Empty;
            if (!columnIsFree && !IsGameBoardFull())
            {
                OnColumnBecameFull(i_ColIndex, new ColumnBecameFullEventArgs(i_ColIndex));
            }

            return columnIsFree;
        }

        private void makeAMove(int i_ColIndex)
        {
            bool isWin = false;
            int numOfTokensInCol = m_GameBoard.GetNumOfTokensInCol(i_ColIndex);
            int RowIndex = m_GameBoard.NumOfRows - numOfTokensInCol - 1;
            Board.eTileStatus playerOrder = CurrentPlayerOrder;

            m_GameBoard.SetTileStatus(RowIndex, i_ColIndex, playerOrder);
            OnChangedTileStatus(this, new ChangedTileStatusEventArgs(playerOrder, RowIndex, i_ColIndex));
            m_AskForAnotherRound = isWin = doWeHaveAWinner(RowIndex, i_ColIndex);
            numOfTokensInCol++;
            m_GameBoard.SetNumOfTokensInCol(i_ColIndex, numOfTokensInCol);

            if (isWin || m_GameBoard.IsBoardFull())
            {
                if (!isWin)
                {
                    m_AskForAnotherRound = m_GameBoard.IsBoardFull();
                    OnGameEnded(this, new GameEndedEventArgs());
                }
                else
                {
                    r_Players[(int)playerOrder - 1].Score++;
                    m_Winner = r_Players[(int)playerOrder - 1].Name;
                    OnGameEnded(this, new GameEndedEventArgs(r_Players[m_TurnsCounter % k_NumOfPlayers]));
                }

                m_EndOfGame = true;
            }
            else
            {
                m_TurnsCounter++;
                OnTurnEnded(this);
            }
        }

        public void InitializeBoard(int i_Rows, int i_Cols)
        {
            m_GameBoard = new Board(i_Rows, i_Cols);
        }

        public void StartNewRound()
        {
            m_GameBoard.Reset();
            m_EndOfGame = false;
            m_AskForAnotherRound = false;
            m_Winner = null;
            m_TurnsCounter = 0;
        }

        public void QuitRequest()
        {
            m_AskForAnotherRound = true;
        }

        public bool AskForAnotherRound
        {
            get { return m_AskForAnotherRound; }
            set { m_AskForAnotherRound = value; }
        }

        public bool CheckIfValidDimentions(int i_NumRows, int i_NumCols)
        {
            return i_NumRows >= k_MinDimention && i_NumCols <= k_MaxDimention &&
               i_NumRows >= k_MinDimention && i_NumRows <= k_MaxDimention;
        }

        public bool CheckIfValidColNum(int i_ColNum)
        {
            return i_ColNum >= 1 && i_ColNum <= m_GameBoard.NumOfCols;
        }

        private bool doWeHaveAWinner(int i_LastMoveRow, int i_LastMoveColumn)
        {
            int checkingRow = i_LastMoveRow;
            int checkingColumn = i_LastMoveColumn;
            int numOfTilesToSkip = k_HowManyInARowToWin - 1;
            const int k_NegOffset = -1, k_PosOffset = 1, k_NeutOffset = 0;
            bool winnerFound = false;

            // vertical
            checkingRow += numOfTilesToSkip;
            winnerFound = lookForWinningSequence(checkingRow, checkingColumn, k_NegOffset, k_NeutOffset);

            // horizontal
            if (!winnerFound)
            {
                checkingRow = i_LastMoveRow;
                checkingColumn = i_LastMoveColumn - numOfTilesToSkip;
                winnerFound = lookForWinningSequence(checkingRow, checkingColumn, k_NeutOffset, k_PosOffset);
            }

            // left to right diagonal
            if (!winnerFound)
            {
                checkingColumn = i_LastMoveColumn - numOfTilesToSkip;
                checkingRow = i_LastMoveRow + numOfTilesToSkip;
                winnerFound = lookForWinningSequence(checkingRow, checkingColumn, k_NegOffset, k_PosOffset);
            }

            // right to left diagonal
            if (!winnerFound)
            {
                checkingColumn = i_LastMoveColumn + numOfTilesToSkip;
                checkingRow = i_LastMoveRow + numOfTilesToSkip;
                winnerFound = lookForWinningSequence(checkingRow, checkingColumn, k_NegOffset, k_NegOffset);
            }

            return winnerFound;
        }

        private bool lookForWinningSequence(int i_CheckingRow, int i_CheckingColumn, int i_RowOffset, int i_ColOffset)
        {
            bool sequenceFound = false;
            int checkRange = (k_HowManyInARowToWin * 2) - 1;
            int playerTokensCounter = 0;
            Board.eTileStatus playerOrder = CurrentPlayerOrder;

            for (int i = 0; (i < checkRange) && (!sequenceFound); i++)
            {
                if (m_GameBoard.IsThePlayerTokenOnTile(i_CheckingRow, i_CheckingColumn, playerOrder))
                {
                    playerTokensCounter++;
                    sequenceFound = playerTokensCounter == k_HowManyInARowToWin;
                }
                else
                {
                    playerTokensCounter = 0;
                }

                i_CheckingRow += i_RowOffset;
                i_CheckingColumn += i_ColOffset;
            }

            return sequenceFound;
        }

        private void setPlayersTypes()
        {
            switch (m_Mode)
            {
                case eGameMode.TwoPlayers: r_Players[k_SecondPlayer].Type = Player.ePlayerType.Human;
                    break;
                case eGameMode.Solo: r_Players[k_SecondPlayer].Type = Player.ePlayerType.Computer;
                    break;
            }
        }

        public string Winner
        {
            get { return m_Winner; }
        }

        public int[] PlayersScore
        {
            get
            {
                int[] playersScore = new int[k_NumOfPlayers];

                playersScore[k_FirstPlayer] = r_Players[k_FirstPlayer].Score;
                playersScore[k_SecondPlayer] = r_Players[k_SecondPlayer].Score;

                return playersScore;
            }
        }

        public string[] PlayersNames
        {
            set
            {
                r_Players[0].Name = (string)value.GetValue(0);
                r_Players[1].Name = (string)value.GetValue(1);
            }
            get
            {
                string[] playersNames = new string[k_NumOfPlayers];
                playersNames[k_FirstPlayer] = r_Players[k_FirstPlayer].Name;
                playersNames[k_SecondPlayer] = r_Players[k_SecondPlayer].Name;
                return playersNames;
            }
        }

        public Board.eTileStatus CurrentPlayerOrder
        {
            get { return r_Players[m_TurnsCounter % k_NumOfPlayers].Order; }
        }

        public Player.ePlayerType CurrentPlayerType
        {
            get { return r_Players[m_TurnsCounter % k_NumOfPlayers].Type; }
        }

        public bool IsGameBoardFull()
        {
            return m_GameBoard.IsBoardFull();
        }

        protected virtual void OnChangedTileStatus(object sender, ChangedTileStatusEventArgs e)
        {
            if (ChangedTileStatus != null)
            {
                ChangedTileStatus(this, e);
            }
        }

        protected virtual void OnColumnBecameFull(object sender, ColumnBecameFullEventArgs e)
        {
            if (ColumnBecameFull != null)
            {
                ColumnBecameFull(sender, e);
            }
        }

        protected virtual void OnTurnEnded(object sender)
        {
            if (TurnEnded != null)
            {
                TurnEnded(sender);
            }
        }

        protected virtual void OnGameEnded(object sender, GameEndedEventArgs e)
        {
            if (GameEnded != null)
            {
                GameEnded(sender, e);
            }
        }
    }
}
