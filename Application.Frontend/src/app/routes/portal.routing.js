"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Portal = void 0;
var estado_cuenta_component_1 = require("../pages/portal/estado-cuenta/estado-cuenta.component");
var auth_layout_component_1 = require("../layouts/auth-layout/auth-layout.component");
exports.Portal = {
    path: 'Portal', component: auth_layout_component_1.AuthLayoutComponent,
    children: [
        {
            path: 'Estado/Cuenta', component: estado_cuenta_component_1.EstadoCuentaComponent
        }
    ]
};
//# sourceMappingURL=portal.routing.js.map