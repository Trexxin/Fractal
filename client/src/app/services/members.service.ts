import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/member';

//Causes a 401 error and I have yet to understand why. The following code seems to work but more investigation is needed as to why
// Temporary way to get authorization
const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
  })
}

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl + 'users', httpOptions);
  }

  getMember(userName: string) {
    return this.http.get<Member>(this.baseUrl + 'users/' + userName, httpOptions);
  }
}

// @Injectable({
//   providedIn: 'root'
// })
// export class MembersService {
//   baseUrl = environment.apiUrl;
 
//   httpOptions = {
//     headers: new HttpHeaders({
//       Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user')).token
//     })
//   };
  
//   constructor(private http: HttpClient) { }
 
//   getMembers() {
//     return this.http.get<Member[]>(this.baseUrl + 'users', this.httpOptions);
//   }
//   getMember(username: string) {
//     return this.http.get<Member>(this.baseUrl + 'user/' + username, this.httpOptions);
//   }
// }