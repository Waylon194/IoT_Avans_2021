import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Datagram } from '../models/Datagram';

@Injectable({
  providedIn: 'root'
})
export class IwsnBackendService {
  constructor(private http: HttpClient) { }

  getMeasurements(){
      return this.http.get<Datagram[]>("http://localhost:5000/backend-api/v1/iwsn/all");
  }
}