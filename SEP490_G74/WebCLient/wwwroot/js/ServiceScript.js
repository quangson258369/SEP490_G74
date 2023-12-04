let services = [];
var selectedDoctorId = document.getElementById("selected-doctor").value;
function addService(id, name, price) {
     
    let service = {
        sid: id,
        name: name,
        price: price,
        doctorid: selectedDoctorId
    };

    console.log(id, name, price, selectedDoctorId);
    
    const index = services.findIndex(service => {
        return service.sid === id && service.name === name && service.price === price && service.doctorid === selectedDoctorId;
    });
    if (index <= -1) {
        addToTable(service);
        services.push(service);
    }
    
}
function addToTable(service) {
    let name = service.name;
    let price = service.price;

    // Create table row html
    let row = `
    <tr>
      <td>${name}</td>
      <td>${price}</td> 
      <td><button type="button" onclick="RemoveService('${name}',${price})">Xóa</button></td>
    </tr>
  `;

    // Add row to table 
    $('.selected-service-table tbody').append(row);
}

function RemoveService(id, name, price) {

    // Find index of object in services array
    const index = services.findIndex(service => {
        return service.sid === id && service.name === name && service.price === price && service.doctorid === selectedDoctorId;
    });

    if (index > -1) {

        // Remove from services array
        services.splice(index, 1);

        const table = document.getElementById("selected-service");
        // Find and remove table row
        if (table) {
            const rows = table.querySelectorAll('tr');

            rows.forEach(row => {
                if (row.textContent.includes(id)) {
                    row.remove();
                    console.log(services);
                }
            })
        }


    }

}

function populateSelect() {
    document.getElementById("jsonData").value = JSON.stringify(services);
}

function sendService() {
    document.getElementById("jsond").value = JSON.stringify(services);
}

$('#submitBtn').on('click', () => {

    // Add array as hidden input
    let input = document.createElement('input');
    input.type = 'hidden';
    input.name = 'selectedServices';
    input.value = JSON.stringify(services);

    document.getElementById('serviceForm').appendChild(input);

    // Submit form
    document.getElementById('serviceForm').submit();

});