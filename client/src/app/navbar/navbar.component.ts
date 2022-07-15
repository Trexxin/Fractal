import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})


export class NavbarComponent implements OnInit {
  model: any = {}

  // Injections
  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  // Uses AccountService to allow user to login
  login() {
    this.accountService.login(this.model).subscribe(response => {
      // Redirects user to members component upon login
      this.router.navigateByUrl('/members')
    }, error => {
      console.log(error);
      this.toastr.error(error.error)
    })
  }

  logout() {
    this.accountService.logout();
    // Redirects user to home component upon logout
    this.router.navigateByUrl('/')
  }

  
}
