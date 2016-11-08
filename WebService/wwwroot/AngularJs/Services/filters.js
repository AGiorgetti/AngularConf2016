(function () {
	"use strict";

	function momentFilter() {
		return function (input, format) {
			if (input != null) {
				var d = moment(input);
				if (d.isValid()) {
					var fmt = format || "L HH:mm";
					return d.format(fmt);
				}
			}
			return input;
		};
	}

	window.app.filter('moment', momentFilter);
})();