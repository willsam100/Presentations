- title : Bringing the fun from functional programming into Xamarin Forms with F#
- description : 
- author : Sam Williams
- theme : sky
- transition : default

***
### Bringing the fun from functional programming into Xamarin Forms with F#

with Sam Williams

Company: EROAD

Twitter: @willsam100

<br/>
<br/>
<br/>
<small>all opinions are my own</small>

***
### What will be covered

- What is F#
- Why F#
    - Expressive syntax
    - A great type system
    - Domain modelling is easier
- applied to an app 
- applied to testing

***
### What is F#

- Programming language developed by Microsoft
- Open source
- Prefers functional style, supports OOP
- FP was discovered in the 1930s
- Runs on .NET
- Supported by Xamarin

***
### The app we'll be building

- Todo/notes app
- Adding todo items
- Will use a View Model
- Support todos first, extend with notes
- Will add unit tests

***
## It's cleaner to read

***
### It's cleaner to read

- Very short syntax
- many keywords can omitted 
- return keyword not needed 
- curly braces not used (indentation instead)

***
## Smarter type system

***
### Smarter type system

- like var in C# (but better)
- types do not always need to be declared
- prefer functions over objects     
    - to take greater advantage of this
- let's see it in action!

' Create the model

***
### Defining a class
#### Some F# Syntax

    type Person(firstname: string) = 

        let mutable _firstname = firstname

        member this.Firstname 
            with get() = _firstname
            and  set(value) = _firstname <- value
***
### Assignment
#### Some F# Syntax

    // '<-' means assignment
    let mutable x = 1
    x <- 2
    // C#: x = 2

***
### Syntax in Xamarin Forms

Let's see it in action!

' Review how the F# code is shorter
' Review Android and iOS projects
' Highlight that Android now 'works'
' iOS has less files
' Connect up the simple view model

***
### Clean unit tests

***
### Clean unit tests

- leverage the type system
- Types inference makes refactoring easy
    - and sometimes not even needed (for the types)
    - inference means the types 'just work'
- functions are easy to declare
    - easy to extract boiler plate code

***
### Testing with F# + Xamarin

- NUnit has been used
- Works with IDEs (VS for Mac/Windows)
- There are great libraries that require NUnit 3.x
- Can use F# with UI test
- Add a unit test project, select F# for language

' many options for unit testing with F#

***
### unit tests with F# + Xamarin

    // DemoViewModelTests.fs
    module DemoForms.Test
    open NUnit.Framework

    [<TestFixture>]
    type Tests() = 

        [<Test>]
        member this.Command_addsEntryToList () =

            let todo = "my todo"
            let vm = DemoViewModel()
            vm.Entry <- todo

            vm.AddTodo.Execute null

            Assert.IsTrue(1 = vm.Todos.Count)

' Braces
' <- assignment
' Equals means Equals
' invoke without parens
' Argue about the name

***
### unit tests with F# + Xamarin

    // .. Test fixture setup

        [<Test>]
        this.``Command adds entry to list`` () =

            // test implementation

***
### unit testing
#### Building a simple DSL

- Want to make the unit tests easy to read
- Easy to refactor 
- pragmatic/clean code
<br>
<br>
- Reminder: last line of an F# function is the return value

***
### F# syntax
#### Piping

    // Console.WriteLine("hello")
    "hello" |> Console.WriteLine

    let num = 42 
    let toString x = 
        sprintf "%d" x

    //Console.WriteLine(toString(42))
    num |> toString |> Console.WriteLine

***
### F# syntax
#### Partial application

    // int -> int -> int
    let add x y = 
        x + y

    // int -> int
    let add3 = add 3    

    add3 2 // returns 5 

***
### unit testing
#### Building a DSL

    [<TestFixture>]
    type Tests() = 
        let createViewModel () = 
            DemoViewModel()

        let setEntry todo (vm: DemoViewModel) = 
            vm.Entry <- todo
            vm

        let addTodo (vm: DemoViewModel) = 
            vm.AddTodo.Execute null 
            vm

        // Current unit tests below

***
### unit testing
#### Building a DSL

    // helper methods defined above 
    // createViewModel, setEntry, addTodo

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let todo = "my todo"
        let vm = 
            createViewModel () 
            |> setEntry todo
            |> addTodo

        vm.Todos |> Seq.head |> (fun t -> Assert.Equals(todo, t))

' Seq.head = list.First();

***
### unit testing 
#### F# libraries

- assertion libraries:
    - FsUnit 
        - idiomatic F# Code
        - use for this app/demo
- Foq: F# version of Moq
    - Moq - 16,000 LOC, Foq less than 1,000 LOC
- FsCheck
    - parameterise unit tests
    - auto generate test data
    - requires NUnit 3

***
### unit testing
#### Building a DSL

    // add to top of file
    open FsUnit

    // rest of implementation

    [<Test>]
    member this.``Command with builder pattern`` () =
            
        let todo = "my todo"
        let vm = 
            createViewModel () 
            |> setEntry todo
            |> addTodo

        vm.Todos |> Seq.head |> should be (equal todo)

***
## Domain modelling
    
***
### Domain modelling

- Declarative language: what over how
- smarter compiler: will check your work
- enhanced data structures
    - Records
    - Choice type / discriminated union
- sensible defaults in the language
    - null is discouraged

***
### Adding a simple model

- Todo 'class' with completed status
- Extend the view model to use this

***
### Adding a simple model
#### F# record

- object for holding information
- equality defined 
- immutable

***
### Adding a simple model
#### F# discriminated union / Choice type

- describes a choice between N types
- Like an enum but with more power!
- immutable
- equality
- max/min defined


    type Bool = true | false

***
### Model demo
    // Model.fs
    module DemoForms.Model

    // Similar to an enum
    type Status = Completed | Current

    type TodoItem = {
        Action: string 
        Status: Status
    }

    let newTodo action = 
        {Todo = action; Status = Current}

    let completeTodo todo = 
        {todo with Status = Completed}

' Completed or Current
' TodoItem simple class
' helper functions

***
### Model demo

    // DemoFormsPage.xaml.fs
    type DemoViewModel() as this = 

        // Items are immutable by default, remove and add
        let markTodoAsCompleted x = 
            collection.Remove x |> ignore 
            x |> completeTodo |> collection.Add

        let addTodo () = 
            this.Entry |> newTodo |> collection.Add

        // omitted previous code

        member this.SelectedItem 
            with get() = null
            and set(value) = markTodoAsCompleted (unbox value)

' selecting cell for completing 
' warnings for not using return values
' branch five for demo

***
### Building on our unit tests

- add a unit test for completed
- extend the DSL
- add a single test

***
### Building on our unit tests
#### Extending the dsl

    // Omitted previous code

    let getFirstTodo (vm: DemoViewModel) = 
        let todo = vm.Todos |> Seq.head
        (todo, vm)

    let completeTodo (todo, vm: DemoViewModel) = 
        vm.SelectedItem <- todo
        vm

    // Omitted existing unit tests

***
### Building on our unit tests   
#### Writing the test 

    [<Test>]
    member this.``Create Todo And Mark As Completed`` () =
            
        let todo = "my todo"

        let firstTodo = 
            createViewModel() 
            |> setEntry todo 
            |> addTodo
            |> getFirstTodo
            |> completeTodo
            |> getFirstTodo

        firstTodo.Status |> should be (equal Completed)
        firstTodo.Action |> should be (equal todo)

***
Domain modelling: Demo time

***
### Domain modelling
#### I want to take notes!!!

- Extend our model to support taking notes
- A note is text. 
- A note cannot be completed
- Use a choice type in our viewModel collection 
- wrap the TodoItem in one choice
- wrap a note in another choice

***
### Domain modelling
#### I want to take notes!!!

    // Choice type can hold data on the choice
    type Item = 
    | Todo of TodoItem 
    | Note of string

***
### View Model
#### I want to take notes!!!

    // will only compile if all the types line up
    let markTodoAsCompleted x = 
        match x with 
        | Todo todo -> 
            todo |> Todo |> collection.Remove |> ignore
            todo |> completeTodo |> Todo |> collection.Add
        | Note _ -> ()

    let addNote () = 
        this.Entry |> Note |> collection.Add

    let addTodo () = 
        this.Entry |> newTodo |> Todo |> collection.Add
        
    member this.AddNote with get() = Command addNote    

---
### View Model
#### Before adding notes

    // DemoFormsPage.xaml.fs
    type DemoViewModel() as this = 

        // Items are immutable by default, remove and add
        let markTodoAsCompleted x = 
            collection.Remove x |> ignore 
            x |> completeTodo |> collection.Add

        let addTodo () = 
            this.Entry |> newTodo |> collection.Add

        // omitted previous code

***
### Taking a Note

I demand a live demo!

***
### Wrapping up
#### F# is...

- shorter/easier to read
- smarter with powerful type inference
- excellent for domain modelling
- provides so much more than this!!!

***

Thanks for your time
<br>
tweet me for more: F#, slides, demo coe
<br>
@willsam100









