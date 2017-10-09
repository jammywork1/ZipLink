import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home';
import { CreateZippedLinkComponent } from './home/createZippedLink';
import { ListComponent } from './list/list';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    { path: 'home', component: HomeComponent, data: { title: 'Home' } },
    { path: 'list', component: ListComponent, data: { title: 'List' } },
    { path: '**', redirectTo: 'home' },
];

@NgModule({
    declarations: [
        HomeComponent,
        ListComponent,
        CreateZippedLinkComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot(routes)
    ]
})
export class AppModule { }
