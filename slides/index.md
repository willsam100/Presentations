- title : Build a Xamarin Forms App with F#, with Sam Williams
- description : mobile apps, xamarin froms and lots of F#
- author : Sam Williams
- theme : league
- transition : default


***
### Building Xamarin Forms apps with F#
#### by Sam Williams

Twitter: @willsam100

Company: EROAD - Mobile Developer

<small>All opinions are my own</small>
' default, simple, sky, beige, serif, solarized, blood, moon, night, black, league or white).

***
### What I'll cover

- The current status of Mobile
- How Xamarin Forms works
- F# and Xamarin Forms
- Functional F# with Xamarin
- Some challenges with F#, .NET and mobile

***
### Quick re-cap of Mobile

- Android iOS, Windows
- Generally very similar
    - pages, buttons, camera, etc
- Technically rather different
    - iOS: UIButton, UIViewController
    - Android: Button, Fragments/Activites
- Different languages: Objective-C, swift, Java (Scala)

' It's a compilicated place
' Scala to compile on Android, apparently possible
' Google IO: Swift is catch up to Java
' Android might support Swift (rumor)
' iOS 2 day turn around to get into the App Store
' Android 2 hours to get into the Play Store

***
### Details about mobile

- Building multiple versions of the same app <strike>is expensive</strike> sucks
- Ignoring 'hybrid/web' cross platfrom solutions
    - These are slow 
    - Limited access to device APIs

' I did this full-time after leaving uni
' The subtitles of swapping between languages can cause bugs. 
' I created a bug, broke the app in production :(
' Update on iOS still took 1 day, must force update other apps

***
### The 2 (and a half) contenders (using F#)

- React Native
- Xamarin 
- Xamarin Forms 

***
### I'm choosing Xamairn Forms but
#### F# + Fable + React Native = possible

- F#  + Fable can be used with React Native 
- It's community supported
- JavaScript is involved (compiles to JavaScript)

***
### Overview of Xamarin

***
### Overview of Xamarin

- Mono allows .NET code to be executed on the device
- Xamarin have ported all the native device APIs to .NET
- write the core of the app in a shared layer
- (re) write only the UI for each platform

***
### Overview of Xamarin Forms

- Share the UI layer accross platforms
- Write each screen using abstract UI elements
- Can use xaml (subset of WPF) to describe UI elements
- UI elements are compiled to platform object ie Button -> UIButton

***

![alt text](images/xmarin+xamarinforms.png "") 

***
### Xamarin architecture (How it works in C#)

***
### Recap: MVVM is the standard architecture

- design pattern
- model -> view -> ViewModel
- Allows for unit testing of the view  

***
### MVVM is the standard architecture

![alt text](images/mvvm-pattern.jpg "") 

***
### Where's the F#

- Demo of a TODO app
- Use F# instead of C# 
- Will require OO 
- The only change made is the langauge

' Demo putting the app together
'   Show project structure and where things are
'   Add in view model 
'   highlight F# helping with types
'   update unit tests (Version 3 is better)
'   Possible to use Property based testing
'   - Not the best support with Xamarin Studio. 
'   - NUnit is the only stable (ish) framework

' Unit tests 
' Replace the core with Functional style

***
### Pros & Cons

- Pros
    - Great for enterprise (selling to your boss)
    - Should be able to buid anything
- Cons
    - View models hold state
        - variables are evil (well..source of bugs)
    - Communication between VMs can become complicated
    - not very declarative/functional
    - not leveraging all of F#'s power

' Facebook - web UI used VMs, they replaced with React

***
### Xamarin with a functional/reactive architecture

***
### A functional architecture

> Elm is a domain-specific programming language for declaratively creating web browser-based graphical user interfaces. Source: wikipedia
![alt text](images/elm.png "")

***
### A functional architecture

- A single model to repsent all data/state in the application
- a function(s) to update the state
- function to render the state
- function/type to model user actions

***
### The Elm Architecture
#### It's all about flow

<img src="images/elm-data-flow.jpg"  width="700" height="500" alt="Computer Hope">

***
### The Elm Architecture
#### Applied to Xamarin Forms

***
### The Elm Architecture
#### One state to rule them all

- Hold a single object with all state
    - no more mutation 
- a limited set of user actions that can be triggered
    - easier debugging
    - DU to represent the possible actions
- functions return a copy of the state
    - easier debugging
    - no more mutation 

***
### Welcome Gjallarhorn
### Much better, but how?

- Gjallarhorn is a framework
    - supports WPF
    - Supports Xamarin Forms
- Ouput bindings rather than HTML/CSS/JS 
- Continue to use Xmal for the describing the UI
- will model the uni-directional flow 
- Bindings + observables couples UI actions/updates

***
### Welcome Gjallarhorn
### Modelling state 

- Signals 
    - models a value that changes over time
    - handles notifcations of when the value is updated 
    - immutable
- Models 
    - everything a signal has
    - adds support for mutation

***
### A simple example 
#### Gjallarhorn Signals

- two buttons, increment - decrement
- output label
- very simple model
- uses functions to change the state
- Gjallarhorn handles everything else

***
### Power Feature
#### Time Travel

- Extend to support every change in the app
- Allows for 'time travelling' in the app
- Undo for free
- easy to persist

***
### Time Travel
#### How it's done

    type Model = int list

    let update msg model = function 
        | Increment = (fst model) + 1 :: model
        | Decrement = (fst model) - 1 :: model

    let bindToSource source (model : ISignal<Model>) =
        Binding.toView source "Current" (fst model)

***
### Time Travel
#### How it's done

- allows the developer to see every change that happend in the life of the app
- undo = tail model
- persist the model to save the entire state of the app

***
### Time Travel
#### How it's done

***
### Show me a real app!
#### Estate Watcher

- An app that I have been building
- We don't have time to go over everything in the code
- This is a prototype 

***
### A bigger app: Estate Watcher

- Track the changes of a listing: price & title
- A single watchlist for all your real estate listings
- Two websites currently supported (HTML scrapping)
    - TradeMe.co.nz
    - RealEstate.co.nz

***
### A bigger app: Estate Watcher

- Uses Gjallarhorn Async mutable to hold the single state
- Gjallarhorn Async mutable utilizers the mailbox processor
- sqlite3 db for permanant storage
- network calls (HTML scraping)

***
### A bigger app: Estate Watcher

Demo Time!  

*** 
### App Model

    type AddListingModel = {
        OutputMessage: string
        ItemAdded: bool
        IsValidatingItem: bool
        EntryText: string }
    type CurrentPage = ListingsPage | AddListingPage | ListingChangesPage | About
    type Model = { 
        Items : FullListing list 
        ShowRemovedListings: bool
        IsRefreshing: bool
        AddListingModel: AddListingModel option
        ListingChanges: ListingId option
        CurrentPage: CurrentPage }
***
### App Commands

    type RequestCompleted =
        | FetchItems of FullListing list
        | ItemSaved
        | RefrehedItems of FullListing list
        | ItemValidated of FullListing option
        | DeletedListing of ListingId
        | DeletedListingFailed of string

    and RequestAction = | RequstLoad | RequstRefresh
        | AddListingMessage of string
        | SetListingDetail of (NavigationDetails * ListingId)
        | DeleteListing of ListingId
        | ToggleShowRemoved 

     and Msg = 
        | RequestAction of RequestAction
        | RequestCompleted of RequestCompleted
        | ChangePage of ChangePage
        | Loop
        
***
### State/update function

    type StateManagement (navPage: NavigationPage, loadItems: unit -> unit, 
        saveListing, validateListing, refreshListings, deleteListing) =

        // .. rest of helper functions here

        let update (msg : Msg) (current : Model) = 
            match msg with
            | Msg.RequestAction ra -> requestAction ra current
            | Msg.RequestCompleted rc -> handleUpdateItems u current
            | Msg.ChangePage cp -> handleChangePage cp current
            | Msg.Loop -> current

        let initialModel: Model = // default starting value  

        let state = new AsyncMutable<Model>(initialModel)

        member __.Update msg = 
            update msg |> state.Update |> ignore

***
### Add a listing page

    let addListing source (model: ISignal<Model>) = 
        model |> Signal.map (fun x ->  x.AddListingModel.OutputMessage)
        |> Binding.toView source "Output"
        
        model |> Signal.map (fun x -> x.AddListingModel.EntryText)
        |> Binding.toView source "ListingText"
            
        // Response stubbed for slide
        let isNotLoadingContent = model |> Signal.map (fun x -> true)
        let isOnAddListingPage = model |> Signal.map (fun x -> true)
        let canExecute = Signal.map2 (&&) isNotLoadingContent isOnAddListingPage

        [source |> Binding.createMessageParamChecked "TrackCommand" 
            canExecute (fun entry -> RequestAction <| AddListingMessage entry)]

***
### A look at the code + Demo

To the demo

***
### Gjallarhorn

- handles notifications 
- handles state
- declaritive

*** 
### Moble + F# + libraries

- A little note

*** 
### Over to libraries

- F# mobile developers want to use your libraris!
    - Incompatibilty error :(
- APIs on mobile are different to Desktop :(
- PCLs were meant to solve this
    - only allow code with the intersection of APIs
- Getting the right profile is hard
- Some magic when the APIs don't line up (it's possible though)

*** 
### F# and libraries
#### How you can help spread F# to mobile

- Consider mobile, use a PCL
- PCL: Target a mobile profile
    - profile 259 (generally the most sensible)
- .netstandard is the new future
    - .netstand 2.0 is coming
    - will solve all out problems!
- If only a few APIs don't fit ask the mobile community
    - `Bait and switch` might be able to solve this
    
*** 
### Over to libraries

- https://blogs.msdn.microsoft.com/dotnet/2016/09/26/introducing-net-standard/
- use PCLs now to target mobile specifically
- use .netstandard (1.0 - 1.6) if building a library that might be useful for mobile
- use .net-standard 2.0 when it comes out
    - .netstandard includes Xamarin APIs

*** 
### Wrapping up

- Mobile is challenging
- Xamarin Forms + F# + MVVM 
    - stable
    - enterprise ready
    - Well understood architecture
- Xamarin Forms + F# + Gjallarhorn
    - The future!
    - Controlled state
    - More declarative
- If writing an F# library
    - Think about mobile
    - use PCL/.netstandard