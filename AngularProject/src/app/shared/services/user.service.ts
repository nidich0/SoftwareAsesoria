import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
    constructor(private httpClient: HttpClient) { }

    getData(data: any) {
        return this.httpClient.post(`/api/v1/User/me`, data)
            .pipe(tap((result) => {

                //localStorage.setItem('authUser', JSON.stringify(result));
            }));
    }
}
