module Gift.Client.Domain

open Shared

type CurrentPage =
    | Home
    | MyGifts
    | Family

    static member fromString(s: string) =
        match s.ToLower() with
        | "home" -> Home
        | "mygifts" -> MyGifts
        | "family" -> Family
        | _ -> failwithf "Unknown page: %s" s

type Model =
    { Page: CurrentPage
      User: Person option
      (* Login Page *)
      LoginLoading: bool
      LoginError: string option }

type Msg =
    | Navigate of CurrentPage
    | Login of string * string
    | LoginSucceeded of Person
    | LoginFailed of string
    | Logout