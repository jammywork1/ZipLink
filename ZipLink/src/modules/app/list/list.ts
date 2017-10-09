import { Component } from '@angular/core';
import { client } from '../../../shared/utils';
import { GetAllZippedLinkResponse, GetAllZippedLinkRequest, GetZippedLinkResponse } from '../../../zippedLink.dtos';
import * as moment from 'moment';
import DateTimeFormat = Intl.DateTimeFormat;

@Component({
    selector: 'list', 
    templateUrl: 'list.html',
    styleUrls: ['list.css']
})
export class ListComponent {
    heading: string;
    zippedLinks: GetZippedLinkResponse[];

    constructor() {
        this.heading = "List";
    }

    async ngOnInit() {
        await this.getLinks();
    }

    async getLinks() {
        let req = new GetAllZippedLinkRequest();
        let r = await client.get(req); 
        this.zippedLinks = r.links;
    }
    
    formatDateTime(dt: any) {
        return moment(dt).format("DD.MM.YYYY, hh:mm");
    }
    
    async unzipLink() {
        await this.getLinks(); 
    }
}