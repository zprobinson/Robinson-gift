module Gift.Client.State

open Elmish
open Domain

let init () = { Empty = () }, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Empty -> model, Cmd.none