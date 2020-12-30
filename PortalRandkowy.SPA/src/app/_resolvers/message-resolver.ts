import { Route } from '@angular/compiler/src/core';
import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Message } from '../_models/message';
import { User } from '../_models/User';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Injectable()
export class MessagesResolver implements Resolve<Message[]> {
    
    pageNumber = 1;
    pageSize = 100;
    messageContener = 'Nieprzeczytane';

    constructor(private userService: UserService,
        private router: Router,
        private alertify: AlertifyService,
        private authService: AuthService) {}

        resolve(route: ActivatedRouteSnapshot): Observable<Message[]> {
        return this.userService.getMesseges(this.authService.decodeToken.nameid, this.pageNumber, this.pageSize, this.messageContener).pipe(
        catchError(error => {
        this.alertify.error('Problem z wyszukiwaniem wiadomości');
        this.router.navigate(['']);
        return of(null);
    })
);
    }
}
