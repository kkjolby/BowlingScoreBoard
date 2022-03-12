using System;
using BowlingScoreBoard;
using BowlingScoreBoard.Printers;
using Xunit;

namespace BowlingScoreBoardUnitTests
{
    public class ScoreBoardUnitTests
    {
        //GameIsFinishedTests
        [Fact]
        public void GameIsNotFinishedForNewGame()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            
            //Assert
            Assert.False(scoreBoard.GameIsFinished());
        }
        
        [Fact]
        public void GameIsNotFinishedForGameWithNineFrames()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SecondFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FourthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FifthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SixthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //EightFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //NinthFrame
            
            //Assert
            Assert.Equal(9,scoreBoard.ActualFrame.FrameNumber);
            Assert.False(scoreBoard.GameIsFinished());
        }
        
        [Fact]
        public void GameIsFinishedForGameWithTenFrames()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SecondFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FourthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FifthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SixthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //EightFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //NinthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //TenthFrame
            
            //Assert
            Assert.Equal(10,scoreBoard.ActualFrame.FrameNumber);
            Assert.True(scoreBoard.GameIsFinished());
        }
        
        [Fact]
        public void GameIsNotFinishedForGameWithTenFramesSpareAndNoBonusRoll()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SecondFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FourthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FifthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SixthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //EightFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //NinthFrame
            scoreBoard.EnterKnockedDownPins(10); scoreBoard.EnterKnockedDownPins(4); //TenthFrame - Spare
            
            //Assert
            Assert.Equal(10,scoreBoard.ActualFrame.FrameNumber);
            Assert.False(scoreBoard.GameIsFinished());
        }
        
        [Fact]
        public void GameIsFinishedForGameWithTenFramesSpareAndBonusRoll()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SecondFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FourthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FifthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SixthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //EightFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //NinthFrame
            scoreBoard.EnterKnockedDownPins(10); scoreBoard.EnterKnockedDownPins(4); scoreBoard.EnterKnockedDownPins(6); //TenthFrame - Spare
            
            //Assert
            Assert.Equal(10,scoreBoard.ActualFrame.FrameNumber);
            Assert.True(scoreBoard.GameIsFinished());
        }
        
        
        //EnterKnockedDownPins tests
        [Fact]
        public void CallingOnceKnockedDownPinsShouldLeaveActualFrameAs1()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1);
            
            
            //Assert
            Assert.Equal(1,scoreBoard.ActualFrame.FrameNumber);
        }
        
        [Fact]
        public void CallingThriceKnockedDownPinsShouldSetActualFrameTo2()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); //SecondFrame
            
            //Assert
            Assert.Equal(2,scoreBoard.ActualFrame.FrameNumber);
        }
        
        [Fact]
        public void CallingTwiceKnockedDownPinsWithStrikeFrameTo2()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(10); //FirstFrame - Strike
            scoreBoard.EnterKnockedDownPins(1); //SecondFrame
            
            //Assert
            Assert.Equal(2,scoreBoard.ActualFrame.FrameNumber);
        }
        
        [Fact]
        public void CallingKnockedDownPinsAfterTenthFrameShouldInvokeAnEception()
        {
            //Arrange
            var scoreBoard = new ScoreBoard(new FakePrettyPrinter());
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FirstFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SecondFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //ThirdFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FourthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //FifthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SixthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //SeventhFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //EightFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //NinthFrame
            scoreBoard.EnterKnockedDownPins(1); scoreBoard.EnterKnockedDownPins(4); //TenthFrame
            
            //Assert
            Assert.Throws<InvalidOperationException>(() => scoreBoard.EnterKnockedDownPins(1));
        }
        
    }

    
    public class FakePrettyPrinter : IPrettyPrinter
    {
        public string Print(ScoreBoard scoreBoard)
        {
            throw new System.NotImplementedException();
        }
    }
}