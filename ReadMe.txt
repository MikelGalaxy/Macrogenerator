MACROGENERATOR - Michał Boniakowski ECOTE 2019

TASK:
    Project task no. 11:
    “Write a macrogenerator with no parameters but definitions can be nested. 
    The discriminant of definition is &.
    &CC means start of definition of macro CC.
    &CC TEXT - possible definitions of macro &
    Macrocall: $CC“


NOTES:
To run this app 2.1 Dotnet Core SDK is required.

HOW TO BUILD:
1. Open terminal in main project location
2. Use "dotnet build" command

HOW TO RUN:
1. Open terminal in Macrogenerator.dll location
2. Write dotnet Macrogenerator.dll EOPSY_TEST.txt      (name of the file)
3. Run it
4. Output output_EOPSY_TEST.txt should be created