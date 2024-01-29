module Client.Tests

open Fable.Mocha

open Shared

let client =
    testList "Client" [ testCase "Test cases" <| fun _ -> Expect.equal false false "Test case" ]

let all =
    testList
        "All"
        [
#if FABLE_COMPILER // This preprocessor directive makes editor happy
          Shared.Tests.shared
#endif
          client ]

[<EntryPoint>]
let main _ = Mocha.runTests all