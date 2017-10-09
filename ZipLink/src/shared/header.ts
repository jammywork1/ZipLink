import { Component, Injectable, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routes } from '../modules/app/app.module';

@Component({
    selector: 'app-header',
    template: `
        <header class="">
            <div class="navbar navbar-default navbar-static-top" role="navigation">
                <div class="container-fluid">
                    <div class="navbar-header">
                      <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar-main">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                      </button>
                      <a class="navbar-brand">ZipLink</a>
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li *ngFor="let route of routes">
                            <a *ngIf="route.data" class="" [class.active]="isActive(route.path)"
                                routerLink="{{route.path}}">{{route.data.title}}</a>
                            </li>    
                        </ul>
                    </div>
                </div>
            </div>
        </header>
    `
})
export class HeaderComponent {
    title = 'ZipLink';
    routes = routes.filter((val) => val.path != '');

    constructor(private route: ActivatedRoute, private router: Router) {}

    isActive(path: string): boolean {
        return this.router.url.substring(1) === path;
    }
}