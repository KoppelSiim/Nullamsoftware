


$(document).ready(function () {

    

   
    $.ajax({
        type: "GET",
        url: "/Home/ReturnEventList",
        success: function (data) {
            let output = data.map(i => "<li>" + i.yritusenimi + "</li>");
            let output2 = data.map(i => "<li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0, 4) + "</li>");
           
            $('#eventlist').html(output);
            $('#eventdates').html(output2);
           
            
        }
    });
    
    $.ajax({
        type: "GET",
        url: "/Home/ReturnPastEventList",
        success: function (data) {
           let output = data.map(i => "<li>" + i.yritusenimi + "</li>");
           let output2 = data.map(i => "<li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0,4) + "</li>");
            $('#pasteventlist').html(output);
            $('#pasteventdates').html(output2);
        }
    });
    
   
    callControllerMethod();
    

});

function hover(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;

}

function hoverout(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;


}  


function sendId(linkid) {
    
    localStorage.setItem('linkid', linkid);
}


function callControllerMethod() {
    const linkid = localStorage.getItem('linkid');

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Home/GetLinkId");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var response = JSON.parse(xhr.responseText);
            $("#fkval").val(response["id"]);
            $("#Ynimi").html("<li>" + response["yritusenimi"] + "</li>");
            $("#Toimumisaeg").html("<li>" + response["toimumisaeg"].slice(8, 10) + "." + response["toimumisaeg"].slice(5, 7) + "." + response["toimumisaeg"].slice(0, 4) + "</li>");
            $("#Koht").html("<li>" + response["koht"] + "</li>");
        }
    };

    

    xhr.send("linkid=" + encodeURIComponent(linkid));
    localStorage.clear();
}
