import { Component, OnInit } from '@angular/core';
import { LayoutService } from "./service/app.layout.service";
import { SelectItem } from 'primeng/api';
import { TranslocoService } from '@jsverse/transloco';

@Component({
    selector: 'app-footer',
    templateUrl: './app.footer.component.html'
})
export class AppFooterComponent implements OnInit {
    languages: SelectItem[] = [];
    selectedLanguage: SelectItem = { value: ''};
    constructor(private translocoService: TranslocoService, public layoutService: LayoutService) { }

    ngOnInit() {
        this.languages = [
            { label: 'Español', value: { id: 1, name: 'Spanish', code: 'ES' } },
            { label: 'English', value: { id: 2, name: 'English', code: 'EN' } },
            { label: 'Português', value: { id: 3, name: 'Portuguese', code: 'PT' } },
            { label: 'Kechua', value: { id: 4, name: 'Quechua', code: 'QU' } },
        ]
        let currentLang = this.translocoService.getActiveLang();

        for (var item of this.languages) {
            if (item.value.code.toLowerCase() == currentLang) {
                this.selectedLanguage = item.value;
                break
            }
        }
    }

    onChange(event: any) {
        this.translocoService.setActiveLang(event.value.code.toLowerCase());
    }
}
