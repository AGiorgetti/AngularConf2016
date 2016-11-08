import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'ng2-test',
    template: '<div><h1>ng2 Component using ng1 Directive</h1><sid-monitoring-heartbeats></sid-monitoring-heartbeats></div>'
})
export class TestComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
 }