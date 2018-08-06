//var applicationKey = "ZljFCQQh5vj1ruw9fE2Hlbeb2WRk9iASaJwvUPRWeUzqMJmI4C59mo2ObEdAiEBt";
//NOT NECESSARY FOR THE CLIENT SIDE

var clientKey = "js-S2A4yglbnZxu6JvAdi2y5dIlvFtP7Afu61YOdfudD8kaDfG05cQZUyyDvsr6bT27"


function locationPopulate(json) {
    debugger;
    // var json = { "zip_code": "10017", "lat": 0.711263, "lng": -1.29106, "city": "New York", "state": "NY", "timezone": { "timezone_identifier": "America\/New_York", "timezone_abbr": "EDT", "utc_offset_sec": -14400, "is_dst": "T" }, "acceptable_city_names": [{ "city": "Grand Central", "state": "NY" }, { "city": "Manhattan", "state": "NY" }, { "city": "Nyc", "state": "NY" }] };
    json = JSON.parse(json);
    var state = document.querySelector("#state .data");
    var city = document.querySelector("#city .data");
    state.value = json.state;
    city.value = json.city;
}


function checkZipcode() {
    debugger;
    var zipcode = document.querySelector("#zipcode input").value;
    var url = "https://www.zipcodeapi.com/rest/" + clientKey + "/info.json/" + zipcode + "/radians";
    //1. create XHR object
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);
    xhr.onreadystatechange = function (e) {    //Call a function when the state changes.
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            var result = xhr.responseText;
            if (result !== null || result !== "" || result !== undefined) {
                if (result.charAt(0) === '"' && result.charAt(result.length - 1) === '"') {
                    result = result.substr(1, result.length - 2);
                }
                debugger;
                locationPopulate(result);
            }
        }
    }
    xhr.send();
}
