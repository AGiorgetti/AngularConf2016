(function () {
    "use strict";
    var app = window.app;
    app.directive('sidMonitoringLogs', function () {
        return {
            templateUrl: 'dashboard/logsView.html',
            scope: {},
            controller: ['$scope', '$q', function logsVM($scope, $q) {
                var hub = $.connection.monitoringHub;

                $scope.vm = {
                    totalPagesCount: 0,
                    logs: [],
                    pageIndex: 1,
                    nextPage: nextPage,
                    prevPage: prevPage
                };

                hub.on('log', function (data) {
                    $scope.vm.logs.unshift(data);
                    $scope.$apply();
                });

                function nextPage() {
                    if ($scope.vm.pageIndex < $scope.vm.totalPagesCount) {
                        $scope.vm.pageIndex++;
                        getLogs();
                    }
                }

                function prevPage() {
                    if ($scope.vm.pageIndex > 1) {
                        $scope.vm.pageIndex--;
                        $scope.vm.pageIndex = Math.max($scope.vm.pageIndex, 1);
                        getLogs();
                    }
                }

                function getLogs() {
                    $q.when(hub.server.getLogs('', $scope.vm.pageIndex, 50)).then(function (result) {
                        $scope.vm.totalPagesCount = result.TotalPagesCount;
                        $scope.vm.logs = result.Data;
                    });
                }

                getLogs();
            }]
        };
    });
})();