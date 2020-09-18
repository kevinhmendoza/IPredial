"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var sistema_service_1 = require("./sistema.service");
describe('SistemaService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [sistema_service_1.SistemaService]
        });
    });
    it('should be created', testing_1.inject([sistema_service_1.SistemaService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=sistema.service.spec.js.map