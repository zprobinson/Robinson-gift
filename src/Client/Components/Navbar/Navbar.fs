module Gift.Client.Components.Navbar

open Feliz
open Gift.Client.Domain

[<ReactComponent>]
let private Template (children: ReactElement list) authButton =
    let NavLinkGroup =
        Html.div
            [ prop.className "flex flex-1 items-center justify-start"
              prop.children
                  [ Html.div[prop.className "flex space-x-4"
                             prop.children children] ] ]

    Html.nav
        [ prop.className "bg-gray-800 px-2"
          prop.children
              [ Html.div
                    [ prop.className "relative flex h-16 items-center mx-6"
                      prop.children [ NavLinkGroup; authButton ] ] ] ]

[<ReactComponent>]

[<Literal>]
let activeNavStyle =
    "bg-gray-900 text-white px-3 py-2 rounded-md text-sm font-medium"

[<Literal>]
let inactiveNavStyle =
    "text-gray-300 hover:bg-gray-700 hover:text-white px-3 py-2 rounded-md text-sm font-medium"

[<ReactComponent>]
let private NavLink (dispatch: Msg -> unit) (text: string) active =
    Html.a[prop.href "#"
           prop.className (if active then activeNavStyle else inactiveNavStyle)
           prop.onClick (fun _ -> dispatch (Navigate(CurrentPage.fromString text)))
           prop.children[Html.text text]]

[<ReactComponent>]
let private AuthButton model dispatch =
    let action =
        match model.User with
        | Some _ -> Logout
        | None -> Login

    let text =
        match model.User with
        | Some _ -> "Logout"
        | None -> "Login"

    Html.button
        [ prop.className inactiveNavStyle
          prop.onClick (fun _ -> dispatch action)
          prop.text text ]

[<ReactComponent>]
let Navbar (model: Model) (dispatch: Msg -> unit) =
    let currentPage = model.Page
    let currentUser = model.User

    let links =
        [ "Home"
          if currentUser |> Option.isSome then
              yield! [ "MyGifts"; "Family" ] ]
        |> List.map (fun name -> (name, CurrentPage.fromString name = currentPage))
        |> List.map (fun (name, active) -> NavLink dispatch name active)

    let authButton =
        Html.div
            [ prop.className "flex flex-1 items-center justify-end"
              prop.children [ AuthButton model dispatch ] ]

    Template links authButton