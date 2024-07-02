import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private httpClient: HttpClient) { }

    signup(data: any) {
        return this.httpClient.post(`/api/v1/register`, data);
    }

    login(data: any) {
        return this.httpClient.post(`/api/v1/User/login`, data)
            .pipe(tap((result) => {
                localStorage.setItem('authUser', JSON.stringify(result));
            }));
    }

    logout() {
        localStorage.removeItem('authUser');
    }

    isLoggedIn() {
        return localStorage.getItem('authUser') !== null;
    }
}
