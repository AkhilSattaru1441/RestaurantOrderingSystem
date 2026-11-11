
import { Component, ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Auth } from '../../services/auth';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {

  isLoggedIn = false;
  userName = '';

  constructor(
    private auth: Auth,
    private cdr: ChangeDetectorRef
  ) {
    this.isLoggedIn = !!localStorage.getItem('token');
    this.userName = localStorage.getItem('userName') || '';

    this.auth.isLoggedIn.subscribe(status => {
      this.isLoggedIn = status;

      if (status) {
        this.userName = localStorage.getItem('userName') || '';
      } else {
        this.userName = '';
      }

      this.cdr.markForCheck();
    });
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('userName');

    this.userName = '';
    this.auth.isLoggedIn.next(false);
  }
}
