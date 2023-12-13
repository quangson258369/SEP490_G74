let services = [];

function addService(name, price) {
  let service = {
    name: name,
    price: price
  };
  console.log(name, price);
  services.push(service);
  addToTable(service);

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

function RemoveService(name, price) {

  // Find index of object in services array
  const index = services.findIndex(service => {
    return service.name === name && service.price === price;
  });

  if (index > -1) {

    // Remove from services array
    services.splice(index, 1);

    // Find and remove table row
    const rows = document.querySelectorAll('tr');

    rows.forEach(row => {
      if (row.textContent.includes(name)) {
        row.remove();
      }
    })

  }

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