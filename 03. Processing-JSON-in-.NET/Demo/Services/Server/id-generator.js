module.exports = {
	get: function () {
		var lastId = 0;
		return {
			next: function () {
				return lastId += 1;
			}
		};
	}
}