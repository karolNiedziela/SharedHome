import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BillsExpensesComponent } from './bills-expenses.component';

describe('BillsExpensesComponent', () => {
  let component: BillsExpensesComponent;
  let fixture: ComponentFixture<BillsExpensesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BillsExpensesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BillsExpensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
