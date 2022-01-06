document.addEventListener("DOMContentLoaded", () => {
	const siteScriptId = "site-script";
	const siteScript = document.getElementById(siteScriptId);

	if (!siteScript) {
		console.warn(`There is no script-tag with id "${siteScriptId}".`);
		return;
	}

	const enabledValue = siteScript.getAttribute("data-analytics-enabled");

	if (!enabledValue || enabledValue.toLowerCase() !== "true") {
		console.log("Analytics not enabled.");
		return;
	}

	const id = parseInt(siteScript.getAttribute("data-analytics-id"));

	if (isNaN(id)) {
		console.error(`The value "${siteScript.getAttribute("data-analytics-id")}" is not a number.`);
		return;
	}

	if (id < 1) {
		console.warn(`The value ${id} is less than 1.`);
		return;
	}

	try {
		const authority = new URL(siteScript.getAttribute("data-analytics-authority"));
	} catch (error) {
		console.error(`The value "${siteScript.getAttribute("data-analytics-authority")}" is not a valid url. ${error}`);
		return;
	}
});