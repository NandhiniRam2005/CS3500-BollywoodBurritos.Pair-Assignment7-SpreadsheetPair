```
Author:     Joel Rodriguez
Partner:    None
Start Date: 20-Aug-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  joel-rodriguez
Repo:      https://github.com/uofu-cs3500-20-fall2024/spreadsheet-joel-rodriguez
Commit Date: 27-September-2024 10:20PM
Solution:   Spreadsheet
Copyright:  CS 3500 and [Joel Rodriguez] - This work may not be copied for use in Academic Coursework.
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

Future extensions of the Spreadsheet program include creating a GUI for the spreadsheet and strategically using the Lookup 
delegate in the Formula class to accurately evaluate Formulas with variables (actually go into the spreadsheet and get the 
value of the cell).

# Examples of Good Software Practice (GSP).  
1. Code Reuse - I am not using the same code over and over again. Instead I reuse code through the use of helper methods. For example my Spreadsheet project has
   a NormalizeToken private helper method because this code is used multiple times throughout the project.
2. Testing strategies - I use multiple testing strategies including the Test Driven Development strategy and regression testing. I frequently run my tests after
   I add anything in my code to ensure I have not harmed my solution. I also practice TDD which helps me get the solution right the first time.
3. Self Documenting Code - My code uses good variable names and easy to read syntax. For example I vouch for using for each statements with good variable names 
   to ensure my code is easy to read even without comments.

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

