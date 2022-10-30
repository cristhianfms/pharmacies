import {Component, OnInit} from '@angular/core';
import {StoreService} from "../../../services/store.service";
import {User} from "../../../models/user.model";
import {SessionsService} from "../../../services/sessions.service";

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

    counter: number = 0;
    profile: User | null = null;

    constructor(private storeService: StoreService, private sessionService: SessionsService) {
    }

    ngOnInit(): void {
        this.storeService.myCart$.subscribe(drugs => {
            this.counter = drugs.length;
        })
        this.sessionService.user$.subscribe(data => {
            this.profile = data;
        })
    }

    login() {
        this.sessionService.loginAndGet("Admin", "admin1234-")
            .subscribe(user => {
                this.profile = user
            })
    }
}
