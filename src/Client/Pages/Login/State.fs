module Gift.Client.Pages.Login.State

open Domain
open Elmish
open Gift.Client.Server

let init () =
    { Username = ""
      Password = ""
      Error = None },
    Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | UsernameChanged username -> { model with Username = username }, Cmd.none
    | PasswordChanged password -> { model with Password = password }, Cmd.none
    | Login ->
        model,
        Cmd.OfAsync.either
            authApi.login
            { Name = model.Username
              Password = model.Password }
            (fun _ -> LoginSuccess)
            (fun exn -> LoginFailure exn.Message)
    | LoginSuccess -> model, Cmd.none
    | LoginFailure error -> { model with Error = Some error }, Cmd.none