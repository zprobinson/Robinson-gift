module Server.Tests

open Expecto

open Shared
open Server

let server =
    testList "Server" [ testCase "Sample test" <| fun _ -> Expect.equal false false "Test case" ]

let all = testList "All" [ Shared.Tests.shared; server ]

[<EntryPoint>]
let main _ = runTestsWithCLIArgs [] [||] all