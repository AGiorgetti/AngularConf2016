(function () {
	// definisce le route sempre disponibili per l'applicazione: il set di route che sono comunque accessibili
	// da tutto il software
	// todo: l'attributo 'data: {level:0}' associato alle route non è più necessario, queste info sono su database ora
	window.app.config(['$routeProvider',
		function ($routeProvider) {
			$routeProvider
				.when('/dashboard', { templateUrl: 'dashboard/dashboard.html', data: {} })
				.when('/messages', { templateUrl: 'app/messages/messages.html', controller: 'sid.messages.ctrl', data: {} })
				.otherwise({ redirectTo: '/dashboard' });
		}]);
})();