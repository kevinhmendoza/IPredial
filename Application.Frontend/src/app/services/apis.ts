export class ApisRestBase {
  public static UrlServer = "http://localhost:59590/";
}

export class ApisRestUser extends ApisRestBase {
  public static PostLogin = ApisRestUser.UrlServer + "Token";
  public static PostLogout = ApisRestUser.UrlServer + "api/Account/Logout";
  public static GetPermissionUser = ApisRestUser.UrlServer + "api/User/Permission";
  public static GetUser = ApisRestUser.UrlServer + "api/User";
  public static PostChangePassword = ApisRestUser.UrlServer + "api/User/ChangePassword";
}


export class ApisRestGestionUsers extends ApisRestBase {
  public static GetAllUsers = ApisRestBase.UrlServer + "api/Gestion/User/Get/All";
  public static PostRegisterUser = ApisRestBase.UrlServer + "api/Gestion/User/Register";
}
