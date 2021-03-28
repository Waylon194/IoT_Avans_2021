import { Component, OnInit } from '@angular/core';
import { FormSubmissionsService } from '../services/form-submissions.service';

@Component({
  selector: 'app-form-submission-component',
  templateUrl: './form-submission-component.component.html',
  styleUrls: ['./form-submission-component.component.scss']
})
export class FormSubmissionComponentComponent implements OnInit {
  constructor(public formSubmissionService: FormSubmissionsService) { }

  ngOnInit(): void {

  }
}