import { Component, Inject, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { IHeartbeatEvent } from '../Shared/heartbeatEvent';
import { LogsService } from '../Services/logsService';
import * as moment from 'moment';

@Component({
    selector: "ng2-heartbeat-view",
    templateUrl: "../Angular2/Dashboard/heartbeatView.component.html"
})
export class HeartbeatView implements OnInit, OnDestroy {

    heartbeats: IHeartbeatEvent[] = [];

    constructor(
        @Inject('logsService')
        private _logsService: any, // LogsService - inject this one to see the Angular2 service at work
        private _changeDetector: ChangeDetectorRef
    ) { }

    ngOnInit() {
        this._logsService.onHeartbeat(this.onHeartbeat);

        this._logsService.getHeartbeats().then(result => {
            this.heartbeats = result
        });
    }

    ngOnDestroy() {
        this._logsService.offHeartbeat(this.onHeartbeat);
    }

    inactive(timestampAsStr: string): boolean {
        let ts = moment(timestampAsStr),
            diff = moment().diff(ts, 'minutes');
        return diff >= 30;
    }

    // This need to be an instance function in order to let TypeScript
    // capture the 'this' so we have the right reference to the class
    // in the callbacks passed to logService.
    // Another approach would have been delegate the calls:
    // logsService.onHeartbeat(data => this.onHeartbeat(data));
    private onHeartbeat = (data: IHeartbeatEvent): void => {
        // find and replace the data
        let idxToReplace = (<any>window).helpers.array.indexOf(this.heartbeats, (val) => {
            return val.Id === data.Id;
        });
        if (idxToReplace > -1) // elemento trovato
            this.heartbeats[idxToReplace] = data;
        else // nuovo elemento
            this.heartbeats.unshift(data);
        // force the change detector
        // ng2 has troubles intercepting SignalR
        this._changeDetector.detectChanges();
    }
}