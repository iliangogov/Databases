var express = require('express'),
	bodyParser = require('body-parser'),
	bounds = require('binary-search-bounds'),
	lowdb = require('lowdb');

var idGen = require('./id-generator').get();

var app = express(),
	db = lowdb('./data.json');

db._.mixin(require('underscore-db'));

app.use(bodyParser.json());

function findPlace(item) {
	return bounds.gt(data, item, function (i1, i2) {
		return (i1.name || '').toLowerCase().localeCompare(
			(i2.name || '').toLowerCase());
	});
}

app.get('/api/items', function (req, res) {
	res.json({
		result: db('items')
			.sortBy('name')
	});
});

app.post('/api/items', function (req, res) {
	var item = req.body;
	if (!item.name || typeof item.name !== 'string') {
		res.status(400)
			.json('Invalid item name');
		return;
	}

	// var index = findPlace(item);
	db('items').insert(item);
	// data.splice(index, 0, item);
	res.status(201)
		.json({
			result: item
		});
});

var port = 3000;

var server = app.listen(port, function () {
	console.log(`Server is running at http://localhost:${port}`);
});