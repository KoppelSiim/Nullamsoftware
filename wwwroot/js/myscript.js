


$(document).ready(function () {
    
    $.ajax({
        type: "GET",
        url: "/Home/ReturnEventList",
        success: function (data) {
            let output = data.map(i => "<li>" + i.yritusenimi + "</li>");
            let output2 = data.map(i => "<li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0,4) + "</li>");
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
    


    $("#1").click(function () {
        var id = "1";
        localStorage.setItem('linkid', id);
    });
    $("#2").click(function () {
        var id = "2";
        localStorage.setItem('linkid', id);
    });
    $("#3").click(function () {
        var id = "3";
        localStorage.setItem('linkid', id);
    });
    $("#4").click(function () {
        var id = "4";
        localStorage.setItem('linkid', id);
    });
    $("#5").click(function () {
        var id = "5";
        localStorage.setItem('linkid', id);
    });


    function callControllerMethod() {
        const linkid = localStorage.getItem('linkid');
       
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/Home/GetLinkId");
        xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4 && xhr.status == 200) {
                var response = JSON.parse(xhr.responseText);
                // alert(response["koht"]);
                $("#esimenel").html("<li>" + response["yritusenimi"] + "</li>");
                $("#teinel").html("<li>" + response["toimumisaeg"] + "</li>");
                $("#kolmasl").html("<li>" + response["koht"] + "</li>");
            }
        };
        
        var id = linkid;
        
        xhr.send("id=" + encodeURIComponent(id));
        localStorage.clear();
    }
    callControllerMethod();
/*
    $.ajax({
        type: "GET",
        url: "/Home/GetLinkId",
        
        success: function (data) {
            let output = data.map(i => "<li>" + i.yritusenimi + "</li><li>" + i.toimumisaeg.slice(8, 10) + "." + i.toimumisaeg.slice(5, 7) + "." + i.toimumisaeg.slice(0, 4) + "</li><li>" + i.koht + "</li>");
            $("#andmehulk").html(output);
        }
        
    });
*/



  
});






function hover(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;
   
}  

function hoverout(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;
    

}  
