module Gift.Client.Pages.Login.Domain

type Model =
    { Username: string
      Password: string
      Error: string option }

type Msg =
    | UsernameChanged of string
    | PasswordChanged of string
    | Login
    | LoginSuccess
    | LoginFailure of string