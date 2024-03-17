import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageProductDialogsComponent } from './manage-product-dialogs.component';

describe('ManageProductDialogsComponent', () => {
  let component: ManageProductDialogsComponent;
  let fixture: ComponentFixture<ManageProductDialogsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageProductDialogsComponent]
    });
    fixture = TestBed.createComponent(ManageProductDialogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
