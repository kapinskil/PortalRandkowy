import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHandler, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User';
import { PaginationResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }


getUsers(page?, itemsPerPage?, userParams?, likesParam?): Observable<PaginationResult<User[]>> {

  const paginationResult: PaginationResult<User[]> = new PaginationResult<User[]>();
  let params = new HttpParams();

  if (page != null && itemsPerPage != null) {
    params = params.append('pageNumber', page);
    params = params.append('pageSize', itemsPerPage);
  }

  if (userParams != null) {
    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('zodiacSign', userParams.zodiacSign);
    params = params.append('orderBy', userParams.orderBy);
  }

  if(likesParam === 'UserLikes') {
    params = params.append('UserLikes', 'true');
  }

  if(likesParam === 'UserIsLiked') {
    params = params.append('UserIsLiked', 'true');
  }

  return this.http.get<User[]>(this.baseUrl + 'users', { observe: 'response', params })
    .pipe(
      map(response => {
        paginationResult.result = response.body;

        if (response.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginationResult;
      })
    );
}
  getUser(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }

  updateUser(id: number, user: User) {
    return this.http.put(this.baseUrl + 'users/' + id, user);
  }

  setMainPhoto(userId: number, id: number){
    return this.http.post(this.baseUrl + 'users/' + userId + '/photos/' + id + '/setMain', {});
  }

   deletePhoto(userId: number, id: number) {
      return this.http.delete(this.baseUrl + 'users/' + userId + '/photos/' + id);
   }

   sendLike(id: number, recipientId: number) {
      return this.http.post(this.baseUrl + 'users/' + id + '/like/' + recipientId, {});
   }

   getMesseges(id: number, page?, itemsPerPage?, messageContener?){
      const paginationResult: PaginationResult<Message[]> = new PaginationResult<Message[]>();
      let params = new HttpParams();

      params = params.append('messageContener',messageContener);
    
      if (page != null && itemsPerPage != null) {
        params = params.append('pageNumber', page);
        params = params.append('pageSize', itemsPerPage);
    }

      return this.http.get<Message[]>(this.baseUrl + 'users/' + id + '/Messages', { observe: 'response', params })
    .pipe(
      map(response => {
        paginationResult.result = response.body;

        if (response.headers.get('Pagination') != null) {
          paginationResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginationResult;
      })
    );
   }
}

