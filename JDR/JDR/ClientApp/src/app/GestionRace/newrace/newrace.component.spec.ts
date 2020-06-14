import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewraceComponent } from './newrace.component';

describe('NewraceComponent', () => {
  let component: NewraceComponent;
  let fixture: ComponentFixture<NewraceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewraceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
