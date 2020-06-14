import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StatinfoComponent } from './statinfo.component';

describe('StatinfoComponent', () => {
  let component: StatinfoComponent;
  let fixture: ComponentFixture<StatinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StatinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
