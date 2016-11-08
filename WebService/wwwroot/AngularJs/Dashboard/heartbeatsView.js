(function () {
    "use strict";

    heartbeatsController.$inject = ['$scope', 'logsService'];
    function heartbeatsController($scope, logsService) {
        var self = this;

        this.heartbeats = [];

        logsService.onHeartbeat(onHeartbeat);

        logsService.getHeartbeats().then(function (result) {
            $scope.vm.heartbeats = result;
        });

        $scope.$on("$destroy", function () {
            logsService.offHeartbeat(onHeartbeat);
        });

        this.inactive = function (timestampAsStr) {
            var ts = moment(timestampAsStr),
                diff = moment().diff(ts, 'minutes');
            return diff >= 30;
        }

        function onHeartbeat(data) {
            // find and replace the data
            var idxToReplace = helpers.array.indexOf(self.heartbeats, function (val) {
                return val.Id === data.Id;
            });
            if (idxToReplace > -1) // elemento trovato
                self.heartbeats[idxToReplace] = data;
            else // nuovo elemento
                self.heartbeats.unshift(data);
            $scope.$apply();
        }        
    }

    var app = window.app;
    app.controller('heartbeatsController', heartbeatsController)
    app.directive('sidMonitoringHeartbeats', function () {
        return {
            restrict: 'E',
            templateUrl: 'dashboard/heartbeatsView.html',
            scope: {},
            controller: "heartbeatsController",
            controllerAs: "vm",
            bindToController: true
        };
    });
})();