import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FormSubmissionsService {
  //Array van betalingen
  betalingen: string[] = [];
  //Totaalbedrag van alle betalingen
  totaalBedrag: number = 0;
  //Aantal betalingen
  aantalBetalingen: number = 0;
  //Bedrag van de betaling, maar afgerond op 2 decimalen
  afgerondBedrag: number = 0;
  
  constructor() { }

  //Voeg formsubmissions toe aan een array
  add(sender: String, reciever: String, bedrag: number){
    this.aantalBetalingen++;
    //Epsilon usage: https://stackoverflow.com/questions/11832914/round-to-at-most-2-decimal-places-only-if-necessary
    this.afgerondBedrag = Math.round((bedrag + Number.EPSILON) * 100) / 100;
    this.totaalBedrag +=  this.afgerondBedrag;
    let betaling = sender + " heeft €" +  this.afgerondBedrag + " betaald!";
    //let betaling = sender + " heeft " + reciever + " €" + bedrag + " betaald!";
    this.betalingen.push(betaling);
  }
}