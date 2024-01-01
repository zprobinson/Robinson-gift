module internal Login

open System
open Shared

let private testUser: Person =
    { Id = Guid.NewGuid()
      FirstName = "Zach"
      LastName = "Robinson"
      Birthday = DateOnly.Parse "1991-01-17" }

let login: Login = fun _ -> async { return Some testUser }