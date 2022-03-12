using System;

namespace BowlingScoreBoard
{
    public class Frame
    {
        public int FrameNumber { get; init; } = 1;
        public int? FirstRoll { get; set; }
        public int? SecondRoll { get; set; }
        public int? BonusRoll { get; set; }

        public Frame NextFrame { get; set; }
        public Frame PreviousFrame { get; set; }

        public bool FrameHasASpare => FirstRoll + SecondRoll == 10  && !FrameHasAStrike;
        public bool FrameHasAStrike => FirstRoll == 10;
        
        public int? AggregatedScore =>  (PreviousFrame?.AggregatedScore + Score) ?? Score;
        public int? Score
        {
            get
            {
                if (FrameHasASpare && FrameNumber == 10)
                    return FirstRoll + SecondRoll + BonusRoll;
                    
                if (FrameHasASpare) 
                    return FirstRoll + SecondRoll + NextFrame?.FirstRoll;
                
                if(FrameHasAStrike && FrameNumber == 10)
                    return FirstRoll + SecondRoll + BonusRoll;
                
                if (FrameHasAStrike) 
                    return FirstRoll + NextFrame?.FirstRoll + (NextFrame?.SecondRoll ?? NextFrame?.NextFrame?.FirstRoll);
                
                return FirstRoll + SecondRoll;
            }
        }
        
        
        public bool FrameIsFull()
        {
            if (FrameNumber < 10 && FrameHasAStrike)
            {
                return FirstRoll.HasValue;
            }

            if (FrameNumber < 10)
            {
                return SecondRoll.HasValue;
            }

            // Framenumber = 10!
            if (FrameHasASpare || FrameHasAStrike)
            {
                return BonusRoll.HasValue;
            }

            return SecondRoll.HasValue;
        }

        public Frame CreateNextFrame()
        {
            var nextFrame = new Frame() {FrameNumber = this.FrameNumber + 1, PreviousFrame = this};
            this.NextFrame = nextFrame;
            return nextFrame;
        }

        public void EnterKnockedDownPins(int numberOfPins)
        {
            ValidateFrameIsFull();
            ValidateNumberOfPins(numberOfPins);

            if (!FirstRoll.HasValue)
            {
                FirstRoll = numberOfPins;
                return;
            }

            if (!SecondRoll.HasValue)
            {
                SecondRoll = numberOfPins;
                return;
            }
            
            //Since frame is not Full we must be in 10th frame and a strike/spare situation
            BonusRoll = numberOfPins;
        }
        

        private  void ValidateNumberOfPins(int numberOfPins)
        {
            if (numberOfPins > 10) throw new ArgumentException($"You cannot knock down {numberOfPins} pins - max is 10 pins");
            
            if (FrameNumber < 10 && numberOfPins + FirstRoll > 10)
                throw new ArgumentException($"You cannot knock down {numberOfPins} - only {10 - FirstRoll} is left");
            
            if(FrameNumber == 10 && !FrameHasAStrike &&  numberOfPins + FirstRoll > 10 && !SecondRoll.HasValue)
                throw new ArgumentException($"You cannot knock down {numberOfPins} - only {10 - FirstRoll} is left");
            
            if(FrameNumber == 10 && FrameHasAStrike &&  numberOfPins + SecondRoll > 10 && SecondRoll < 10)
                throw new ArgumentException($"You cannot knock down {numberOfPins} - only {10 - SecondRoll} is left");
        }

        private void ValidateFrameIsFull()
        {
            if (FrameIsFull())
                throw new InvalidOperationException("You cannot enter more pins in this Frame - it is already full");
        }
    }
}
