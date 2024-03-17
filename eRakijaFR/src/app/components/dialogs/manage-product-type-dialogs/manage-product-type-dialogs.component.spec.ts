import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageProductTypeDialogsComponent } from './manage-product-type-dialogs.component';

describe('ManageProductTypeDialogsComponent', () => {
  let component: ManageProductTypeDialogsComponent;
  let fixture: ComponentFixture<ManageProductTypeDialogsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ManageProductTypeDialogsComponent]
    });
    fixture = TestBed.createComponent(ManageProductTypeDialogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
