import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewStatRaceComponent } from './new-stat-race.component';

describe('NewStatRaceComponent', () => {
  let component: NewStatRaceComponent;
  let fixture: ComponentFixture<NewStatRaceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewStatRaceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewStatRaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
