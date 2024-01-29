module Gift.Client.Domain

type CurrentPage =
    | Home
    | Login
    | MyGifts
    | Family

type Model = { Empty: unit }

type Msg = Empty