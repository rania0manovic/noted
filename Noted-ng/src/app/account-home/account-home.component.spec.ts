import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountHomeComponent } from './account-home.component';

// @ts-ignore
describe('AccountHomeComponent', () => {
  let component: AccountHomeComponent;
  let fixture: ComponentFixture<AccountHomeComponent>;

  // @ts-ignore
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // @ts-ignore
  it('should create', () => {
    // @ts-ignore
    expect(component).toBeTruthy();
  });
});
