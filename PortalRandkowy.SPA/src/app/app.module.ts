import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { appRoutes } from './routes';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { UserService } from './_services/user.service';
import { UserListComponent } from './users/user-list/user-list.component';
import { LikesComponent } from './likes/likes.component';
import { MessagesComponent } from './messages/messages.component';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';



export function tokenGetter(){
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [		
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      UserListComponent,
      LikesComponent,
      MessagesComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            allowedDomains: ['localhost:5001'],
            disallowedRoutes: []
         }
      }),
      RouterModule.forRoot(appRoutes),
      BrowserAnimationsModule,
      BsDropdownModule.forRoot()
   ],
   providers: [
      AuthService,
      AlertifyService,
      UserService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
