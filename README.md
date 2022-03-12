# Introduktion 
I have tried to write the solution creating classes with rich behaviour as we discussed in our last meeting.
This has resulted in three classes:

* Frame
* ScoreBoard
* HorizontalPrettyPrinter

## Frame
Frame is modelling a single frame in the scoreboard - storing information of the pins knocked down in first and second roll of the frame.
I wondered if I should modulate a Roll also but decided to keep the Rolls as nullable int and revisit the decision if I felt the need to. 
It turned out that nullable int worked fined as can be seen in the relative simple implementation of the Score property (Here the nullable part really helps in determining if a Score is undetermined - returns null) 
I have never used so many ? in a class before :)

I have used a linked list approach to link the Frames together as can be seen in the properties Previous Frame and NextFrame. I started out with a link list approach because of the way a score is calculated - to calculate aggregated score you need the aggregated score of the previous and to calculate score you need to know about the pins knocked down in the next frame).
Again I felt i ended a nice place and kept the implementation like this - an alternative could of course be to have a List of Frames in the ScoreBoard class. 

## ScoreBoard
The ScoreBoard class turned out to be pretty lean. As the program evolved much of the work could be delegated to state changes in a Frame or to the IPrettyPrinter interface which has becomed responsible for printing the score board in form of a string.
It only needed to know about the first frame (used when printing a scoreboard) and the last (actualFrame).

## IPrettyPrinter interface and HorizontalPrettyPrinter
I created the IPrettyPrinter interface to be injected into the score board class. It is - as the name suggest - responsible for printing the score board - in this simple case as a string to be written for instance in the console view. HorizontalPrettyPrinter implements this interface and provides a horisontal print inspired by the one in the assignment. You could also create a VerticalPrettyPrinter implementation that would look more like the one you meet in the bowling alley.


## Performance neglated
I have in some cases ignored the performance - for instance in the calculation of aggregated score. 
With this implementation when aggregated score property is called by the Pretty Printer for each Frame then the same calculation are done multiple times.
However these are in memory calculation for max 10 frames and the purpose of the scoreboard printer is very unlikely to be high Performance and with this implementation we instead get the very readable code:

*public int? AggregatedScore =>  (PreviousFrame?.AggregatedScore + Score) ?? Score;*


## When I look at the code
I am actually pretty satisfied. I like the use off nullable int (which I very rarely use) and the linked list approach (which I also rarely use)
There are some of the bool logic which might be written more readable - but this is where the code ends today. There are many unittest covering the code so refactoring of the bool logic should be a safe task if someone wanted to pretify this

# Getting Started
The solution can be started via the console application project Starter.
Bu running the solution 3 score board examples will be printed in console - 1 gutter game example, 1 perfect game example and 1 Kasper goes bowling (random score) example

# Build and Test
Unit test for the classes in BowlingScoreBoard can be seen in the project BowlingScoreBoardUnitTests projekt
