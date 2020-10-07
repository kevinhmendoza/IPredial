"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
exports.ApisRestGestionPermisos = exports.ApisRestEstadoCuenta = exports.ApisRestCambiarEstadoUserAndTercero = exports.ApisRestModificarUserAndTercero = exports.ApisRestGestionUsers = exports.ApisRestUser = exports.ApisRestBase = void 0;
var ApisRestBase = /** @class */ (function () {
    function ApisRestBase() {
    }
    ApisRestBase.UrlServer = "http://localhost:8053/";
    return ApisRestBase;
}());
exports.ApisRestBase = ApisRestBase;
var ApisRestUser = /** @class */ (function (_super) {
    __extends(ApisRestUser, _super);
    function ApisRestUser() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestUser.PostLogin = ApisRestUser.UrlServer + "Token";
    ApisRestUser.PostLogout = ApisRestUser.UrlServer + "api/Account/Logout";
    ApisRestUser.GetPermissionUser = ApisRestUser.UrlServer + "api/User/Permission";
    ApisRestUser.GetUser = ApisRestUser.UrlServer + "api/User";
    ApisRestUser.PostChangePassword = ApisRestUser.UrlServer + "api/User/ChangePassword";
    return ApisRestUser;
}(ApisRestBase));
exports.ApisRestUser = ApisRestUser;
var ApisRestGestionUsers = /** @class */ (function (_super) {
    __extends(ApisRestGestionUsers, _super);
    function ApisRestGestionUsers() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestGestionUsers.GetAllUsers = ApisRestBase.UrlServer + "api/User/Get/All";
    ApisRestGestionUsers.PostRegisterUser = ApisRestBase.UrlServer + "api/Gestion/User/Register";
    ApisRestGestionUsers.PostToggleUser = ApisRestBase.UrlServer + "api/Gestion/User/ChangeState";
    return ApisRestGestionUsers;
}(ApisRestBase));
exports.ApisRestGestionUsers = ApisRestGestionUsers;
var ApisRestModificarUserAndTercero = /** @class */ (function (_super) {
    __extends(ApisRestModificarUserAndTercero, _super);
    function ApisRestModificarUserAndTercero() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestModificarUserAndTercero.GetTerceroAndUser = ApisRestModificarUserAndTercero.UrlServer + "api/Modificar/UserWithTercero";
    ApisRestModificarUserAndTercero.PostModificarTercero = ApisRestModificarUserAndTercero.UrlServer + "api/Modificar/UserWithTercero";
    return ApisRestModificarUserAndTercero;
}(ApisRestBase));
exports.ApisRestModificarUserAndTercero = ApisRestModificarUserAndTercero;
var ApisRestCambiarEstadoUserAndTercero = /** @class */ (function (_super) {
    __extends(ApisRestCambiarEstadoUserAndTercero, _super);
    function ApisRestCambiarEstadoUserAndTercero() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestCambiarEstadoUserAndTercero.GetTerceroAndUser = function (idTercero) { return ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/Cambiar/Estado/UserWithTercero" + idTercero; };
    ApisRestCambiarEstadoUserAndTercero.PostModificarTercero = ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/Cambiar/Estado/UserWithTercero";
    return ApisRestCambiarEstadoUserAndTercero;
}(ApisRestBase));
exports.ApisRestCambiarEstadoUserAndTercero = ApisRestCambiarEstadoUserAndTercero;
var ApisRestEstadoCuenta = /** @class */ (function (_super) {
    __extends(ApisRestEstadoCuenta, _super);
    function ApisRestEstadoCuenta() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestEstadoCuenta.PostEstadoCuenta = ApisRestCambiarEstadoUserAndTercero.UrlServer + "api/EstadoCuenta/Consultar";
    return ApisRestEstadoCuenta;
}(ApisRestBase));
exports.ApisRestEstadoCuenta = ApisRestEstadoCuenta;
var ApisRestGestionPermisos = /** @class */ (function (_super) {
    __extends(ApisRestGestionPermisos, _super);
    function ApisRestGestionPermisos() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ApisRestGestionPermisos.GetModules = ApisRestGestionPermisos.UrlServer + "api/Roles/Modules";
    ApisRestGestionPermisos.PostGetPermission = ApisRestGestionPermisos.UrlServer + "api/Roles/Get/Permission";
    ApisRestGestionPermisos.PostGestionPermission = ApisRestGestionPermisos.UrlServer + "api/Roles/Set/Permission";
    ApisRestGestionPermisos.GetUser = ApisRestGestionPermisos.UrlServer + "api/Roles/User";
    return ApisRestGestionPermisos;
}(ApisRestBase));
exports.ApisRestGestionPermisos = ApisRestGestionPermisos;
//# sourceMappingURL=apis.js.map