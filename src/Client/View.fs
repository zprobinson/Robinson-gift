module Gift.Client.View

open Feliz
open Domain
open Gift.Client.Components

let render (model: Model) (dispatch: Msg -> unit) =
    let activePage =
        match model.Page, model.User with
        | Home, Some user -> "Home: You are logged in as " + user |> Html.text

        | MyGifts, Some user -> "MyGifts" |> Html.text

        | Family, Some user -> "Family" |> Html.text

        | _, None -> Pages.Login.View.Login model dispatch

    React.fragment [ Navbar.Navbar model dispatch; activePage ]