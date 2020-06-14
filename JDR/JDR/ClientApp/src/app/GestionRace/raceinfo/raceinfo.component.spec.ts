import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RaceinfoComponent } from './raceinfo.component';

describe('RaceinfoComponent', () => {
  let component: RaceinfoComponent;
  let fixture: ComponentFixture<RaceinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RaceinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RaceinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
