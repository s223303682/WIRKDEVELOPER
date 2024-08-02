
var option= document.getElementById('option')
var option2= document.getElementById('option2')
var option3= document.getElementById('option3')
var add= document.getElementById('add_more')
var remove = document.getElementById('remove')

add_more.onclick = function () {
    var newfield = document.createElement('input');
    newfield.setAttribute('type', 'text');
    newfield.setAttribute('name', 'option[]');
    newfield.setAttribute('class', 'option');
    newfield.setAttribute('size', 50);
    newfield.setAttribute('placeholder', 'add medication');
    option.appendChild(newfield);

    var newfield2 = document.createElement('input');
    newfield2.setAttribute('type', 'text');
    newfield2.setAttribute('name', 'option2[]');
    newfield2.setAttribute('class', 'option2');
    newfield.setAttribute('size', 50);
    newfield2.setAttribute('placeholder', 'add  Quantity');
    option2.appendChild(newfield2);

    var newfield3 = document.createElement('input');
    newfield3.setAttribute('type', 'text');
    newfield3.setAttribute('name', 'option3[]');
    newfield3.setAttribute('class', 'option3');
    newfield3.setAttribute('size', 50);
    newfield3.setAttribute('placeholder', 'add  Instructions');
    option3.appendChild(newfield3);

}
remove.onclick = function () {
    var input_tag = option.getElementsByTagName('input');
    var input_tag2 = option2.getElementsByTagName('input');
    var input_tag3 = option3.getElementsByTagName('input');
    if (input_tag.length > 2) {
        option.removeChild(input_tag[(input_tag.length) - 1]);
    }
    else if (input_tag2.length > 2) {
        option2.removeChild(input_tag2[(input_tag2.length) - 1]);
    }
   else if (input_tag3.length > 2) {
        option3.removeChild(input_tag3[(input_tag3.length) - 1]);
    }
    
}
