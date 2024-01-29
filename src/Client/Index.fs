module Gift.Client.Index

open Elmish
open Gift.Client.Pages
open Domain

type Msg = Empty

let update msg model =
    match msg with
    | Empty -> model, Cmd.none

let render (model: Model) (dispatch: Msg -> unit) = Layout.view ()