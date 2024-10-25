```
Author:     Joel Rodriguez and Nandhini Ramanathan
Partner:    None
Start Date: 20-Aug-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  joel-rodriguez and NandhiniRam2005
Repo:      https://github.com/uofu-cs3500-20-fall2024/spreadsheetpair-bollywood-burritos
Commit Date: 28-October-2024 10:20PM
Solution:   Spreadsheet
Copyright:  CS 3500 and [Joel Rodriguez and Nandhini Ramanathan] - This work may not be copied for use in Academic Coursework.
```

# Assignment Specific Topics
Throughout all the assignments we have learned about delegates, funcs, extension classes, unit testing in visual studio, debugging, and many other 
helpful tools to become a more efficient programmer in c#. We will soon learn more about GUIs and abstract classes.

# Overview of the Spreadsheet functionality

The Spreadsheet program is currently capable of assessing the syntax of formulas and checking whether or not a formula is correct 
syntactically. It achieves this through individually checking each token and assessing whether or not it is a valid token 
and if the token comes in a place where it makes logical sense.

Since then we have added the idea of a Dependency Graph to our Spreadsheet program. The Dependency Graph details the relationships
between cells and other cells. For example if one cell depends on the other and specifically keeps track of what cells are
the Dependee and which cells are Dependents. Using a dictionary and sets this extension of the program runs in O(1) time for
most methods. 

We have now extended the Spreadsheet program to be able to evaluate formulas that are given to it through the formula class.
The == and != operators have also been overwritten so that they make more sense in the context of comparing formula objects.
We have also modified the hash code and Equals methods.

The spreadsheet solution has been extended to include an abstract idea of what spreadsheet is. We have ways to manage dependencies and find
circular dependencies or invalid names. Eventually we will be able to actually evaluate what is inside of our cells.

The spreadsheet solution has now been extended to have a model of the spreadsheet that serves as the connecting pieces between the dependency
graph, the formulas, and the spreadsheet. This model has also added functionality for the spreadsheet to be saved and loaded using JSON 
serialization. In addition to that there have also been some Public API additions which allow users to get the values of the cells instead
of just the contents.

The Spreadsheet program now includes a GUI for the spreadsheet model. This GUI is a friendly adaptation of our spreadsheet model that we created in assignment 6.
and strategically using the Lookup delegate in the Formula class to accurately evaluate Formulas with variables (actually go into the spreadsheet and get the 
value of the cell).

Future extensions to spreadsheet will be adding ads and hopefully selling out.

# Examples of Good Software Practice (GSP).  
1. Code Reuse - I am not using the same code over and over again. Instead I reuse code through the use of helper methods. For example my Spreadsheet project has
   a NormalizeToken private helper method because this code is used multiple times throughout the project.
2. Testing strategies - I use multiple testing strategies including the Test Driven Development strategy and regression testing. I frequently run my tests after
   I add anything in my code to ensure I have not harmed my solution. I also practice TDD which helps me get the solution right the first time.
3. Self Documenting Code - My code uses good variable names and easy to read syntax. For example I vouch for using for each statements with good variable names 
   to ensure my code is easy to read even without comments.
4. DRY - I am aware that this is somewhat like code reuse but it also encompasses not repeating data. In my solution I ensure to not repeat data or code! For example in 
   my spreadsheet class I do not repeat the data of the name of a cell inside the cell class and only store it in the Dictionary.
5. Style - I ensure to follow the style guidelines that were given to us in class and in documents shared on canvas. I do not have unindented code or code that is hard 
   to read in my solution.
6. XML Documentation - In addition to having self documenting code I made sure to write good XML comments for all my member variables, methods, and tests. This ensures
   that in addition to my code being easy to read there are also concise XML comments that are easy to read both on-line and in an IDE using intellisense. 
Others I have achieved are:
1. Well named, commented, short methods that do a specific job and return a specific result.
2. Abstraction
3. Regression Testing (I did have old tests)

# Time Management Skills
Throughout this project I have learned many things about time management skills and estimation skills. One of the main things I have learned is 
that if anything it is best to overestimate how long you think an assignment will take you that way you can plan ahead and have the proper amount
of time dedicated to it. I have also learned that it is good to not sit down and do all your coding in one sitting. This leads to you missing
edge cases or possibly forgetting to do certain things in your work. I think I have been getting slowly better with my time estimation skills and will
continue to get better as the semester continues.

# Time Management Skills  / Time Tracking - A7
Throughout this assignment we learned more about time tracking and how to accurately estimate our time and improve our time management skills. We both felt
that working with a partner added more pressure to being focused because both of grades are on the line. This led to us working on the assignment more 
diligently. We also felt like we were able to accurately predict how long the assignment take us, which was about 13 hours. We hope to take these skills into 
our next assignments.

# Time Expenditures:

    1. Assignment 1: Test Driven Development        Predicted Hours:  8        Actual Hours:   6.5
    2. Assignment 2: Formula Class                  Predicted Hours:  17       Actual Hours:   8.5
                                                                               
                                                                                Hours Spent - 
                                                                                   Effectively:     3
                                                                                   Debugging:       3
                                                                                   Learning Tools:  2.5

    3. Assignment 3: Dependency Graph Class         Predicted Hours:  15       Actual Hours:   9.2

                                                                               Hours Spent - 
                                                                                   Effectively:     4.2
                                                                                   Debugging:       3
                                                                                   Learning Tools:  2

    4. Assignment 4: Formula Evaluate Extension     Predicted Hours:  16       Actual Hours:   10
                                                                                Hours Spent - 
                                                                                   Effectively:     6.5
                                                                                   Debugging:       3
                                                                                   Learning Tools:  0.5

    5. Assignment 5: Onward to a Spreadsheet        Predicted Hours:  16       Actual Hours:   9
                                                                                Hours Spent - 
                                                                                   Effectively:     3
                                                                                   Debugging:       5.8
                                                                                   Learning Tools:  0.2

    6. Assignment 6: A Full Spreadsheet Model        Predicted Hours:  15       Actual Hours:   18
                                                                                Hours Spent - 
                                                                                   Effectively:     4
                                                                                   Debugging:       13.5
                                                                                   Learning Tools:  0.5

    7. Assignment 6: A Full Spreadsheet Model        Nandhini Ramanathan: 
                                                     Predicted Hours:  13         Actual Hours:   
                                                                                Hours Spent - 
                                                                                   Effectively:     
                                                                                   Debugging:       
                                                                                   Learning Tools:  

                                                     Joel Rodriguez:
                                                     Predicted Hours: 13          Actual Hours:   
                                                                                Hours Spent - 
                                                                                   Effectively:     
                                                                                   Debugging:       
                                                                                   Learning Tools: 
