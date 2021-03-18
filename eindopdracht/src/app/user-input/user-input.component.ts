import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html',
  styleUrls: ['./user-input.component.scss']
})
export class UserInputComponent implements OnInit {
  checkoutForm = this.formBuilder.group({
    naam: '',
    bedrag: ''
  });

  constructor(
    public inputForm: FormGroup,
    private formBuilder: FormBuilder,
  ) { }

  onSubmit(): void {
    console.warn('Je hebt betaald!', this.checkoutForm.value);
    this.checkoutForm.reset();
  }

  ngOnInit(): void {
  }
}