"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var modificar_usuario_service_1 = require("./modificar-usuario.service");
describe('ModificarUsuarioService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [modificar_usuario_service_1.ModificarUsuarioService]
        });
    });
    it('should be created', testing_1.inject([modificar_usuario_service_1.ModificarUsuarioService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=modificar-usuario.service.spec.js.map