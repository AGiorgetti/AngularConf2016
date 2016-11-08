namespace LogDashboard {
    "use strict";

    export class LogsService {

        private baseUrl: string = 'http://localhost:5000/logApi/';
        private hub = (<any>$.connection).monitoringHub as SignalR.Hub.Proxy;

        static $inject = ['$http'];
        constructor(
            private $http: ng.IHttpService,
        ) { }

        getHeartbeats(): ng.IPromise<IHeartbeatEvent[]> {
            return this.$http.get(this.baseUrl + 'GetHeartbeats')
                .then(response => response.data);            
        }

        onHeartbeat(callback: (data: IHeartbeatEvent) => void): void {
            this.hub.on('heartbeat', callback);
        }

        offHeartbeat(callback: (data: IHeartbeatEvent) => void): void {
            this.hub.off('heartbeat', callback);
        }
    }

    let app = (<any>window).app;
    app.service('logsService', LogsService);
}