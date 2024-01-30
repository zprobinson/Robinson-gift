module Gift.Client.State

open Elmish
open Domain

let init () =
    { Page = CurrentPage.Home; User = None }, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Navigate page -> { model with Page = page }, Cmd.none

    | Login ->
        { model with
            User = Some "test"
            Page = CurrentPage.MyGifts },
        Cmd.none

    | Logout ->
        { model with
            User = None
            Page = CurrentPage.Home },
        Cmd.none