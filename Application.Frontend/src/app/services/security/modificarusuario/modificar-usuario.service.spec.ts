import { TestBed, inject } from '@angular/core/testing';

import { ModificarUsuarioService } from './modificar-usuario.service';

describe('ModificarUsuarioService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModificarUsuarioService]
    });
  });

  it('should be created', inject([ModificarUsuarioService], (service: ModificarUsuarioService) => {
    expect(service).toBeTruthy();
  }));
});
