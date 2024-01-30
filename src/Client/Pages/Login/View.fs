module Gift.Client.Pages.Login.View

open Feliz
open Gift.Client.Domain

[<ReactComponent>]
let private StyledInput (xs: IReactProperty list) =
    let properties =
        prop.className "border-2 border-gray-300 bg-white h-10 px-5 rounded-lg text-sm focus:outline-none"
        :: xs

    Html.input properties

[<ReactComponent>]
let private StyledButton (xs: IReactProperty list) =
    let properties =
        prop.className "bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-md w-full"
        :: xs

    Html.button properties

[<ReactComponent>]
let Login model (dispatch: Msg -> unit) =
    let appHeading =
        Html.h1
            [ prop.className "text-3xl my-10 font-bold text-center text-slate-600"
              prop.children [ Html.text "Please Log In" ] ]

    let loginForm =
        Html.form
            [ prop.className "flex flex-col gap-3 justify-center justify-self-center items-center items-stretch"
              prop.onSubmit (fun _ -> dispatch Login)
              prop.children
                  [ StyledInput [ prop.type' "text"; prop.placeholder "Username"; prop.autoFocus true ]
                    StyledInput [ prop.type' "password"; prop.placeholder "Password" ]
                    StyledButton [ prop.type' "submit"; prop.children [ Html.text "Login" ] ] ] ]

    Html.div
        [ prop.className "flex justify-center"
          prop.children
              [ Html.div
                    [ prop.className "flex flex-col w-1/3 h-screen"
                      prop.children [ appHeading; loginForm ] ] ] ]