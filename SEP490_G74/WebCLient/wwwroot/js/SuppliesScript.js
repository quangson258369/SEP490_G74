let supplies = [];
var id = 1;
function addSupplies(name,idOfSupplies) {
    let supplise = {
        ids : id,
        name: name,
        idOfSupplies: idOfSupplies,
    };
    console.log(id, name);
    addToTable(supplise);
    id += 1;
    supplies.push(supplise);
}
function addToTable(supplise) {
    let ids = supplise.ids;
    let name = supplise.name;
    let idOfSupplies=supplise.idOfSupplies
    // Create table row html
    let row = `
    <tr data-ids="${ids}">
      <td>${ids}</td>
      <td>${name}</td>
      <input type="hidden" name = "IdOfSupplies" value="${idOfSupplies}"/>
      <td><input type="number" min="0" class="form - control"name = "UnitInStock" /></td> 
      <td><button type="button" onclick="RemoveSupplies('${ids}','${name}')">Xóa</button></td>
    </tr>
    `;

    // Add row to table 
    $('.Selected-supplies-table tbody').append(row);
}

function RemoveSupplies(ids,name,price) {   
    console.log(supplies);
    // Find index of object in services array
    const index = supplies.findIndex(supplise => {
        return supplise.ids === ids && supplise.name === name && supplise.price === price
    });
    console.log(ids+name+price);
    console.log(index);
    if (index > -1) {

        supplies.splice(index, 1);

        // Find and remove table row
        const rowToRemove = document.querySelector(`.selected-service-table tbody tr[data-ids="${ids}"]`);

        if (rowToRemove) {
            console.log(`Removing row with ids ${ids}`);
            rowToRemove.remove();
        } else {
            console.log(`Row with ids ${ids} not found`);
        }

    }

}
$('#submitBtn').on('click', () => {

    // Add array as hidden input
    let input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'selectedServices';
    input.value = JSON.stringify(supplies);

    document.getElementById('serviceForm').appendChild(input);

    // Submit form
    document.getElementById('serviceForm').submit();

});