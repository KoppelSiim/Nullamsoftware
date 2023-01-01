

// Start the function when the document has finished loading
$(document).ready(function () {

    // Using ajax to dynamically call the HomeController:ReturnPList.
    $.ajax({
        type: "GET",
        url: "/Home/ReturnPList",   
        success: function (data) { // Receave the data in json format
            // Map the data using data.map and add html elements to it
            let output = data.map(i => "<li>" + i.eesnimi + "&nbsp;" + i.perenimi + "&nbsp;" + i.isikukood + "</li>");
            // Insert the data into the page
            $('#participantslist').html(output);
        }
    });

   // Using ajax to dynamically call the HomeController:ReturnEventList.
    $.ajax({
        type: "GET",
        url: "/Home/ReturnEventList",
        success: function (data) { // Receave the data in Json format
            // Map the data and add html to it
            let output = data.map(i => "<li>" + i.yritusenimi + "</li>");
            // I use slice to form the correct datestring
            let output2 = data.map(i => "<li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0, 4) + "</li>");
            // Add the date to the page
            $('#eventlist').html(output);
            $('#eventdates').html(output2);
        }
    });

      // Using ajax to dynamically call the HomeController:ReturnPastEventList.
    $.ajax({
        type: "GET",
        url: "/Home/ReturnPastEventList",
        success: function (data) { // Receave the data in Json Format
          // Map the data and add html to it
          let output = data.map(i => "<li>" + i.yritusenimi + "</li>");
          // Map the data, add html and use slice form the correct datestring
          let output2 = data.map(i => "<li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0, 4) + "</li>");
          // Insert data into the page
          $('#pasteventlist').html(output);
          $('#pasteventdates').html(output2);
        }
    });
    
    callControllerMethod();
    
});

// Change the navbar links and nav background colors when the user hovers over them
function hover(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;

}
// Change the colors again when the users cursor leaves the element
function hoverout(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;
}  

// I get the id of the link clicked, it corresponds to the event cliked, i store the value in local storage
function sendId(linkid) {
    localStorage.setItem('linkid', linkid);
}

// Here i call my HomeController method to display the details of the event
function callControllerMethod() {
    //Get the linkid variable i stored earlier
    const linkid = localStorage.getItem('linkid');
    // Make a new XMLHttpRequest
    var xhr = new XMLHttpRequest();
    // Call my controller to get the events data
    xhr.open("POST", "/Home/GetLinkId");

    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.onreadystatechange = function () {
           // If we have a successful response with no errors
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Conver the response to Json
            var response = JSON.parse(xhr.responseText);
            // Here, i fill the the hidden div with the foreign key value to match participants with the correct event later
            $("#fkval").val(response["id"]);
            // Insert the events name, date and time values into the page
            $("#Ynimi").html("<li>" + response["yritusenimi"] + "</li>");
            $("#Toimumisaeg").html("<li>" + response["toimumisaeg"].slice(8, 10) + "." + response["toimumisaeg"].slice(5, 7) + "." + response["toimumisaeg"].slice(0, 4) + "</li>");
            $("#Koht").html("<li>" + response["koht"] + "</li>");
        }
    };
    // Pass the linkid as a parameter to my controller
    xhr.send("linkid=" + encodeURIComponent(linkid));
    // Clear the local storage so it always contains the link id clicked
    localStorage.clear();
}
