module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn
open Microsoft.AspNetCore.Http

open Giraffe
open Microsoft.Extensions.DependencyInjection

let authApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<AuthApi>().build ())
    |> Remoting.buildHttpHandler

let personApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<PersonApi>().build ())
    |> Remoting.buildHttpHandler

let giftApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<GiftApi>().build ())
    |> Remoting.buildHttpHandler

let configureServices (services: IServiceCollection) =
    services
        .AddSingleton<AuthApi>()
        .AddSingleton<PersonApi>()
        .AddSingleton<GiftApi>()

let app = application {
    use_router (choose [ authApi; personApi; giftApi ])
    memory_cache
    use_static "public"
    service_config configureServices
    use_gzip
}

[<EntryPoint>]
let main _ =
    run app
    0