module Gift.Client.View

open Domain
open Gift.Client.Components

let render (model: Model) (dispatch: Msg -> unit) = Navbar.Navbar model dispatch