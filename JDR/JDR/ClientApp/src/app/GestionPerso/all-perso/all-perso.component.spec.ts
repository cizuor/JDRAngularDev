import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllPersoComponent } from './all-perso.component';

describe('AllPersoComponent', () => {
  let component: AllPersoComponent;
  let fixture: ComponentFixture<AllPersoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllPersoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllPersoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
