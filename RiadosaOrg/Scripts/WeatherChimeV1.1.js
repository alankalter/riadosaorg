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
		return Boolean(rule[1]);
	else if (neighborPix1 > colorCutoff && pixel > colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[2]);
	else if (neighborPix1 > colorCutoff && pixel < colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[3]);
	else if (neighborPix1 > colorCutoff && pixel < colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[4]);
	else if (neighborPix1 < colorCutoff && pixel > colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[5]);
	else if (neighborPix1 < colorCutoff && pixel > colorCutoff && neighborPix2 < colorCutoff)
		return Boolean(rule[6]);
	else if (neighborPix1 < colorCutoff && pixel < colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[7]);
	else if (neighborPix1 > colorCutoff && pixel > colorCutoff && neighborPix2 > colorCutoff)
		return Boolean(rule[8]);
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
	context,
	width,
	height,
	len,
	result,
	offset,
	delta;

$(document).ready(function () {


	factor = 5;
	canvas = document.getElementById("myCanvas");
	context = canvas.getContext("2d"),
		width = canvas.width,
		height = canvas.height,
		len = 4 * width * height,
		result = context.createImageData(width, height),
		offset = new Array(len),
		delta = new Array(len);

	var seedImage = new Image();


	seedImage.src = "/WeatherChime/GetPhoto?request=" + getUrlVars()["request"];


	$(seedImage).on("load", function () {


		context.drawImage(seedImage, 0, 0);

		setInterval(function () {
			source = context.getImageData(0, 0, width, height);
			target = morphImage(source);

			result = context.createImageData(width, height);
			for (i = 0; i < len; i += 1) {
				offset[i] = target.data[i];
				delta[i] = source.data[i] - target.data[i];
				result.data[i] = 255;
			}

			
			crossfade();
		}, 1200);

	});
});

function morphImage(imageData) {
	
	var pixels = imageData.data;
		var newImage = context.createImageData(width, height);

	for (var i = 3; i < pixels.length-3; i++) {
		
		var ruleComputed = ruleCompute(rules[humidityRule], pressureColorCutOff, pixels[i - 3], pixels[i], pixels[i + 3]);
		
		if (ruleComputed) {

			if (pixels[i] + factor > 255) {
				newpixel = pixels[i] + factor - 255;
				newImage.data[i] = newpixel;
			}
			else {
				newpixel = pixels[i] + factor;
				newImage.data[i] = newpixel;
			}
		}
		else {
			if (pixels[i] - factor < 0) {
				newpixel = 255 + pixels[i] - factor;
				newImage.data[i] = newpixel;
			}
			else {
				newpixel = pixels[i] - factor;
				newImage.data[i] = newpixel;
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
	context.putImageData(result, 0, 0);
}

function crossfade() {
	timestamp = Date.now();
	var factor = 1;

	ticker = window.setInterval(function () {
		factor -= 0.08;
		tween(factor);
		if (factor < 0.5) { clearInterval(ticker); }
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
		environment.start();

	}
});