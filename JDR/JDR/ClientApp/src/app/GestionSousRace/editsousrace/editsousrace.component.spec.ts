import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditsousraceComponent } from './editsousrace.component';

describe('EditsousraceComponent', () => {
  let component: EditsousraceComponent;
  let fixture: ComponentFixture<EditsousraceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditsousraceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditsousraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
