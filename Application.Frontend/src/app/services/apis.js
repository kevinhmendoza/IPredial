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
exports.ApisRestUser = exports.ApisRestBase = void 0;
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
//# sourceMappingURL=apis.js.map