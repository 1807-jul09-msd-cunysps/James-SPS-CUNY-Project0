function GETfromDB() {
    //var url_1 = "http://jamesbio.azurewebservices.net/roladex/api/James";
    //var url_2 = "http://jamesbio.azurewebsites.net/roladex/api/James";
    var url_3 = "https://jamesbio.azurewebsites.net/roladex/api/James";
    //var url_4 = "https://jamesbio.azurewebservices.net/roladex/api/James";

    alert("The next alert should be a JSON string of everyone in the database. Here we go!")
    //create xhr obj
    var xhreq = new XMLHttpRequest();
    xhreq.open('GET', url_3);
    xhreq.onreadystatechange = function (e) { // call a function when the state changes
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) { // is the state DONE?
            alert(xhreq.responseText);
        }
    }
    xhreq.send();
}


    /*
function checkzipcode() {
    debugger;
    var zipcode = document.queryselector("#zipcode input").value;
    var url = "https://www.zipcodeapi.com/rest/" + clientkey + "/info.json/" + zipcode + "/radians";
    //1. create xhr object
    var xhr = new xmlhttprequest();
    xhr.open('get', url);
    xhr.onreadystatechange = function (e) {    //call a function when the state changes.
        if (this.readystate == xmlhttprequest.done && this.status == 200) {
            var result = xhr.responsetext;
            if (result !== null || result !== "" || result !== undefined) {
                if (result.charat(0) === '"' && result.charat(result.length - 1) === '"') {
                    result = result.substr(1, result.length - 2);
                }
                debugger;
                //locationpopulate(result);
            }
        }
    }
    xhr.send();
}
//*/

