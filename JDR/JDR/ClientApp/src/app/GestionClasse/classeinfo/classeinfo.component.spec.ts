import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClasseinfoComponent } from './classeinfo.component';

describe('ClasseinfoComponent', () => {
  let component: ClasseinfoComponent;
  let fixture: ComponentFixture<ClasseinfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClasseinfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClasseinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
