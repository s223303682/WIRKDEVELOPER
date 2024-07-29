//const addbtn = document.querySelector(".add");
//const input = document.querySelector(".inpu-group");

//function removeInput() {
//    this.parentElement.remove();
//}
//function addInput() {
//    const name = document.createElement("input");
//    name.type = "text";
//    email.placeholder = "Medication";

//    const instrutions = document.createElement("input");
//    instrutions.type = "instrutions";
//    instrutions.placeholder = "Instructions";


//    const Quantity = document.createElement("input");
//    Quantity.type = 1;
//    Quantity.placeholder = "Quantity";

//    const btn = document.createElement("a");
//    btn.className = "delete";
//    btn.innerHTML = "&times";

//    btn.addEventListener("click", removeInput);


//    const flex = document.createElement("div");
//    flex.className = "flex";

//    input.appendChild(flex);
//    flex.appendChild(name);
//    flex.appendChild(email);
//    flex.appendChild(Quantity);
//    flex.appendChild(btn)
//}

//addbtn.addEventListener("click", addInput);

var option= document.getElementById('option')
var add= document.getElementById('add')
var remove = document.getElementById('delete')

add.onclick = function () {
    var newfield = document.createElement('input');
    newfield.setAttribute('type', 'text');
    newfield.setAttribute('name', 'option');
    newfield.setAttribute('class', 'option');
    newfield.setAttribute('six', 50);
    newfield.setAttribute('placeholder', 'name');
    option.appendChild(newfield);

}
remove.onclick = function () {
    var input_tag = option.getElementsByTagName('input');
    if (input_tag.length > 2) {
        option.removeChild(input_tag(input_tag.length) - 1);
    }
}
