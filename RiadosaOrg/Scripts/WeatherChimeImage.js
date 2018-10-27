function getUrlVars() {
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

var factor,
	canvas,
	ctx,
	width,
	height,
	len,
	result,
	offset,
	delta,
	value;


$(document).ready(function () {


	factor = 5;
	canvas = document.getElementById("myCanvas");
	ctx = canvas.getContext("2d"),
		width = canvas.width,
		height = canvas.height,
		len = 4 * width * height,
		result = ctx.createImageData(width, height),
		offset = new Array(len),
		delta = new Array(len),
		value = 0;

	var seedImage = new Image();


	seedImage.src = "/WeatherChime/GetPhoto?request=" + getUrlVars()["request"];



	$(seedImage).on("load", function () {

		ctx.drawImage(seedImage, 0, 0);
		currentImage = ctx.getImageData(0, 0, canvas.width, canvas.height);
		var morphedImage = morphImage(currentImage);

		setInterval(function () {
			currentImage = new ImageData(width, height);
			currentImage.data = morphedImage.data.slice(0);
		morphedImage = morphImage(currentImage);


		

			result = ctx.createImageData(width, height);
		for (i = 0; i < len; i += 1) {
			offset[i] = morphedImage.data[i];
				delta[i] = currentImage.data[i] - morphedImage.data[i];
				result.data[i] = 255;
			}

			//ctx.fillStyle = '#fff';
			//ctx.fillRect(0, 0, width, height);

		
		
			//ctx.putImageData(newImage, 0, 0);

		}, 1000);
		start();

	});
});

function morphImage(imageData) {
	
	var pixels = imageData.data;
		var newImage = ctx.createImageData(width, height);

	for (var i = 3; i < (pixels.length) - 3; i++) {

		if (ruleCompute(rules[humidityRule], pressureColorCutOff, pixels[i - 3], pixels[i], pixels[i + 3])) {
			if (pixels[i] + factor > 255) {
				newImage.data[i] = pixels[i] + factor - 255;
			}
			else {
				newImage.data[i] = pixels[i] + factor;
			}
		}
		else {
			if (pixels[i] - factor < 0) {
				newImage.data[i] = 255 + pixels[i] - factor;
			}
			else {
				newImage.data[i] = pixels[i] - factor;
			}
		}
	}

	return newImage;
}





function tween(factor) {
	var i, r;
	r = result.data;
	for (i = 0; i < len; i += 4) {
		r[i] = offset[i] + delta[i] * factor;
		r[i + 1] = offset[i + 1] + delta[i + 1] * factor;
		r[i + 2] = offset[i + 2] + delta[i + 2] * factor;
	}
	ctx.putImageData(result, 0, 0);
	frames += 1;
}

function start() {
	value = 0;
	timestamp = Date.now();
	ticker = window.setInterval(function () {
		value += 0.1;
		tween(0.5 + 0.5 * Math.sin(value));
	}, 1000 / 60);
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
		//environment.start();

	}
});