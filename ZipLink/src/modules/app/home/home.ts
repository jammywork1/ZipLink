import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CreateZippedLinkComponent } from './createZippedLink';

@Component({
    selector: 'home',
    template: `
        <section class="">
            <div class="">
                <createZippedLink></createZippedLink> 
            </div>
        </section>
    `
})
export class HomeComponent {
    name: string;
    heading: string;

    constructor(private route: ActivatedRoute) {
        this.heading = "Home";
    }

    ngOnInit() {
        this.route.data.subscribe(x => this.name = x.name);
    }
}