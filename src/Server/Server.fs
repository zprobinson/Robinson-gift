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

let authProtocol: IAuthApi =
    { login = fun _ -> async { return None }
      logout = fun _ -> async { return () } }

let authApi =
    Remoting.createApi ()
    |> Remoting.fromValue authProtocol
    |> Remoting.buildHttpHandler

let personProtocol: IPersonApi =
    { getPeople = fun _ -> async { return [] }
      getPerson = fun _ -> async { return None }
      addPerson =
        fun _ -> async {
            return
                { Id = Guid.Empty
                  FirstName = ""
                  LastName = ""
                  Birthday = DateOnly.MinValue }
        }
      updatePerson =
        fun _ -> async {
            return
                { Id = Guid.Empty
                  FirstName = ""
                  LastName = ""
                  Birthday = DateOnly.MinValue }
        }
      deletePerson = fun _ -> async { return () } }

let personApi =
    Remoting.createApi ()
    |> Remoting.fromValue personProtocol
    |> Remoting.buildHttpHandler

let giftProtocol: IGiftApi =
    { gifts = fun _ -> async { return [] }
      gift = fun _ -> async { return None }
      addGift =
        fun _ -> async {
            return
                { Id = Guid.Empty
                  Person =
                    { Id = Guid.Empty
                      FirstName = ""
                      LastName = ""
                      Birthday = DateOnly.MinValue }
                  Name = ""
                  Description = None
                  Url = None
                  Image = [||]
                  Price = None }
        }
      updateGift =
        fun _ -> async {
            return
                { Id = Guid.Empty
                  Person =
                    { Id = Guid.Empty
                      FirstName = ""
                      LastName = ""
                      Birthday = DateOnly.MinValue }
                  Name = ""
                  Description = None
                  Url = None
                  Image = [||]
                  Price = None }
        }
      deleteGift = fun _ -> async { return () }
      reserveGift =
        fun _ -> async {
            return
                { Id = Guid.Empty
                  Gift =
                    { Id = Guid.Empty
                      Person =
                        { Id = Guid.Empty
                          FirstName = ""
                          LastName = ""
                          Birthday = DateOnly.MinValue }
                      Name = ""
                      Description = None
                      Url = None
                      Image = [||]
                      Price = None }
                  ReservedBy =
                    { Id = Guid.Empty
                      FirstName = ""
                      LastName = ""
                      Birthday = DateOnly.MinValue }
                  ReservedAt = DateTimeOffset.MinValue }
        }
      unreserveGift = fun _ -> async { return () } }

let giftApi =
    Remoting.createApi ()
    |> Remoting.fromValue giftProtocol
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