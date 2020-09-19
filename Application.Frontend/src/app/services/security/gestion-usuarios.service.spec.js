"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var gestion_usuarios_service_1 = require("./gestion-usuarios.service");
describe('GestionUsuariosService', function () {
    var service;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({});
        service = testing_1.TestBed.inject(gestion_usuarios_service_1.GestionUsuariosService);
    });
    it('should be created', function () {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=gestion-usuarios.service.spec.js.map