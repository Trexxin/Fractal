import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
 // Service to make requests to API
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }
 // Recieves credentials from login form in the navbar
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}
