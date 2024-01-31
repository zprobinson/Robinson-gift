[<RequireQualifiedAccess>]
module Result

/// Fable does not support Result.isOk so it is reimplemented here.
let isOk result =
    match result with
    | Ok _ -> true
    | _ -> false

/// Fable does not support Result.isError so it is reimplemented here.
let isError result =
    match result with
    | Ok _ -> false
    | _ -> true