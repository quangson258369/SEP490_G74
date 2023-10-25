async function includeHTML() {
  const sidebarDiv = document.querySelector('.sidebar');
  const response = await fetch('Sidebar.html');
  const sidebarHTML = await response.text();

  sidebarDiv.innerHTML = sidebarHTML; 

  // Initialize after the content is loaded
  initializeNavLinks();
  setActiveLink();
}

function initializeNavLinks() {
  console.log("Initializing nav links");

  // Function to handle the active state of the links
  function handleNavLinkClick(event) {
      event.preventDefault();  // Stop the link from navigating immediately

      // Remove active-link class from all links
      const navLinks = document.querySelectorAll(".nav-link");
      navLinks.forEach(function(innerLink) {
          innerLink.parentElement.classList.remove("active-link");
      });

      // Add active-link class to the clicked link
      this.parentElement.classList.add("active-link");

      // Navigate to the desired page
      window.location.href = this.getAttribute("href");
  }

  let navLinks = document.querySelectorAll(".nav-link");
  navLinks.forEach(function(link) {
      link.addEventListener("click", handleNavLinkClick);
  });
}

function setActiveLink() {
  console.log("Setting active link based on URL");

  // Function to check and set the active link based on the current URL
  let currentPath = window.location.pathname;
  let activeItem;

  switch(currentPath) {
      case "/Home.html":
          console.log(currentPath);
          activeItem = document.querySelector('.nav-item-sidebar[data-section="home"]');
          console.log("Trying to select home:", activeItem);
          break;

      case "/UserManage.html":
          console.log(currentPath);
          activeItem = document.querySelector('.nav-item-sidebar[data-section="UserManage"]');
          console.log("Trying to select UserManage:", activeItem);
          break;
          case "/ManageInvoice.html":
            console.log(currentPath);
            activeItem = document.querySelector('.nav-item-sidebar[data-section="Invoice"]');
            console.log("Trying to select Invoice:", activeItem);
            break;
            case "/CreateInvoice.html":
                console.log(currentPath);
                activeItem = document.querySelector('.nav-item-sidebar[data-section="Invoice"]');
                console.log("Trying to select Invoice:", activeItem);
                break;
      // Add more cases for other paths as necessary...
      default:
          console.log("Path not matched:", currentPath);
          break;
  }

  if (activeItem) {
      activeItem.classList.add("active-link");
  } else {
      console.log("No active item found for current path.");
  }
}

// Start the inclusion of the sidebar content
includeHTML();
