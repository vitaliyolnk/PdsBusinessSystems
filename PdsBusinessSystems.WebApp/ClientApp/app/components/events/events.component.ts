import { Component } from '@angular/core';
import { Http } from "@angular/http";

@Component({
    selector: 'events',
    template: require('./events.component.html')
})

export class EventsComponent {
    public events: EventItem[] = [];
    constructor(public http: Http) {
        this.getCurrentEvents();
    }

    getCurrentEvents() {

        this.http.get('/api/Events/GetCurrentEvents').subscribe(result => {
            this.events = result.json();
        });
    }

    getEvents(startDate: string, endDate: string) {
        this.http.get('/api/Events/GetEvents/' + startDate + '/' + endDate).subscribe(result => {
            this.events = result.json();
        });
    }
}

export interface EventItem {
    StartDate: string;
    EndDate: string;
    StartTime: string;
    EndTime: string;
    Description: string;
    Category: string;
    Members: Member[];
}

export interface Member {
    FullTitle: string;
    Party: string;
    MemberFrom: string;
}