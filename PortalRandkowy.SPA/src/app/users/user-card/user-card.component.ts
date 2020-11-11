import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent implements OnInit {

  @Input() user: User;

  constructor(private authService: AuthService, 
              private userService: UserService, 
              private alertyfi: AlertifyService) { }

  ngOnInit() {
  }

  sendLike(id: number) {
    this.userService.sendLike(this.authService.decodeToken.nameid, id)
        .subscribe(data => {
          this.alertyfi.success('Polubiłeś ' + this.user.username);
        }, error => {
          this.alertyfi.error(error);
        });
  }

}
