document.addEventListener("DOMContentLoaded", function () {
    const darkModeToggle = document.getElementById("darkModeToggle");
    const themeStylesheet = document.getElementById("themeStylesheet");

    darkModeToggle.addEventListener("click", function () {
        if (themeStylesheet.getAttribute("href").includes("flatly")) {
            themeStylesheet.setAttribute("href", "/css/darkly.min.css");
        } else {
            themeStylesheet.setAttribute("href", "/css/flatly.min.css");
        }
        console.log("Karanlık mod geçişi tıklandı.");
    });
});
