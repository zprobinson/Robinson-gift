module Gift.Client.Domain

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
      User: string option }

type Msg =
    | Navigate of CurrentPage
    | Login
    | Logout