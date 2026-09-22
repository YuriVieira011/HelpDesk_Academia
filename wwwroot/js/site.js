// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
 
// Write your JavaScript code.
 
const themeButton = document.getElementById("theme-toggle");
 
if (themeButton) {
 
    // Verifica se o usuário já escolheu um tema
    const savedTheme = localStorage.getItem("theme");
 
    if (savedTheme === "dark") {
        document.body.classList.add("dark-mode");
        themeButton.textContent = "☀️ Modo claro";
    }
 
    themeButton.addEventListener("click", function () {
 
        document.body.classList.toggle("dark-mode");
 
        if (document.body.classList.contains("dark-mode")) {
            localStorage.setItem("theme", "dark");
            themeButton.textContent = "☀️ Modo claro";
        } else {
            localStorage.setItem("theme", "light");
            themeButton.textContent = "🌙 Modo escuro";
        }
    });
}