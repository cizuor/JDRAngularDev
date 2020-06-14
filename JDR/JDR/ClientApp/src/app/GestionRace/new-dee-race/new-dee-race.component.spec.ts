import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewDeeRaceComponent } from './new-dee-race.component';

describe('NewDeeRaceComponent', () => {
  let component: NewDeeRaceComponent;
  let fixture: ComponentFixture<NewDeeRaceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewDeeRaceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewDeeRaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
