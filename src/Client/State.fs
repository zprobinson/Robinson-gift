module Gift.Client.State

open Elmish
open Domain
open Server

let init () =
    { Page = CurrentPage.Home
      User = None
      LoginError = None
      LoginLoading = false },
    Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Navigate page -> { model with Page = page }, Cmd.none

    | Login(username, password) ->
        { model with LoginLoading = true },
        Cmd.OfAsync.either
            authApi.login
            { Name = username; Password = password }
            (fun response ->
                match response with
                | Ok person -> LoginSucceeded person
                | Error err -> LoginFailed err)
            (fun exn -> LoginFailed exn.Message)

    | LoginSucceeded person ->
        { model with
            User = Some person
            Page = CurrentPage.Home
            LoginLoading = false
            LoginError = None },
        Cmd.none

    | LoginFailed err ->
        { model with
            LoginError = Some err
            LoginLoading = false },
        Cmd.none

    | Logout ->
        { model with
            User = None
            Page = CurrentPage.Home },
        Cmd.none