module Index

open Elmish
open Shared

type Model = { Input: string; User: Person option }

open Feliz

[<ReactComponent>]
let private todoAction () =
    let text, setText = React.useState ""

    Html.div
        [ prop.className "flex flex-col sm:flex-row mt-4 gap-4"
          prop.children
              [ Html.input
                    [ prop.className
                          "shadow appearance-none border rounded w-full py-2 px-3 outline-none focus:ring-2 ring-teal-300 text-grey-darker"
                      prop.value text
                      prop.placeholder "What needs to be done?"
                      prop.autoFocus true
                      prop.onChange setText
                      prop.onKeyPress (fun ev ->
                          if ev.key = "Enter" then
                              printfn "enter key pressed"
                              printfn "text is %s" text) ]
                Html.button
                    [ prop.className
                          "flex-no-shrink p-2 px-12 rounded bg-teal-600 outline-none focus:ring-2 ring-teal-300 font-bold text-white hover:bg-teal disabled:opacity-30 disabled:cursor-not-allowed"
                      prop.disabled (false)
                      prop.onClick (fun _ -> printfn "%s" text)
                      prop.text "Add" ] ] ]

[<ReactComponent>]
let View () =
    Html.section
        [ prop.className "h-screen w-screen"
          prop.style
              [ style.backgroundSize "cover"
                style.backgroundImageUrl "https://unsplash.it/1200/900?random"
                style.backgroundPosition "no-repeat center center fixed" ]

          prop.children
              [ Html.a
                    [ prop.href "https://safe-stack.github.io/"
                      prop.className "absolute block ml-12 h-12 w-12 bg-teal-300 hover:cursor-pointer hover:bg-teal-400"
                      prop.children [ Html.img [ prop.src "/favicon.png"; prop.alt "Logo" ] ] ]

                Html.div
                    [ prop.className "flex flex-col items-center justify-center h-full"
                      prop.children
                          [ Html.h1
                                [ prop.className "text-center text-5xl font-bold text-white mb-3 rounded-md p-4"
                                  prop.text "robinson_gift" ]
                            todoAction () ] ] ] ]