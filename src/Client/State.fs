module Gift.Client.State

open Elmish
open Domain

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
            (fun (username: string, password: string) -> async {
                do! Async.Sleep 1000

                match password with
                | "password" -> return username
                | _ -> return invalidOp "Yo dawg, that ain't right"
            })
            (username, password)
            (fun name -> LoginSucceeded name)
            (fun exn -> LoginFailed exn.Message)

    | LoginSucceeded username ->
        { model with
            User = Some username
            Page = CurrentPage.Home
            LoginLoading = false },
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