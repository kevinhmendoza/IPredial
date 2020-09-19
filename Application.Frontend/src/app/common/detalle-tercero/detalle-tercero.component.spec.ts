import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalleTerceroComponent } from './detalle-tercero.component';

describe('DetalleTerceroComponent', () => {
  let component: DetalleTerceroComponent;
  let fixture: ComponentFixture<DetalleTerceroComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalleTerceroComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalleTerceroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
