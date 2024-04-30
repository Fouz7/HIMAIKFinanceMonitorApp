import { Component } from '@angular/core';
import { AuthService } from '../../Core/Services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string ='';
  password: string ='';

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  submit() {
    this.authService.login(this.username, this.password).subscribe(
      data => {
        // handle successful login
        this.router.navigate(['/admin-dashboard']);
      },
      error => {
        // handle error
        alert("Password Salah")
      }
    );
  }
}
