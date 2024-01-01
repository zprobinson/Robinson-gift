namespace Shared

[<AutoOpen>]
type LoginPerson = { Name: string; Password: string }

type Login = LoginPerson -> Async<Person option>
type Logout = unit -> Async<unit>

type IAuthApi =
    { login: LoginPerson -> Async<Person option>
      logout: unit -> Async<unit> }