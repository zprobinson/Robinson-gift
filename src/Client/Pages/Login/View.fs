module Gift.Client.Pages.Login.View

open Feliz
open Gift.Client.Domain
open Shared

[<ReactComponent>]
let private StyledInput (xs: IReactProperty list) =
    let properties =
        prop.className "border-2 border-gray-300 bg-white h-10 px-5 rounded-lg text-sm focus:outline-none"
        :: xs

    Html.input properties

[<ReactComponent>]
let private StyledButton (xs: IReactProperty list) =
    let properties =
        prop.className
            "bg-blue-500 hover:bg-blue-700 disabled:bg-blue-300 text-white font-bold py-2 px-4 rounded-md w-full"
        :: xs

    Html.button properties

let private validateEmpty (value: string) =
    if value.Trim().Length = 0 then
        Error "This field is required"
    else
        Ok ""

[<ReactComponent>]
let Login model (dispatch: Msg -> unit) =
    let username, setUsername = React.useState ""
    let password, setPassword = React.useState ""

    let validForm =
        validateEmpty username
        |> Result.bind (fun _ -> validateEmpty password)
        |> Result.isOk

    let appHeading =
        Html.h1
            [ prop.className "text-3xl my-10 font-bold text-center text-slate-600"
              prop.children [ Html.text "Please Log In" ] ]

    let formError =
        match model.LoginError with
        | Some err -> Html.p [ prop.className "text-red-400 text-center"; prop.children [ Html.text err ] ]
        | None -> React.fragment []

    let loginForm =
        Html.form
            [ prop.className "flex flex-col gap-3 justify-center justify-self-center items-center items-stretch"
              prop.onSubmit (fun event ->
                  event.preventDefault ()
                  dispatch <| Login(username, password))
              prop.children
                  [ formError
                    StyledInput
                        [ prop.type' "text"
                          prop.placeholder "Username"
                          prop.autoFocus true
                          prop.value username
                          prop.onChange setUsername ]
                    StyledInput
                        [ prop.type' "password"
                          prop.placeholder "Password"
                          prop.value password
                          prop.onChange setPassword ]
                    StyledButton
                        [ prop.type' "submit"
                          prop.disabled (not validForm || model.LoginLoading)
                          prop.children [ Html.text (if model.LoginLoading then "Loading..." else "Login") ] ] ] ]

    Html.div
        [ prop.className "flex justify-center"
          prop.children
              [ Html.div
                    [ prop.className "flex flex-col w-1/3 h-screen"
                      prop.children [ appHeading; loginForm ] ] ] ]