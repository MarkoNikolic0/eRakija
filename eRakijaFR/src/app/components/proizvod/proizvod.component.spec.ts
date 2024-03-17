import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProizvodComponent } from './proizvod.component';

describe('ProizvodComponent', () => {
  let component: ProizvodComponent;
  let fixture: ComponentFixture<ProizvodComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProizvodComponent]
    });
    fixture = TestBed.createComponent(ProizvodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
