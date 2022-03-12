using System;
using BowlingScoreBoard.Printers;

namespace BowlingScoreBoard
{
    public class ScoreBoard
    {
        private readonly IPrettyPrinter _prettyPrinter;
        public Frame FirstFrame { get; }
        public Frame ActualFrame { get; private set; }

        public ScoreBoard(IPrettyPrinter prettyPrinter)
        {
            _prettyPrinter = prettyPrinter;
            FirstFrame = new Frame();
            ActualFrame = FirstFrame;
        }

        public void EnterKnockedDownPins(int i)
        {
            if (GameIsFinished())
                throw new InvalidOperationException("You cannot enter more scores - Game is finished");

            if (ActualFrame.FrameIsFull())
                ActualFrame = ActualFrame.CreateNextFrame();

            ActualFrame.EnterKnockedDownPins(i);
        }

        public bool GameIsFinished()
        {
            if (ActualFrame.FrameNumber < 10)
                return false;

            if (!ActualFrame.SecondRoll.HasValue)
                return false;

            if (ActualFrame.FrameHasASpare && !ActualFrame.BonusRoll.HasValue)
                return false;

            if (ActualFrame.FrameHasAStrike && !ActualFrame.BonusRoll.HasValue)
                return false;

            return true;
        }

        public string PrintScoreBoard()
        {
            return _prettyPrinter.Print(this);
        }
    }
}