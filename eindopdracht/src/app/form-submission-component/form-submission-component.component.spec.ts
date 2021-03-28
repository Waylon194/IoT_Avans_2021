import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormSubmissionComponentComponent } from './form-submission-component.component';

describe('FormSubmissionComponentComponent', () => {
  let component: FormSubmissionComponentComponent;
  let fixture: ComponentFixture<FormSubmissionComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormSubmissionComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormSubmissionComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
