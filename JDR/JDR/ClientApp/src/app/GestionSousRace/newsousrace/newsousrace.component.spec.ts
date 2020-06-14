import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsousraceComponent } from './newsousrace.component';

describe('NewsousraceComponent', () => {
  let component: NewsousraceComponent;
  let fixture: ComponentFixture<NewsousraceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewsousraceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewsousraceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
