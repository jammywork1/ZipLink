import { Component, Input, ChangeDetectorRef } from '@angular/core';
import { client } from '../../../shared/utils';
import { CreateZippedLinkRequest, CreatedStatusEnum } from "../../../zippedLink.dtos";
import { debounce } from 'lodash';

@Component({
    selector: 'createZippedLink',
    templateUrl: 'createZippedLink.html',
    styleUrls: ['createZippedLink.css']
})
export class CreateZippedLinkComponent {
    isZipping: boolean;
    link: string;
    alertStatus: CreatedStatusEnum;
    alertText: string;
    constructor(private cdref: ChangeDetectorRef) { }

    //@Input() link: string;

    ngOnInit() {
        this.isZipping = false;
    }

    async zipLink() {
        if (this.link) {
            this.isZipping = true;
            try {
                var req = new CreateZippedLinkRequest();
                req.link = this.link;
                var r = await client.get(req); 
                this.alertText = r.statusText;
                this.alertStatus = r.status;
                if (this.isSuccess())
                    this.link = "";
            }
            finally {
                this.isZipping = false;
            }
        } else {
            this.alertStatus = "Fail";
            this.alertText = "Link field can't be empty";
        }
        this.cdref.detectChanges();
    }
    isDisableForm() {
        return this.isZipping;
    }
    isAlertInvisible() {
        return this.alertText === "";
    }
    isSuccess() {
        return this.alertStatus === "Success";
    }
    isFail() {
        return this.alertStatus === "Fail";
    }
}