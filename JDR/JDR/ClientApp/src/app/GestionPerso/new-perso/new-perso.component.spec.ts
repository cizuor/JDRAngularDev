import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewPersoComponent } from './new-perso.component';

describe('NewPersoComponent', () => {
  let component: NewPersoComponent;
  let fixture: ComponentFixture<NewPersoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewPersoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewPersoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
