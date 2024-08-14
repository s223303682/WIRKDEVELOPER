$(document).ready(function () {


    // when the state dropdown changes
    $("#country").change(function () {

        // check country
        if ($(this).val() == "USA") {
            // land of the free
            $(".usa").show();
            $(".can").hide();
            // clear the values in case they picked the wrong country
            $(".can select").val("");
        } else if ($(this).val() == "CAN") {
            // oh canada
            $(".usa").hide();
            $(".can").show();
            // clear the values in case they picked the wrong country
            $(".usa select").val("");
        }






    }); // on country select change END






    // document ready  
});



const notesContainer = document.querySelector(".note-container");
const createBtn = document.querySelector(".btn");
let notes = document.querySelectorAll(".input-box");

function shownotes() {
    notesContainer.innerHTML = localStorage.getItem("notes");
}
function updateStorage() {
    localStorage.setItem("notes", notesContainer.innerHTML);

}
shownotes();
createBtn.addEventListener("click", () => {
    let inputBox = document.createElement("p");
    let img = document.createElement("img");
    inputBox.className = "input-box";
    inputBox.setAttribute("contenteditable", "true");
    img.src = "~/Images/Surgeon/delete.jpeg";
    notesContainer.appendChild(inputBox).appendChild(img);
})
notesContainer.addEventListener("click", function (e) {
    if (e.target.tagName === "IMG") {
        e.target.parentElement.remove();
        updateStorage();
    }
    else if (e.target.tagName === "P") {
        notes = document.querySelectorAll(".input-box");
        notes.forEach(nt => {
            nt.onkeyup = function () {
                updateStorage();
            }
        })
    }


})
document.addEventListener("keydown", event => {
    if (event.key === "Enter") {
        document.execCommand("insertLineBreak");
        event.preventDefault();
    }
})