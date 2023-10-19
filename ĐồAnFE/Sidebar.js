const sidebarDiv = document.querySelector('.sidebar');
    
async function includeHTML() {
  const response = await fetch('Sidebar.html');
  const sidebarHTML = await response.text();
  
  
  sidebarDiv.innerHTML = sidebarHTML; 
}

includeHTML();