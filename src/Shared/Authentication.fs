namespace Shared

[<AutoOpen>]
type LoginPerson = { Name: string; Password: string }

type IAuthApi = {
    login: LoginPerson -> Async<Person option>
    logout: unit -> Async<unit>
}