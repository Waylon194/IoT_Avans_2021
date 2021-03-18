import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html',
  styleUrls: ['./user-input.component.scss']
})

export class UserInputComponent implements OnInit {
  infoForm = this.formBuilder.group({
    naam: '',
    bedrag: ''
  });

  inputForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
  ) { }

  onSubmit(): void {
    console.warn('Je hebt betaald!', this.infoForm.value);
    this.infoForm.reset();
  }

  ngOnInit(): void {

  }
}