import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from "../models/user"; 

@Injectable({
  providedIn: 'root'
})
export class BackendConnectorService {
  constructor(private http: HttpClient) { }

  private dataUrl = 'http://localhost:28556/api/v1/';

  getData(id: number) : Observable<User>{
    console.log('Fetched user: ${naam} with balance ${balance}');

    const url = `${this.dataUrl}/${id}`;
    return this.http.get<User>(url);
  }
}