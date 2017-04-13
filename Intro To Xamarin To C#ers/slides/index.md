- title : Bringing the fun from functional programming into Xamarin Forms with F#
- description : 

what's the problem 

F# is a great .Net based functional programming language, that is also supported by Xamarin. With OOP support in F# it's possible to write awesome apps with Xamarin Forms taking advangte of functional programming concepts.

What's the talk 
For this talk, Sam Williams will show you how F# and Xamarin fit together. Building a simple app you'll see: F# has a clean syntax, F# for improved type inference (find out what that means too), F# for unit testing your app, F# in your C# app, along with a little bit of domain modelling. Sam will wrap up with a few nice F# features that are great on .NET, and are useful for that 'extra' stuff needed for app development.

By the end of this talk you'll know some of the compelling reasons why you should consider F# and how to add it to your existing C# app or start using it for your next app.  


- author : Sam Williams
- theme : night
- transition : default

***
### What I'll cover

- What is F#
- Why F#
    - working through an example
    - Easy to read: syntax is cleaner
    - Less typing: A great type system
    - Test code is cleaner: better testing tools
    - Domain modelling is easier

***
### What I won't be covering

- Every language features
- F# on the server
    - this is big strength of F#
    - works with Azure/Azure Functions
    - Data analytics
- type providers (feature specific to F#)
- It's ok if you don't understand all of material
    - It's not JavaScript, the language was made with more though! 
        (ie some of it is complicated)

***
### What is F#

- Programming language developed by Microsoft
- Open source
- Supports OOP and functional programming 
- Runs on .NET
- Supported by Xamarin

### The app we'll be building

- Task/todo app
- Add todo items
- wire up with view model
- extend the design a litle
- unit test

***
### Why F#: It's cleaner to read

- Very clean syntax
- many keywords can ommitted 
- return keyword not needed 
- curly braces not used (indentation instead)

***
### Smarter type system

- like Var in C# (but better)
- Types do not always need to be declared
- Prefer functions over Objects
- Let's see it in action!

' Create the model

***
### Syntax in Xamarin Forms

- Let's see it in action!

' Review how the F# code is shorter
' Review Android and iOS projects
' Highlight that Android now 'works'
' iOS has less files
' Connect up the simple view model

***
### Why F#: Clean unit tests

- leverage the type as above
- Types make refactoring easy
- functions are easy to declare

***
### unit tests with F# + Xamarin

- NUnit is supported
- 1 very cool feature for naming: will demo
- Great F# libraries that support NUnit 3
- Can use F# with UI test

' Demo creating a project with single page

***
### Why F#: Dommain modelling

- Declarative language: what over how
- smarter compiler: will check your work
- enhanced data structures
    - Records
    - Descriminated unions
- sensible defaults

***
### Adding a simple model

- Todo 'class' with completed status
- F# record:
    - lightweight class with private members
    - equality defined 
    - mutable
- Enum: In F# called Descriminated union 
    - immutable
    - equality
    - max/min defined

***
### Model demo

- Over to the demo

***
### Think twice before using null

- by default F# gives you a warning
- can turn off

 `` Tony Hoare: This has led to innumerable errors, vulnerabilities, and system crashes, which have probably caused a billion dollars of pain and damage in the last forty years``

***
### Wiring up the view model with the model

- Sensible to have a null here
- Record types do not support null
- This is a good thing
- We can override using box
