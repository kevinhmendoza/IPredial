"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var gestion_permisos_service_1 = require("./gestion-permisos.service");
describe('GestionPermisosService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [gestion_permisos_service_1.GestionPermisosService]
        });
    });
    it('should be created', testing_1.inject([gestion_permisos_service_1.GestionPermisosService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=gestion-permisos.service.spec.js.map