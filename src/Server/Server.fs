module Server

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Saturn
open System

open Shared
open Giraffe

module Storage =
    let todos = ResizeArray()

    let addTodo (todo: Todo) =
        if Todo.isValid todo.Description then
            todos.Add todo
            Ok()
        else
            Error "Invalid todo"

    do
        addTodo (Todo.create "Create new SAFE project") |> ignore
        addTodo (Todo.create "Write your app") |> ignore
        addTodo (Todo.create "Ship it!!!") |> ignore

let todosApi =
    { getTodos = fun () -> async { return Storage.todos |> List.ofSeq }
      addTodo =
        fun todo -> async {
            return
                match Storage.addTodo todo with
                | Ok() -> todo
                | Error e -> failwith e
        } }


let webApp =
    Remoting.createApi ()
    |> Remoting.fromValue todosApi
    |> Remoting.buildHttpHandler

let authApi =
    Remoting.createApi ()
    |> Remoting.fromValue AuthApi.authProtocol
    |> Remoting.buildHttpHandler

let personApi =
    Remoting.createApi ()
    |> Remoting.fromValue PersonApi.personProtocol
    |> Remoting.buildHttpHandler

let giftApi =
    Remoting.createApi ()
    |> Remoting.fromValue GiftApi.giftProtocol
    |> Remoting.buildHttpHandler

let app = application {
    use_router (choose [ webApp; authApi; personApi; giftApi ])
    memory_cache
    use_static "public"
    use_gzip
}

[<EntryPoint>]
let main _ =
    run app
    0