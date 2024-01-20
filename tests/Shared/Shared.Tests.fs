module Shared.Tests

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

open Shared

let shared =
    testList "Shared" [ testCase "Sample test" <| fun _ -> Expect.equal false false "Should be false" ]