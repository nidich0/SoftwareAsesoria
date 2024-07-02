import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AutoCompleteCompleteEvent, AutoCompleteModule } from 'primeng/autocomplete';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslocoModule } from '@jsverse/transloco';

@Component({
    selector: 'app-solicitar',
    templateUrl: './solicitar.component.html',
    standalone: true,
    imports: [
        TranslocoModule,
        AutoCompleteModule,
        ReactiveFormsModule,
        FormsModule
    ]
})

export class SolicitarComponent implements OnInit {
    asesores: any[] = [];
    filteredAsesores: any[] = [];
    selectedAsesor: any;
    constructor(private http: HttpClient) { }

    ngOnInit() {

    }
    filterAsesor(event: AutoCompleteCompleteEvent) {
        let filtered: any[] = [];
        let query = event.query;

        for (let i = 0; i < (this.asesores as any[]).length; i++) {
            let asesor = (this.asesores as any[])[i];
            if (asesor.name.toLowerCase().indexOf(query.toLowerCase()) == 0) {
                filtered.push(asesor);
            }
        }

        this.filteredAsesores = filtered;
    }
}
