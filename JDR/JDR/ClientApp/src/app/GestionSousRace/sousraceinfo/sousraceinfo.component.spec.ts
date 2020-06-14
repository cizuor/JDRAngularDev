import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SousraceinfoComponent } from './sousraceinfo.component';

describe('SousraceinfoComponent', () => {
  let component: SousraceinfoComponent;
  let fixture: ComponentFixture<SousraceinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SousraceinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SousraceinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
