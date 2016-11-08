(function () {
    "use strict";

    logsService.$inject = ['$http'];
    function logsService($http) {
        var baseUrl = 'http://localhost:5000/logApi/';
        var hub = $.connection.monitoringHub;

        // this function will return an angular promise with the array of data
        this.getHeartbeats = function () {
            return $http.get(baseUrl + 'GetHeartbeats')
                .then(function (response) { return response.data; });
            // using signalr hubs
            //return $q.when(hub.server.getHeartbeats())
        }

        this.onHeartbeat = function (callback) {
            hub.on('heartbeat', callback);
        }

        this.offHeartbeat = function (callback) {
            hub.off('heartbeat', callback);
        }
    }

    var app = window.app;
    app.service('logsService', logsService);
})();