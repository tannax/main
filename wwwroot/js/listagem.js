$(document).ready(function() {
    // Listen for changes to the "itensPorPagina" dropdown
    $("#itensPorPagina").change(function() {
      // Get the selected value and convert it to a number
      var value = parseInt($(this).val());
      
      // Check if the value is a number and is within the valid range
      if (isNaN(value) || value < 1 || value > 100) {
        // Display an error message
        $("#itensPorPagina").after("<p class='text-danger'>Please enter a value between 1 and 100</p>");
      } else {
        // Remove any existing error message
        $("#itensPorPagina").next(".text-danger").remove();
      }
    });
  });
  