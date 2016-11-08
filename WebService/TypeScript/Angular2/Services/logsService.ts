import { Injectable } from '@angular/core';
import { IHeartbeatEvent } from '../Shared/heartbeatEvent';
import { Http, Headers, URLSearchParams } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class LogsService {

    private baseUrl: string = 'http://localhost:5000/logApi/';
    private hub = (<any>$.connection).monitoringHub as SignalR.Hub.Proxy;

    constructor(
        private http: Http
    ) { }

    getHeartbeats(): Promise<IHeartbeatEvent[]> {
        return this.http.get(this.baseUrl + 'GetHeartbeats')
            .toPromise()
            .then(response => response.json() as IHeartbeatEvent[]);           
    }

    onHeartbeat(callback: (data: IHeartbeatEvent) => void): void {
        this.hub.on('heartbeat', callback);
    }

    offHeartbeat(callback: (data: IHeartbeatEvent) => void): void {
        this.hub.off('heartbeat', callback);
    }
}
