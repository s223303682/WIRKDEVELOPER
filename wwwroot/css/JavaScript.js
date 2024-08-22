
//var option= document.getElementById('option')
//var option2= document.getElementById('option2')
//var option3= document.getElementById('option3')
//var add= document.getElementById('add_more')
//var remove = document.getElementById('remove')

//add_more.onclick = function () {
//    var newfield = document.createElement('input');
//    newfield.setAttribute('type', 'text');
//    newfield.setAttribute('name', 'option[]');
//    newfield.setAttribute('class', 'option');
//    newfield.setAttribute('size', 50);
//    newfield.setAttribute('placeholder', 'add medication');
//    option.appendChild(newfield);

//    var newfield2 = document.createElement('input');
//    newfield2.setAttribute('type', 'number');
//    newfield2.setAttribute('name', 'option2[]');
//    newfield2.setAttribute('class', 'option2');
//    newfield.setAttribute('size', 50);
//    newfield2.setAttribute('placeholder', 'add  Quantity');
//    option2.appendChild(newfield2);

//    var newfield3 = document.createElement('input');
//    newfield3.setAttribute('type', 'text');
//    newfield3.setAttribute('name', 'option3[]');
//    newfield3.setAttribute('class', 'option3');
//    newfield3.setAttribute('size', 50);
//    newfield3.setAttribute('placeholder', 'add  Instructions');
//    option3.appendChild(newfield3);
    
//}
//remove.onclick = function () {
//    var input_tag = option.getElementsByTagName('input');
//    var input_tag2 = option2.getElementsByTagName('input');
//    var input_tag3 = option3.getElementsByTagName('input');
//    if (input_tag.length > 2) {
//        option.removeChild(input_tag[(input_tag.length) - 1]);
//    }
//    else if (input_tag2.length > 2) {
//        option2.removeChild(input_tag2[(input_tag2.length) - 1]);
//    }
//   else if (input_tag3.length > 2) {
//        option3.removeChild(input_tag3[(input_tag3.length) - 1]);
//    }
    
//}
(function () {
    var button = document.getElementById("add-user");
    button.addEventListener('click', function (event) {
        event.preventDefault();
        var cln = document.getElementsByClassName("user")[0].cloneNode(true);
        document.getElementById("users").insertBefore(cln, this);
        return false;
    });
})();


    function selectRow(button) {
        // Get the selected row
        var row = button.parentNode.parentNode;

    // Get the column values
    var column1Value = row.cells[0].innerHTML;
    var column2Value = row.cells[1].innerHTML;

    // Populate the form fields
    document.getElementById("column1").value = column1Value;
    document.getElementById("column2").value = column2Value;
        // Select the table and button
        const table = document.getElementById('myTable');
        const button = document.getElementById('myButton');

        // Add an event listener to the button
        button.addEventListener('click', () => {
            // Select the row (assuming the first row is selected)
            const row = table.rows[1]; // Change the index to select a different row

            // Extract data from the row
            const email = row.cells[1].textContent;
            const age = row.cells[2].textContent;