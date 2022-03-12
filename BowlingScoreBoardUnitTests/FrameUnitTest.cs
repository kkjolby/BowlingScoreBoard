using System;
using BowlingScoreBoard;
using Xunit;

namespace BowlingScoreBoardUnitTests
{
    public class FrameUnitTest
    {
        
        //Clean Score Tests
        [Fact]
        public void ScoreIsSumOfPins()
        {
            //Arrange
            var frame = new Frame
                {FrameNumber = 1, FirstRoll = 3, SecondRoll = 5};
            
            //Assert
            Assert.Equal(8, frame.Score);
        }

        [Fact]
        public void SpareScoreIsSumOfPinsPlusNextFrameFirstRoll()
        {
            //Arrange
            var firstFrame = new Frame() {FrameNumber = 1, FirstRoll = 3, SecondRoll = 7};
            var secondFrame = firstFrame.CreateNextFrame();
            secondFrame.FirstRoll = 5;
            
            //Assert
            Assert.Equal( 3 + 7 + 5, firstFrame.Score);
        }

        [Fact]
        public void StrikeScoreIsSumOfPinsPlusNextFrameFirstAndSecondRoll()
        {
            //Arrange
            var firstFrame = new Frame() {FrameNumber = 1, FirstRoll = 10};
            var secondFrame = firstFrame.CreateNextFrame();
            secondFrame.FirstRoll = 5;
            secondFrame.SecondRoll = 3;
            
            //Assert
            Assert.Equal( 10 + 5 + 3, firstFrame.Score);
        }

        [Fact]
        public void TwoStrikesInARowScoreIsSumOfPinsPlusNextFrameFirstAndNextFrameFirstRoll()
        {
            //Arrange
            var firstFrame = new Frame() {FrameNumber = 1, FirstRoll = 10};
            var secondFrame = firstFrame.CreateNextFrame();
            secondFrame.FirstRoll = 10;

            var thirdFrame = secondFrame.CreateNextFrame();
            thirdFrame.FirstRoll = 5;
            
            //Assert
            Assert.Equal( 10 + 10 + 5 , firstFrame.Score);
        }

        [Fact]
        public void StrikeInFrame10ShouldUseBonusRoll()
        {
            //Arrange
            var tenthFrame = new Frame() {FrameNumber = 10, FirstRoll = 10, SecondRoll = 5, BonusRoll = 7};
            
            //Assert
            Assert.Equal(10 + 5 + 7, tenthFrame.Score);
        }
        
        [Fact]
        public void SpareInFrame10ShouldUseBonusRoll()
        {
            //Arrange
            var tenthFrame = new Frame() {FrameNumber = 10, FirstRoll = 7, SecondRoll = 3, BonusRoll = 4};
            
            //Assert
            Assert.Equal(7 + 3 + 4, tenthFrame.Score);
        }

        
        //Score is Undefined tests
        [Fact]
        public void ScoreOfEmptyFrameIsUndefined()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 1};
            
            //Assert
            Assert.Null(frame.Score);
        }

        [Fact]
        public void ScoreAfterFirstRollIsUndefined()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 1, FirstRoll = 5};
            
            //Assert
            Assert.Null(frame.Score);
        }

        [Fact]
        public void ScoreAfterSpareIsUndefined()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 1, FirstRoll = 4, SecondRoll = 6};
            
            //Assert
            Assert.Null(frame.Score);
        }
        
        [Fact]
        public void ScoreAfterStrikeIsUndefined()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 1, FirstRoll = 10};
            
            //Assert
            Assert.Null(frame.Score);
        }
        
        [Fact]
        public void ScoreAfterStrikeAndNextFrameFirstRollIsUndefined()
        {
            //Arrange
            var firstFrame = new Frame() {FrameNumber = 1, FirstRoll = 10};
            var secondFrame = firstFrame.CreateNextFrame();
            secondFrame.FirstRoll = 5;
            
            //Assert
            Assert.Null(firstFrame.Score);
        }
        
        //ExceptionTests
        
        [Fact]
        public void EnterPinsInAFullFrameShouldThrowException()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 1, FirstRoll = 3, SecondRoll = 7};
            
            //Assert
            Assert.Throws<InvalidOperationException>(() => frame.EnterKnockedDownPins(4));
        }

        [Fact]
        public void EnterMoreThan10PinsShouldThrowException()
        {
            //Arrange
            var frame = new Frame();
           
            //Assert
            Assert.Throws<ArgumentException>(() => frame.EnterKnockedDownPins(11));
        }
        
        [Fact]
        public void EnterMoreThan10PinsInTwoCallsShouldThrowException()
        {
            //Arrange
            var frame = new Frame();
            frame.EnterKnockedDownPins(6);
           
            //Assert
            Assert.Throws<ArgumentException>(() => frame.EnterKnockedDownPins(5));
        }

        [Fact]
        public void TenthFrameSpareShouldNotThrowExceptionWhen10BonusPins()
        {
            //Arrange
            var frame = new Frame() {FrameNumber = 10, FirstRoll = 3, SecondRoll = 7};
            
            //Assert no Exception
            frame.EnterKnockedDownPins(10);
        }
    }
}