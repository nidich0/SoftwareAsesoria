import { Component } from '@angular/core';
import { inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { TranslocoModule } from '@jsverse/transloco';
import { AuthService } from '@services/auth.service';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        ReactiveFormsModule,
        RouterModule,
        CheckboxModule,
        InputTextModule,
        ButtonModule,
        RippleModule
    ]
})

export class LoginComponent {
    authService = inject(AuthService);
    router = inject(Router);

    protected loginForm = new FormGroup({
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required])
    })

    onSubmit() {
        if (this.loginForm.valid) {
            //console.log(this.loginForm.value);
            this.authService.login(this.loginForm.value)
                .subscribe((data: any) => {
                    if (this.authService.isLoggedIn()) {
                        this.router.navigate(['/']);
                    }
                });
        }
    }
}
