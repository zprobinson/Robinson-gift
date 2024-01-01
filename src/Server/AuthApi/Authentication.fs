module AuthApi

open Shared

let authProtocol: IAuthApi =
    { login = Login.login
      logout = Logout.logout }