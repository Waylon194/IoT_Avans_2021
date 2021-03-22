import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-button',
  templateUrl: './user-button.component.html',
  styleUrls: ['./user-button.component.scss']
})
export class UserButtonComponent implements OnInit {

  constructor() { }

  onAdd(): void {
    console.warn('Je hebt op Add geklikt!');
  }

  onOpnieuw(): void {
    console.warn('Je hebt op Opnieuw geklikt!');
  }

  ngOnInit(): void {
  }
}