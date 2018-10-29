﻿function getUrlVars() {
	var vars = [], hash;
	var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
	for (var i = 0; i < hashes.length; i++) {
		hash = hashes[i].split('=');
		vars.push(hash[0]);
		vars[hash[0]] = hash[1];
	}
	return vars;
}

var rules = [
	["rule30", 0, 0, 0, 1, 1, 1, 1, 0],
	["rule54", 0, 0, 1, 1, 0, 1, 1, 0],
	["rule60", 0, 0, 1, 1, 1, 1, 0, 0],
	["rule62", 0, 0, 1, 1, 1, 1, 1, 0],
	["rule90", 0, 1, 0, 1, 1, 0, 1, 0],
	["rule94", 0, 1, 0, 1, 1, 1, 1, 0],
	["rule102", 0, 1, 1, 0, 0, 1, 1, 0],
	["rule110", 0, 1, 1, 0, 1, 1, 1, 0],
	["rule122", 0, 1, 1, 1, 1, 0, 1, 0],
	["rule126", 0, 1, 1, 1, 1, 1, 1, 0],
	["rule150", 1, 0, 0, 1, 0, 1, 1, 0],
	["rule158", 1, 0, 0, 1, 1, 1, 1, 0],
	["rule182", 1, 0, 1, 1, 0, 1, 1, 0],
	["rule188", 1, 0, 1, 1, 1, 1, 0, 0],
	["rule190", 1, 0, 1, 1, 1, 1, 1, 0],
	["rule220", 1, 1, 0, 1, 1, 1, 0, 0],
	["rule222", 1, 1, 0, 1, 1, 1, 1, 0],
	["rule250", 1, 1, 1, 1, 1, 0, 1, 0]
];

function ruleCompute(rule, colorCutoff, neighborPix1, pixel, neighborPix2) {

	if (neighborPix1 > colorCutoff && pixel > colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[1])
	else if (neighborPix1 > colorCutoff && pixel > colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[2])
	else if (neighborPix1 > colorCutoff && pixel < colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[3])
	else if (neighborPix1 > colorCutoff && pixel < colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[4])
	else if (neighborPix1 < colorCutoff && pixel > colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[5])
	else if (neighborPix1 < colorCutoff && pixel > colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[6])
	else if (neighborPix1 < colorCutoff && pixel < colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[7])
	else if (neighborPix1 > colorCutoff && pixel > colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[8])
}

//ranges
var ruleRange = [0, 17];
var colorCutoffRange = [107, 147];

//scaled values
var humidityRule = Math.ceil(convertRange(humidityRange[0], humidityRange[1], ruleRange[0], ruleRange[1], values.Humidity));
var pressureColorCutOff = Math.ceil(convertRange(pressureRange[0], pressureRange[1], colorCutoffRange[0], colorCutoffRange[1], values.Pressure));
console.log("humidity: " + humidityRule);
console.log("pressurecolor: " + pressureColorCutOff);

var factor = 5;

$(document).ready(function () {

	var canvas = document.getElementById("myCanvas");
	var ctx = canvas.getContext("2d");

	var image = new Image();
	if (getUrlVars()["weatherEvent"]) {
		image.src = "/WeatherChime/GetPhoto?weatherEvent=" + getUrlVars()["weatherEvent"];
	}
	else {
		image.src = "/WeatherChime/GetPhoto?request=" + getUrlVars()["request"];
	}
	$(image).on("load", function () {
		ctx.drawImage(image, 0, 0);



		setInterval(function () {
			//var randFactor = Math.random();

			var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);


			var newImage = morphImage(imageData);


			ctx.clearRect(0, 0, canvas.width, canvas.height);
			ctx.putImageData(newImage, 0, 0);
		}, 1000);

	});
});



function morphImage(imageData) {

	var pixels = imageData.data;

	for (var i = 3; i < (pixels.length) - 3; i++) {

		if (ruleCompute(rules[humidityRule], pressureColorCutOff, pixels[i - 3], pixels[i], pixels[i + 3])) {
			if (pixels[i] + factor > 255) {
				pixels[i] = pixels[i] + factor - 255;
			}
			else {
				pixels[i] = pixels[i] + factor;
			}
		}
		else {
			if (pixels[i] - factor < 0) {
				pixels[i] = 255 + pixels[i] - factor;
			}
			else {
				pixels[i] = pixels[i] - factor;
			}
		}
	}
	return imageData;
}

$(document).ready(function () {
	$(".labels").on("click", function () {
		$(".labels").slideToggle();
	});

	$(".mute").on("click", function () {
		if (!environment.model.isPlaying) {
			environment.start();
			$(".mute").html("X");
		}
		else {
			environment.stop();
			$(".mute").html("O");

		}
	});

	if (/Mobi/.test(navigator.userAgent) || /iPhone/.test(navigator.userAgent)) {
		// mobile!

		$(".playOverlay").css({ "display": "block" });

		$(".playOverlay").on("click", function () {
			environment.start();
			$(".playOverlay").css({ "display": "none" });

		});
	}
	else {
		environment.start();

	}
});