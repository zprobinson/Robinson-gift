module Gift.Client.Pages.Home.View

open Feliz
open Gift.Client.Domain
open Shared
open Gift.Client.Components

[<ReactComponent>]
let Home (person: Person) (model: Model) (dispatch: Msg -> unit) =
    Html.div
        [ prop.className "mt-10 text-4xl font-sans flex justify-center"
          prop.children
              [ Html.h1 [ prop.children [ Html.text (sprintf "Hello %s" person.FirstName) ] ]
                Card.Card() ] ]