import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
 // Service to make requests to API
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }
 // Recieves credentials from login form in the navbar
  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      // Populates user in browse storage for persistance
      map((response: User) => {
        // Gets the user from the response
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }
  // Registers user
  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }
  // Helper method
  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  // Removes user from localStorage
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
