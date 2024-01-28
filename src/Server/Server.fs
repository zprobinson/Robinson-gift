module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn
open Microsoft.AspNetCore.Http

open Giraffe
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open System
open Shared

let personDocs = Docs.createFor<IPersonApi> ()

let personApiDocs =
    Remoting.documentation
        "Person Api"
        [ personDocs.route <@ fun api -> api.getPerson @>
          |> personDocs.alias "Get person"
          |> personDocs.description "Returns a person" ]

let authDocs = Docs.createFor<IAuthApi> ()

let authApiDocs =
    Remoting.documentation
        "Auth Api"
        [ authDocs.route <@ fun api -> api.login @>
          |> authDocs.alias "Login"
          |> authDocs.description "Returns a token" ]

let authApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<AuthApi>().build ())
    |> Remoting.withDocs "/api/auth/docs" authApiDocs
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildHttpHandler

let personApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<PersonApi>().build ())
    |> Remoting.withDocs "/api/person/docs" personApiDocs
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildHttpHandler

let giftApi =
    Remoting.createApi ()
    |> Remoting.fromContext (fun (ctx: HttpContext) -> ctx.GetService<GiftApi>().build ())
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildHttpHandler

let configureServices (services: IServiceCollection) =
    services
        .AddGiraffe()
        .AddSingleton<AuthApi>()
        .AddSingleton<PersonApi>()
        .AddSingleton<GiftApi>()
// |> ignore

let configureApp (app: IApplicationBuilder) =
    app.UseGiraffe(choose [ authApi; personApi; giftApi ])

let app = application {
    use_router (choose [ authApi; personApi; giftApi ])
    memory_cache
    use_static "public"
    service_config configureServices
    use_gzip
}

[<EntryPoint>]
let main _ =
    // WebHostBuilder()
    //     .UseKestrel()
    //     .Configure(Action<IApplicationBuilder> configureApp)
    //     .ConfigureServices(configureServices)
    //     .Build()
    //     .Run()
    run app
    0