import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html',
  styleUrls: ['./user-input.component.scss']
})

export class UserInputComponent implements OnInit {
  inputForm = new FormGroup({
    naam: new FormControl(''),
    bedrag: new FormControl(''),
  });

  constructor() { }

  onSubmit(): void {
    console.warn('Je hebt betaald!', this.inputForm.value);
    this.inputForm.reset();
  }

  ngOnInit(): void {

  }
}