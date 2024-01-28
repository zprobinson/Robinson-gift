module Index

open Elmish
open Fable.Remoting.Client
open Shared

type Model = { Input: string; User: Person option }

type Msg =
    | SetInput of string
    | Login of LoginPerson
    | UserLoggedIn of Person option
    | Logout
    | UserLoggedOut

let authApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IAuthApi>

let init () =
    let model = { User = None; Input = "" }
    model, Cmd.none

let update msg model =
    match msg with
    | SetInput input -> { model with Input = input }, Cmd.none
    | Login person ->
        Browser.Dom.console.log ("login", [| person |])
        model, Cmd.OfAsync.perform authApi.login person UserLoggedIn
    | UserLoggedIn user ->
        Browser.Dom.console.log ("user logged in", [| user |])
        { model with User = user }, Cmd.none
    | Logout -> model, Cmd.OfAsync.perform authApi.logout () (fun _ -> UserLoggedOut)
    | UserLoggedOut -> { model with User = None }, Cmd.none

open Feliz

let private todoAction (model: Model) dispatch =
    Html.div
        [ prop.className "flex flex-col sm:flex-row mt-4 gap-4"
          prop.children
              [ Html.input
                    [ prop.className
                          "shadow appearance-none border rounded w-full py-2 px-3 outline-none focus:ring-2 ring-teal-300 text-grey-darker"
                      prop.value model.Input
                      prop.placeholder "What needs to be done?"
                      prop.autoFocus true
                      prop.onChange (SetInput >> dispatch)
                      prop.onKeyPress (fun ev ->
                          if ev.key = "Enter" then
                              Browser.Dom.console.log "enter key pressed") ]
                Html.button
                    [ prop.className
                          "flex-no-shrink p-2 px-12 rounded bg-teal-600 outline-none focus:ring-2 ring-teal-300 font-bold text-white hover:bg-teal disabled:opacity-30 disabled:cursor-not-allowed"
                      prop.disabled (false)
                      prop.onClick (fun _ ->
                          Login
                              { Name = model.Input
                                Password = "password" }
                          |> dispatch)
                      prop.text "Add" ] ] ]

let private todoList model dispatch =
    Html.div
        [ prop.className "bg-white/80 rounded-md shadow-md p-4 w-5/6 lg:w-3/4 lg:max-w-2xl"
          prop.children
              [ Html.ol [ prop.className "list-decimal ml-6"; prop.children [] ]

                todoAction model dispatch ] ]

let view model dispatch =
    Html.section
        [ prop.className "h-screen w-screen"
          prop.style
              [ style.backgroundSize "cover"
                style.backgroundImageUrl "https://unsplash.it/1200/900?random"
                style.backgroundPosition "no-repeat center center fixed" ]

          prop.children
              [ Html.a
                    [ prop.href "https://safe-stack.github.io/"
                      prop.className "absolute block ml-12 h-12 w-12 bg-teal-300 hover:cursor-pointer hover:bg-teal-400"
                      prop.children [ Html.img [ prop.src "/favicon.png"; prop.alt "Logo" ] ] ]

                Html.div
                    [ prop.className "flex flex-col items-center justify-center h-full"
                      prop.children
                          [ Html.h1
                                [ prop.className "text-center text-5xl font-bold text-white mb-3 rounded-md p-4"
                                  prop.text "robinson_gift" ]
                            todoList model dispatch ] ] ] ]