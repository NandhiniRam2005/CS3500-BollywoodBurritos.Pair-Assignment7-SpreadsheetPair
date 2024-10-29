```
Author:     Joel Rodriguez and Nandhini Ramanathan
Partner:    None
Start Date: 22-Oct-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  joel-rodriguez and NandhiniRam2005
Repo:      https://github.com/uofu-cs3500-20-fall2024/spreadsheetpair-bollywood-burritos
Commit Date: 29-October-2024 10:00 PM
Project:    GUI Client
Copyright:  CS 3500 and[Joel Rodriguez and Nandhini Ramanathan] - This work may not be copied for use in Academic Coursework.```
```

# Comments to Evaluators:
We decided to add the README to both the GUI and GUI.client per the instructions of a TA via office hours. We were also told to just leave the contents of
a cell that has been cut off after shortening spreadsheet in the spreadsheet per instructions of both Professor De St Germaine and a TA.
Upon loading we removed the popup which shows the JSON being loaded. This popup in our opinion is ugly and not user friendly.A random person in the real world
would get concerned by seeing such a thing so we removed it.
In class Professor De St Germaine's GUI implementation included a Name in the Json.So we went ahead and included the name in our spreadsheet JSON as well.
A student also asked in class if we would be allowed to add a feature where we could add the name to the spreadsheet in which Professor De St Germaine said
yes.So we implemented it.  (Check lecture 16 time stamp 41:00 if you want proof)
We noticed that the website looks different on different machines, this is because Professor De St Germaine used px as a unit for defining the size of components instead of 
vw and vh. We chose not to change this because this would require a lot of time (finding all components and figuring out how to convert to vw and vh). Please just use 
a zoom percentage which seems right.

# Assignment Specific Topics
We decided to make various design decisions.For example we decided to:
1. Added functionality to type inside the cells themselves
2. Organized the spreadsheet to look more friendly to the user.
3. Added CSS styling to make the spreadsheet more inviting.
4. Added way for the user to change the amount of rows and columns (up to 100 rows and 26 columns)
5. Added Save and Load functionality 
6. Added way for someone to name the spreadsheet and for the name to be displayed
7. Added functunality for when spreadsheet loads that its size changes to the minimum size it needs to be (min 10 x 10) For example if loaded spreadsheet
   has a cell at Z100 filled out then the spreadsheet will load in at being 100x100. If there is no cell outside of the default 10x10 then the size loaded is 10x10.
8. All other expected functionality of a basic spreadsheet program.
Throughout the implementation of this project we frequently referenced stack overflow, the documentation of Blazor, and of course the ever knowledgeable
TA's of CS 3500. 
We occasionally ran into problems when attempting to add certain things to our spreadsheet which we eventually gave up on to ensure we
would not be spending too much time and things that didn't matter. For example:
1. Attempted to add arrows keys to maneuver around cells in spreadsheet, it worked in every situation except for the situation where you were typing something and then pressed
   arrow key. This led to various javascript errors which for whatever reason would overwrite the cell we maneuvered too with the text that we were typing. There was an also 
   an issue of practicality. A user must be selected into a input (and typing) to use the arrow keys. We abandoned the idea (you can view our git commits to see what we tried).
2. Attempted to add Google Ad-sense to our project for fun. We realized this would require a server so abandoned the idea.
Using our page is simple and functions just as a spreadsheet would. The basic idea is:
1. Click on cell to type into 
2. Type into cell itself or the contents in the tool-bar
3. Press enter or click to different cell to save contents into spreadsheet
4. Press save and load to save and load files.
5. Press clear to clear spreadsheet.

# Partnership Information
All of our code was done using pair programming. We found that it would be too difficult and unfair for us to split up the work so we
decided to not split up the work.Our schedules aligned very well so we decided to just meet up whenever we could on campus to work on
the assignment.On occasion we would also work together over discord on late nights when we were feeling motivated.The only work
we did on our own time at times was searching up bugs.When we encountered a bug in our code we would pause and do some googling on our own
and rejoin when we had a found a solution on line.

We both felt that our partnership was a success.We were partners in 2420 so we are already familiar with each others coding styles and this
assignment was a breeze because of that.The first reason why our partnership was so successful was because both of us having very different
ways of approaching code problems. For example there was many times during our implementation where one of us would stop the other person
while they were trying to code something and suggest a simpler and faster way of doing things.Another way our partnership was successful
was that we were able to bounce ideas of each other and give each other good and constructive feedback.Neither of us were afraid to tell
each other that we thought an idea wouldn't add much to the spreadsheet or would be too hard too implement. To help the code process go faster 
when we realized that one us was getting tired of typing or was getting their brain fried we would switch off drivers and all the other person
to start. We would take periodic breaks to ensure we only coded quality code. We chose not to assign tasks since that felt unfair to the both of us.

Although are partnership was great their was places where we could improve. For example I felt that we had two problems that sometimes made us program
slowly and not be to get the assignment done as quickly as we could of. One of the things that held us back was our similar class schedule we would frequently
get distracted talking about other class homework like 3810 homework since we have the same class together. Another problem we had was talking too much, we
always went to do work in the cade lab together with all of our friends and we found that we kept distracting each other with conversations about Bronny James
and Jujutsu Kaisen.We need to be able to focus more on future assignments.

# Branching

We did all of our coding in pairs together. On occasion we would switch off who's computers we were using. We mostly used one computer (which is why most 
commits are under joel-rodriguez) but sometimes we would switch to Nandhini's computer when we were doing pair programming online since her computer is faster.
This is why you see a mix of commits from both Joel and Nandhini.We didn't work on additional functionality on our own to make sure that no one would fall 
behind.So to sum it up there are NO branches.


# Consulted Peers:

List any peers (or other people) in the class (or outside for that matter) that you talked with about the project for more than one minute.

1. Adharsh
2. Jacob
3. Various TA's (Parker and Tim).

# References:

    1. Blazor Documentation - https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0
    2. Input type number - https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/number
    3. Blazor Stackover flow (for inspiration) - https://stackoverflow.com/questions/tagged/blazor
    4. When to call State has changed - https://stackoverflow.com/questions/60096040/when-should-i-call-statehaschanged-and-when-blazor-automatically-intercepts-that