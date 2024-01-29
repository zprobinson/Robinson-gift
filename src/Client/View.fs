module Gift.Client.View

open Domain
open Gift.Client.Pages

let render (model: Model) (dispatch: Msg -> unit) = Layout.view ()