(function (helpers, $, undefined) {

	helpers.array = {
		indexOf: function indexOf(collection, filter) {
			for (var i = 0; i < collection.length; i++) {
				if (filter(collection[i], i, collection))
					return i;
			}
			return -1;
		}
	};

	helpers.string = {
		isNullOrEmpty: function (str) {
			return str == null || str === '';
		},
		startsWith: function (sourceString, prefix) {
			return sourceString.indexOf(prefix) === 0;
		},
		endsWith: function (sourceString, suffix) {
			return sourceString.indexOf(suffix, sourceString.length - suffix.length) !== -1;
		},
		contains: function (sourceString, substr) {
			// case insensitive search inside the string
			var ri = new RegExp(substr, 'i');
			return ri.test(sourceString);
		}
	};

})(window.helpers = window.helpers || {}, jQuery);

(function () {
	"use strict";

	// define module containers
	angular.module('signalr', []);

	var compileProvider,
        controllerProvider,
        filterProvider,
        provide,
        routeProvider;

	// create your application module and store references to the compileProvider, controllerProvider, filterProvider, and provider
	var app = angular.module('app', ['ngRoute', 'signalr'],
	['$compileProvider', '$controllerProvider', '$filterProvider', '$provide', '$routeProvider', function ($compileProvider, $controllerProvider, $filterProvider, $provide, $routeProvider) {
		compileProvider = $compileProvider;
		controllerProvider = $controllerProvider;
		filterProvider = $filterProvider;
		routeProvider = $routeProvider;
		provide = $provide;
	}]);

	window.app = app;

	var lazyAngular = {
		registerFactoryProvider: function (factory) {
			provide.factory.apply(null, factory);
		},
		registerFilterProvider: function (filter) {
			filterProvider.register.apply(null, filter);
		},
		registerDirective: function (directive) {
			compileProvider.directive.apply(null, directive);
		},
		registerController: function (controller) {
			controllerProvider.register.apply(null, controller);
		},
		registerRoute: function (route) {
			/// <summary>
			/// accepts an object in the form:
			/// {
			///   path: '/path',
			///   route: { templateUrl: '...', controller: '...' }
			/// }
			/// </summary>
			/// <param name="route"></param>
			routeProvider.when(route.path, route.route);
		}
	};

	// Now, register a factory you can use to register lazily loaded controllers, directives, filters and factories
	app.factory("lazyAngular", function () {
		return lazyAngular;
	});

	// Note that you can also reference the lazyAngular object directly, without dependency injection if you wish.

	// primo tentativo per 'sconfiggere' la cache del browser in deploy release (per evitare la cache sui file http se facciamo deploy di versioni aggiornate)
	// $http viene utilizzato internamente da tutte le direttive per scaricare i template
	// todo: take the proper versioning from H8
	app.constant('appVersion', { ver: '1.0.0' });

	// console logging enable
	app.config(['$logProvider', function ($logProvider) {
		$logProvider.debugEnabled(true);
	}]);
})();