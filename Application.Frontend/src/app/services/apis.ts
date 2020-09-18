export class ApisRestBase {
  public static UrlServer = "http://localhost:8053/";
}

export class ApisRestUser extends ApisRestBase {
  public static PostLogin = ApisRestUser.UrlServer + "Token";
  public static PostLogout = ApisRestUser.UrlServer + "api/Account/Logout";
  public static GetPermissionUser = ApisRestUser.UrlServer + "api/User/Permission";
  public static GetUser = ApisRestUser.UrlServer + "api/User";
  public static PostChangePassword = ApisRestUser.UrlServer + "api/User/ChangePassword";
}
