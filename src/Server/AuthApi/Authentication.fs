[<AutoOpen>]
module AuthApi

open System
open Shared

type AuthApi() =

    let testUser: Person =
        { Id = Guid.NewGuid()
          FirstName = "Zach"
          LastName = "Robinson"
          Birthday = DateOnly(1991, 01, 17) }

    member this.login =
        fun command -> async {
            match command.Password.ToLower() with
            | "password" ->
                return
                    ({ testUser with
                        FirstName = command.Name }
                     |> Ok)
            | _ -> return Error "Invalid username or password"
        }

    member this.logout = fun _ -> async { return () }

    member this.build() : IAuthApi =
        { login = this.login
          logout = this.logout }