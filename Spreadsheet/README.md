```
Author:     Joel Rodriguez
Partner:    None
Start Date: 20-Sep-2024
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  joel-rodriguez
Repo:      https://github.com/uofu-cs3500-20-fall2024/spreadsheet-joel-rodriguez
Commit Date: 18-October-2024 10:00 PM
Project:   Spreadsheet
Copyright:  CS 3500 and [Joel Rodriguez] - This work may not be copied for use in Academic Coursework.```
```

# Comments to Evaluators:
I added a constructor to the spreadsheet and a cell class. I did this because in the assignment details it advised to create a cell class. The constructor 
was advised to be created in piazza question @402. I created a helper method for all three SetCellContents that way it is easier to understand what is 
happening at each stage. I moved the header comment to the correct location (under the namespace and using) so please look there for my header comment.
I have also not created an extension class for my spreadsheet class as it was not specifically mentioned in the assignment.
I was told by a TA that I had the freedom to decide how my evaluate method handles strings (Treat them as 0's or throw an exception). I choose to throw a formula .
Given that changed is a public property it would be included in the JSon serialization so I added a JsonIgnore tag I added a JSON include tag for the dictionary.
I also deserialize and serialize a spreadsheet object instead of a dictionary object. Computing it this way leads to less logic and easier to read code.
I also chose to add  a lot to my white board because I argued that a lot of the things were necessary and made sense to include in my white board. Most of the methods 
are built off of each other. And every method depends on other methods which makes them important.
I also understand that recalculating could cause a stackoverflow if we have a chain of 10,000 dependents but this is the way.
# Assignment Specific Topics
Assignment 5: Learn to utilize/combine the appropriate existing functionality to generate a more powerful combination.
Continue practicing comprehensive and deep reading of complex requirements and specifications. Continue to utilize fundamental 
programming concepts such as recursion. Continue to practice test driven development and achieving full code coverage from tests
Starting to understand the utility of the Model-View-Controller architecture
Assignment 6: We are to learn and practice using Serialization (via reflection) to save and restore data .Understanding more about
regression testing and updating tests to meet new APIs/requirements. Understanding how to draw useful sketches to represent system 
architectures. We will also continue to practice test driven development.

# Consulted Peers:

List any peers (or other people) in the class (or outside for that matter) that you talked with about the project for more than one minute.

1. Nandhini Ramanathan

# References:

    1. IList Interface - https://learn.microsoft.com/en-us/dotnet/api/system.collections.ilist?view=net-8.0
    2. Json in C# - https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/how-to
