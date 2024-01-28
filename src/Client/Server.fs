module Gift.Client.Server

open Fable.Remoting.Client
open Shared

let authApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<IAuthApi>