namespace LogDashboard {
    "use strict";

    class heartbeatsController {

        heartbeats: IHeartbeatEvent[] = [];

        static $inject = ['$scope', 'logsService'];
        constructor(
            private $scope: ng.IScope,
            private logsService: LogsService
        ) {
            logsService.onHeartbeat(this.onHeartbeat);

            logsService.getHeartbeats().then((result) => {
                this.heartbeats = result;
            });

            $scope.$on("$destroy", () => {
                this.logsService.offHeartbeat(this.onHeartbeat);
            });
        }

        inactive(timestampAsStr: string): boolean {
            let ts = (<any>window).moment(timestampAsStr),
                diff = (<any>window).moment().diff(ts, 'minutes');
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
            if(idxToReplace > -1) // elemento trovato
                this.heartbeats[idxToReplace] = data;
            else // nuovo elemento
                this.heartbeats.unshift(data);
            this.$scope.$apply();
        }
    }

    let app = (<any>window).app;
    app.controller('heartbeatsController', heartbeatsController)
    app.directive('sidMonitoringHeartbeats', function () {
        return <ng.IDirective>{
            restrict: 'E',
            templateUrl: 'dashboard/heartbeatsView.html',
            scope: {},
            controller: "heartbeatsController",
            controllerAs: "vm",
            bindToController: true
        };
    });
}