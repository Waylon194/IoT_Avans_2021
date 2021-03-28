import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FormSubmissionsService } from '../services/form-submissions.service';

@Component({
  selector: 'app-user-input',
  templateUrl: './user-input.component.html',
  styleUrls: ['./user-input.component.scss']
})

export class UserInputComponent implements OnInit {
  inputForm = new FormGroup({
    zender: new FormControl(''),
    ontvanger: new FormControl(''),
    bedrag: new FormControl(''),
  });

  constructor(public formSubmissionsService: FormSubmissionsService) { }

  onSubmit(): void {
    //Simpele form validation, werkt niet 100%
    if(this.inputForm.value.zender || this.inputForm.value.ontvanger || this.inputForm.value.bedrag){  
      this.formSubmissionsService.add(this.inputForm.value.zender, this.inputForm.value.ontvanger, this.inputForm.value.bedrag);
    }
    else{
      console.log("Zender, ontvanger of bedrag is null!");
    }
  }

  //Reset knop voor de form
  onReset(): void {
    this.inputForm.reset();
  }

  ngOnInit(): void {

  }
}