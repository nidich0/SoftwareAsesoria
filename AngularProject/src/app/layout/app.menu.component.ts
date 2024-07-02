import { Inject, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import { TranslocoService } from '@jsverse/transloco';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    constructor(private translocoService: TranslocoService, public layoutService: LayoutService) { }

    ngOnInit() {
        this.translocoService.selectTranslation()
            .subscribe(itemLabels => {
                this.model = [
                    {
                        label: itemLabels['menubar.home'],
                        items: [
                            { label: itemLabels['menubar.homepage'], icon: 'pi pi-fw pi-home', routerLink: ['/'] },
                            { label: itemLabels['menubar.advisors'], icon: 'pi pi-fw pi-users', routerLink: ['/asesores'] },
                            { label: itemLabels['menubar.request'], icon: 'pi pi-fw pi-send', routerLink: ['/solicitar'] }
                        ]
                    }
                ];
            });
    }
}
