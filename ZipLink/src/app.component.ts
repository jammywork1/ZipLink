import { Component } from '@angular/core';

@Component({
    selector: 'app-root',
    template: `
    <div mdl class="">

        <app-header></app-header>

        <main class="container">

            <div class="">
                <router-outlet></router-outlet>
            </div>

        </main>
    </div>
    `
})
export class AppComponent {

    constructor() {

    }
}
