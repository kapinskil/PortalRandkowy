import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/message';
import { Pagination, PaginationResult } from '../_models/pagination';
import { AuthService } from '../_services/auth.service';
import { ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  messageContainer: 'Outbox';

  constructor(private userService: UserService, 
              private authService: AuthService,
              private route: ActivatedRoute,
              private alertyfy: AlertifyService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.messages = data.messages.result;
      this.pagination = data.messages.pagination;
    });
  }

  loadMessages() {
    this.userService.getMesseges(this.authService.decodeToken.nameid, this.pagination.currentPage,
                                  this.pagination.itemsPerPage, this.messageContainer)
        .subscribe((res: PaginationResult<Message[]>) => {
          this.messages = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertyfy.error(error);
        });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
}

}
