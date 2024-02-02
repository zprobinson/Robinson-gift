module Gift.Client.Components.Card

open Feliz
open Gift.Client.Domain

[<ReactComponent>]
let Card () =
    Html.div
        [ prop.className "bg-white shadow-lg rounded-lg overflow-hidden"
          prop.children
              [ Html.div
                    [ prop.className "bg-cover bg-center h-56"
                      prop.style [ style.backgroundImage "url('https://source.unsplash.com/random')" ]
                      prop.children [] ]
                Html.div
                    [ prop.className "p-4"
                      prop.children
                          [ Html.h1 [ prop.className "text-2xl font-bold"; prop.children [ Html.text "Title" ] ]
                            Html.p
                                [ prop.className "text-gray-600 text-sm mt-1"
                                  prop.children [ Html.text "Description" ] ] ] ] ] ]