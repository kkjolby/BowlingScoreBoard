using System;
using BowlingScoreBoard;
using BowlingScoreBoard.Printers;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            var gutterGame = GetScoreBoardForGutterGame();
            Console.WriteLine("This is the scoreboard for a gutter game:");
            Console.WriteLine(gutterGame.PrintScoreBoard());

            Console.WriteLine();
            Console.WriteLine();

            var perfectGame = GetScoreBoardForPerfectGame();
            Console.WriteLine("This is the scoreboard for a perfect game:");
            Console.WriteLine(perfectGame.PrintScoreBoard());

            Console.WriteLine();
            Console.WriteLine();
            
            var kasperInTheBowlingAlley = GetRandomScoreBoard();
            Console.WriteLine("This is the scoreboard last time Kasper was bowling:");
            Console.WriteLine(kasperInTheBowlingAlley.PrintScoreBoard());
        }

        private static ScoreBoard GetScoreBoardForGutterGame()
        {
            var scoreBoard = new ScoreBoard(new HorizontalPrettyPrinter());
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //FirstFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //SecondFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //FourthFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //FifthFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //SixthFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //EightFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //NinthFrame
            scoreBoard.EnterKnockedDownPins(0); scoreBoard.EnterKnockedDownPins(0); //TenthFrame

            return scoreBoard;
        }

        private static ScoreBoard GetScoreBoardForPerfectGame()
        {
            var scoreBoard = new ScoreBoard(new HorizontalPrettyPrinter());
            scoreBoard.EnterKnockedDownPins(10); //FirstFrame
            scoreBoard.EnterKnockedDownPins(10); //SecondFrame
            scoreBoard.EnterKnockedDownPins(10); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(10); //FourthFrame
            scoreBoard.EnterKnockedDownPins(10); //FifthFrame
            scoreBoard.EnterKnockedDownPins(10); //SixthFrame
            scoreBoard.EnterKnockedDownPins(10); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(10); //EightFrame
            scoreBoard.EnterKnockedDownPins(10); //NinthFrame
            scoreBoard.EnterKnockedDownPins(10); scoreBoard.EnterKnockedDownPins(10); scoreBoard.EnterKnockedDownPins(10); //TenthFrame

            return scoreBoard;
        }

        private static ScoreBoard GetRandomScoreBoard()
        {
            var scoreBoard = new ScoreBoard(new HorizontalPrettyPrinter());
            var rnd = new Random();

            try
            {
                while (!scoreBoard.GameIsFinished())
                {
                    var leftPins = 10;
                    //First Shot
                    var firstShotInFrame = rnd.Next(leftPins + 1);
                    scoreBoard.EnterKnockedDownPins(firstShotInFrame);
                    if (scoreBoard.ActualFrame.FrameIsFull())
                        continue;

                    //Second Shot
                    leftPins -= firstShotInFrame;
                    if (scoreBoard.ActualFrame.FrameNumber == 10 && leftPins == 0)
                        leftPins = 10;

                    var secondShotInFrame = rnd.Next(leftPins + 1);
                    scoreBoard.EnterKnockedDownPins(secondShotInFrame);
                    if (scoreBoard.ActualFrame.FrameIsFull())
                        continue;

                    //ThirdShot
                    leftPins -= secondShotInFrame;
                    if (scoreBoard.ActualFrame.FrameNumber == 10 && leftPins == 0)
                        leftPins = 10;

                    var thirdShotInFrame = rnd.Next(leftPins + 1);
                    scoreBoard.EnterKnockedDownPins(thirdShotInFrame);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine(scoreBoard.PrintScoreBoard());
            }

            return scoreBoard;
        }
    }
}