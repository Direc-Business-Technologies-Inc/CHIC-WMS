document.querySelectorAll("li.disable-click").forEach(li => li.addEventListener("click", e => e.preventDefault()));
let isDarkStyle = window.Helpers.isDarkStyle();