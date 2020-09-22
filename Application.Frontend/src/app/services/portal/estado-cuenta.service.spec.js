"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var estado_cuenta_service_1 = require("./estado-cuenta.service");
describe('EstadoCuentaService', function () {
    var service;
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({});
        service = testing_1.TestBed.inject(estado_cuenta_service_1.EstadoCuentaService);
    });
    it('should be created', function () {
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=estado-cuenta.service.spec.js.map