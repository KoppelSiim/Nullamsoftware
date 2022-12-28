

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
});



function hover(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;
   
}  

function hoverout(color, color2, id, id2) {
    document.getElementById(id).style.backgroundColor = color;
    document.getElementById(id2).style.color = color2;
    

}  
