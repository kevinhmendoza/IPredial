"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UtilitiesAnibalService = void 0;
var apis_1 = require("./apis");
var UtilitiesAnibalService = /** @class */ (function () {
    function UtilitiesAnibalService() {
    }
    UtilitiesAnibalService.Console = function (mensaje) {
        console.log(mensaje);
    };
    UtilitiesAnibalService.ShowLoading = function () {
        this.Loading = true;
    };
    UtilitiesAnibalService.HideLoading = function () {
        this.Loading = false;
    };
    UtilitiesAnibalService.SetUrlForIframe = function (idIframe, url) {
        jQuery("#" + idIframe).attr('src', url + "");
        setTimeout(function () {
            jQuery("#" + idIframe).attr('src', url + "");
        }, 500);
    };
    UtilitiesAnibalService.DomainServer = apis_1.ApisRestBase.UrlServer;
    UtilitiesAnibalService.AppName = "IPredial";
    UtilitiesAnibalService.Loading = false;
    return UtilitiesAnibalService;
}());
exports.UtilitiesAnibalService = UtilitiesAnibalService;
//# sourceMappingURL=utilities.js.map