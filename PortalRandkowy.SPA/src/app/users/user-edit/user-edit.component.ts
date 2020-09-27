import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/User';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  user: User;
  @ViewChild('editForm') editForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private route: ActivatedRoute,
              private alertify: AlertifyService,
              private userService: UserService,
              private authService: AuthService){ }

  ngOnInit() {
    this.route.data.subscribe(data =>{
      this.user = data.user;
    });
  }

  updateUser() {

    this.userService.updateUser(this.authService.decodeToken.nameid, this.user)
      .subscribe(next => {
        this.alertify.success('Profil pomyslnie zaktualizowany');
        this.editForm.reset(this.user);
      }, error => {
        this.alertify.error(error);
      });
  }


}
