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


export class ApisRestGestionUsers extends ApisRestBase {
  public static GetAllUsers = ApisRestBase.UrlServer + "api/User/Get/All";
  public static PostRegisterUser = ApisRestBase.UrlServer + "api/Gestion/User/Register";
  public static PostToggleUser = ApisRestBase.UrlServer + "api/Gestion/User/ChangeState";
}

export class ApisRestModificarUserAndTercero extends ApisRestBase {
  public static GetTerceroAndUser = ApisRestModificarUserAndTercero.UrlServer + "api/Modificar/UserWithTercero";
  public static PostModificarTercero = ApisRestModificarUserAndTercero.UrlServer + "api/Modificar/UserWithTercero";
}

export class ApisRestCambiarEstadoUserAndTercero extends ApisRestBase {
  public static GetTerceroAndUser(idTercero): string { return ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/Cambiar/Estado/UserWithTercero" + idTercero; }
  public static PostModificarTercero = ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/Cambiar/Estado/UserWithTercero";
}

export class ApisRestEstadoCuenta extends ApisRestBase {
  public static PostEstadoCuenta = ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/EstadoCuenta/Consultar";
}

export class ApisRestGestionPermisos extends ApisRestBase {
  public static GetModules = ApisRestGestionPermisos.UrlServer + "api/Roles/Modules";
  public static PostGetPermission = ApisRestGestionPermisos.UrlServer + "api/Roles/Get/Permission";
  public static PostGestionPermission = ApisRestGestionPermisos.UrlServer + "api/Roles/Set/Permission";
  public static GetUser = ApisRestGestionPermisos.UrlServer + "api/Roles/User";
}

