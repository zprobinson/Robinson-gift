module Gift.Client.View

open Feliz
open Domain
open Gift.Client.Components
open Shared

let render (model: Model) (dispatch: Msg -> unit) =
    let activePage =
        match model.Page, model.User with
        | Home, Some user -> Pages.Home.View.Home user model dispatch

        | MyGifts, Some user -> "MyGifts" |> Html.text

        | Family, Some user -> "Family" |> Html.text

        | _, None -> Pages.Login.View.Login model dispatch

    React.fragment [ Navbar.Navbar model dispatch; activePage ]