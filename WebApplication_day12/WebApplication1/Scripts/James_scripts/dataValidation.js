function checkAge() {
    var age_element = document.querySelector("#age .data");
    var age = age_element.value;
    var valid = true;
    if (isNaN(age)) valid = false;
    if (age < 15 || age > 100) valid = false;
    if (!valid) {
        age_element.classList.remove("yesvalid");
        age_element.classList.add("notvalid");
    }
    else {
        age_element.classList.remove("notvalid");
        age_element.classList.add("yesvalid");
        
        //age_element.classList.replace("notvalid","yesvalid")
        //classList.replace() won't add "yesvalid" unless "notvalid already present
    }
}

function eatCereal() {
    var myform = document.forms[0] //pulls the form (there's only 1 form, hence index 0)
    var result_json = myform 
}


