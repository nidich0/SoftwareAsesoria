import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { TableModule } from 'primeng/table';
import { TranslocoModule } from '@jsverse/transloco';

interface Usuario {
    firstName: string;
    lastName: string;
    codigo: string;
    telefono: string;
    email: string;
    role: number;
}

interface ServerData {
    data: {
        items: Usuario[]
    };
}

@Component({
    selector: 'app-asesores',
    templateUrl: './asesores.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        TableModule
    ]
})
export class AsesoresComponent implements OnInit {
    public usuarios: Usuario[] = [];

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getAsesores();
    }

    getAsesores() {
        this.http.get<ServerData>('/api/v1/User/')
            .pipe(
                map(result => result.data.items)
            ).subscribe(res => {
                this.usuarios = res;
                console.log(res);
            });
    }
}
