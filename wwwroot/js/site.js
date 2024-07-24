// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("scroll", () => {
    if (window.scrollY > 10) {
        document.querySelector("header").style.background = "rgba(0, 0, 0, 0.7)";
    } else {
        document.querySelector("header").style.background = "transparent"
    }
})
//Tempdata
setTimeout(function () {
    var tempdat = document.getElementById('tempdata');
    if (tempdat) {
        tempdat.remove();
        tempdat.textContent = '';
    }
}, 3000);
// Wait for the document to load
document.addEventListener('DOMContentLoaded', function () {
    // Check if the student has registered (replace 'hasRegistered' with your condition)
    var hasRegistered = true; // Replace with your logic to determine if the student has registered

    // If the patient has registered, show the modal
    if (hasRegistered) {
        var modal = new bootstrap.Modal(document.getElementById("myModal"));
        modal.show();

        // Add an event listener to close the modal when the 'x' button is clicked
        var closeButton = document.getElementById("close");
        closeButton.addEventListener("click", function () {
            modal.hide();
        });
    }
});
window.onload = function () {
    console.log("works");
}

const btnprint = document.getElementById('PrintBTn');
btnprint.addEventListener("click", printpage);
function printpage() {
    document.getElementById('sidebar').style.visibility = "hidden"
    document.getElementById('NavbarSide').style.visibility = "hidden";
    btnprint.style.visibility = "hidden";
    window.print();
    document.getElementById('sidebar').style.visibility = "visible"
    document.getElementById('NavbarSide').style.visibility = "visible";
    btnprint.style.visibility = "visible";
}