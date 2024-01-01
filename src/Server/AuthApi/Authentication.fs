[<AutoOpen>]
module AuthApi

open System
open Shared

type AuthApi() =

    let testUser: Person =
        { Id = Guid.NewGuid()
          FirstName = "Zach"
          LastName = "Robinson"
          Birthday = DateOnly.Parse "1991-01-17" }

    member this.login = fun _ -> async { return Some testUser }

    member this.logout = fun _ -> async { return () }

    member this.build() : IAuthApi =
        { login = this.login
          logout = this.logout }