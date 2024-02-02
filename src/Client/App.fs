module App

open Elmish
open Gift.Client

Fable.Core.JsInterop.importSideEffects "./index.css"
Fable.Core.JsInterop.importSideEffects "@fontsource/inter"

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram State.init State.update View.render
|> Program.withReactSynchronous "root"
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
#endif
|> Program.run