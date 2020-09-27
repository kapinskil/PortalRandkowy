import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  title(title: any) {
    throw new Error("Method not implemented.");
  }

 constructor(private aouthService: AuthService) {}
 jwtHelper = new JwtHelperService();

  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token) {
      this.aouthService.decodeToken = this.jwtHelper.decodeToken(token);
    }
  }
}
