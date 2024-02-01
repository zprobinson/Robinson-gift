namespace Shared

[<AutoOpen>]
type LoginPerson = { Name: string; Password: string }

type Login = LoginPerson -> Async<Result<Person, string>>
type Logout = unit -> Async<unit>

type IAuthApi = { login: Login; logout: Logout }