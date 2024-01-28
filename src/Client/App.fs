module App

open Browser
open Feliz

open Elmish
open Elmish.React

open Fable.Core.JsInterop

importSideEffects "./index.css"

#if DEBUG
open Elmish.Debug
open Elmish.HMR
open Gift.Client.Pages
#endif

ReactDOM.createRoot (document.getElementById "root")
|> fun x -> x.render (Layout.view ())
// |> fun x -> x.render (Index.View())

// Program.mkProgram Index.init Index.update Index.view
// #if DEBUG
// |> Program.withConsoleTrace
// #endif
// |> Program.withReactSynchronous "elmish-app"
// #if DEBUG
// |> Program.withDebugger
// #endif
// |> Program.run