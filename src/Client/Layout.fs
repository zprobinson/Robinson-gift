module Gift.Client.Pages.Layout

open Feliz
open Gift.Client.Server
open Fable.Core

type Page =
    | Login
    | HelloWorld

module internal NavButton =

    [<ReactComponent>]
    let private navBtn (page: Page) (activePage: Page) (setPage: Page -> unit) =
        Html.button
            [ prop.className (
                  if page = activePage then
                      "rounded-lg bg-cyan-500 px-2 py-1 text-teal-50"
                  else
                      "rounded-lg px-2 py-1"
              )
              prop.text (
                  match page with
                  | Login -> "Login"
                  | HelloWorld -> "Hello World"
              )
              prop.onClick (fun _ -> setPage page) ]

    [<ReactComponent>]
    let HelloWorld = navBtn HelloWorld

    [<ReactComponent>]
    let Login = navBtn Login

[<ReactComponent>]
let view () =
    let (page, setPage) = React.useState Login

    let pageRender =
        match page with
        | Login ->
            Html.div
                [ prop.children
                      [ Html.button
                            [ prop.onClick (fun _ ->
                                  printfn "login clicked"

                                  authApi.login { Name = ""; Password = "" }
                                  |> Async.StartAsPromise
                                  |> Promise.tap (function
                                      | Some _ -> setPage HelloWorld
                                      | None -> ())
                                  |> Promise.tap (fun x -> printfn "resolved: %A" x)
                                  |> Promise.catchEnd (printfn "error: %A"))

                              prop.text "click to login" ] ] ]
        | HelloWorld -> Html.div [ prop.text "Hello World" ]

    Html.div
        [ prop.className "h-screen bg-gradient-to-r from-violet-600 to-indigo-600 text-slate-100"
          prop.children
              [ Html.div
                    [ prop.className "pt-16 flex justify-center gap-5"
                      prop.children [ NavButton.HelloWorld page setPage; NavButton.Login page setPage ] ]
                Html.div [ prop.className "mx-48"; prop.children [ pageRender ] ] ] ]