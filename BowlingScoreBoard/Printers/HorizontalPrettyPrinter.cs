using System;

namespace BowlingScoreBoard.Printers
{
    public class HorizontalPrettyPrinter : IPrettyPrinter
    {
        private const int LengthOfFirstColumn = 6;
        private const int LengthOfSecondColumn = 6;
        private const int LengthOfThirdColumn = 9;
        private const int LengthOfFourthColumn = 6;
        private const int LengthOfFifthColumn = 30;
        
        public string Print(ScoreBoard scoreBoard)
        {
            string result = string.Empty;

            result = PrintDividingLine() + PrintHeader() + PrintDividingLine();
            
            var printingFrame = scoreBoard.FirstFrame;

            while (printingFrame is not null)
            {
                result += PrintFrame(printingFrame) + PrintDividingLine();
                printingFrame = printingFrame.NextFrame;
            }

            return result;
        }

        private string PrintHeader()
        {
            var header1 =
                CreateStringWithTrailingSpaces("Frame", LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("Roll", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces("Knocked", LengthOfThirdColumn) +
                CreateStringWithTrailingSpaces("Total", LengthOfFourthColumn) + 
                CreateStringWithTrailingSpaces("Notes", LengthOfFifthColumn);
            
            var header2 =
                CreateStringWithTrailingSpaces("", LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces("down", LengthOfThirdColumn) +
                CreateStringWithTrailingSpaces("score", LengthOfFourthColumn) +
                CreateStringWithTrailingSpaces("", LengthOfFifthColumn);
            
            var header3 =
                CreateStringWithTrailingSpaces("", LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces("pins", LengthOfThirdColumn) +
                CreateStringWithTrailingSpaces("", LengthOfFourthColumn) + 
                CreateStringWithTrailingSpaces("", LengthOfFifthColumn);;

            return header1 + Environment.NewLine + header2 + Environment.NewLine + header3 + Environment.NewLine;
        }

        private string PrintDividingLine()
        {
            return new string('-', LengthOfFirstColumn + LengthOfSecondColumn + LengthOfThirdColumn + LengthOfFourthColumn + LengthOfFifthColumn) + 
                   Environment.NewLine;
        }

        private string PrintFrame(Frame frame)
        {
            var firstLine =
                CreateStringWithTrailingSpaces(frame.FrameNumber.ToString(), LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("1", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces(frame.FirstRoll.ToString(), LengthOfThirdColumn) +
                CreateStringWithTrailingSpaces("", LengthOfFourthColumn) + 
                CreateStringWithTrailingSpaces(frame.FrameHasAStrike ? "Strike" : "", LengthOfFifthColumn);
            
            var secondLine =
                CreateStringWithTrailingSpaces(frame.FrameNumber.ToString(), LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("2", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces(frame.SecondRoll.ToString(), LengthOfThirdColumn);

            if (!BonusLineShouldBePrinted(frame))
            {
                secondLine += CreateStringWithTrailingSpaces(frame.AggregatedScore.ToString(), LengthOfFourthColumn) + 
                              CreateStringWithTrailingSpaces(frame.FrameHasASpare ? "Spare" : "", LengthOfFifthColumn);
            }
            else
            {
                secondLine += CreateStringWithTrailingSpaces("", LengthOfFourthColumn) +
                              CreateStringWithTrailingSpaces(frame.FrameHasASpare ? "Spare" : "",LengthOfFifthColumn );
            }

            
            var thirdLine =
                CreateStringWithTrailingSpaces(frame.FrameNumber.ToString(), LengthOfFirstColumn) +
                CreateStringWithTrailingSpaces("3", LengthOfSecondColumn) +
                CreateStringWithTrailingSpaces(frame.BonusRoll.ToString(), LengthOfThirdColumn) +
                CreateStringWithTrailingSpaces(frame.AggregatedScore.ToString(), LengthOfFourthColumn) +
                CreateStringWithTrailingSpaces("Bonus Roll", LengthOfFifthColumn);

            if (BonusLineShouldBePrinted(frame))
                return firstLine + Environment.NewLine + secondLine + Environment.NewLine + thirdLine +
                       Environment.NewLine;
            else
                return firstLine + Environment.NewLine + secondLine + Environment.NewLine;
        }

        private string CreateStringWithTrailingSpaces(string content, int totalLength)
        {
            var numberOfTrailingSpacesNeeded = totalLength - content.Length;

            if (numberOfTrailingSpacesNeeded < 0)
                throw new ArgumentException($"Content , {content}, is larger than total length, {totalLength}.");

            var trailingSpaces = new string(' ', numberOfTrailingSpacesNeeded);

            return content + trailingSpaces;
        }

        private static bool BonusLineShouldBePrinted(Frame frame)
        {
            if (frame.FrameNumber < 10)
                return false;

            return frame.FrameHasASpare || frame.FrameHasAStrike;
        }
    }
}