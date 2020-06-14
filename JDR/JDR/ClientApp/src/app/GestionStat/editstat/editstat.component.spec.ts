import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditstatComponent } from './editstat.component';

describe('EditstatComponent', () => {
  let component: EditstatComponent;
  let fixture: ComponentFixture<EditstatComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditstatComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditstatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
