import { TestBed, inject } from '@angular/core/testing';

import { GestionPermisosService } from './gestion-permisos.service';

describe('GestionPermisosService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GestionPermisosService]
    });
  });

  it('should be created', inject([GestionPermisosService], (service: GestionPermisosService) => {
    expect(service).toBeTruthy();
  }));
});
