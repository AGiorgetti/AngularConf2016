(function () {
	"use strict";
	var app = window.app;
	app.controller('sid.messages.ctrl', ['$scope', '$q', function ($scope, $q) {
		var hub = $.connection.monitoringHub;

		$scope.vm = {
			type: '',
			logs: [],
			search: search
		};

		hub.on('message', function (data) {
			$scope.vm.logs.unshift(data);
			$scope.$apply();
		});

		function search(type) {
			$q.when(hub.server.getRawLogs(100, type)).then(function (result) {
				$scope.vm.logs = result;
				//$scope.$apply();
			});
		};

		search();
	}]);
})();