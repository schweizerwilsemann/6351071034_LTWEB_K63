// script.js
function validate() {
    var result = "";
    result += validateName();
    result += validateEmail();
    result += validatePassword();
    result += validateTerms();

    if (result == "") return true;

    alert("Validation Result:\n\n" + result);
    return false;
}
function validateForm() {
    const form = document.forms[0];
    let valid = true;

    form.querySelectorAll("input[required]").forEach(input => {
        if (!input.value) {
            input.classList.add("error");
            valid = false;
        } else {
            input.classList.remove("error");
        }
    });

    if (!valid) {
        alert("Please fill out all required fields.");
    }
    return valid;
}

function validateName() {
    var name = document.getElementsByName("HoTen")[0].value;
    if (name.length <= 3)
        return "Name should be at least three characters.\n";
    return "";
}

function validatePassword() {
    var password = valueOf("Matkhau");
    var retype = valueOf("retype_password");

    if (password.length <= 6)
        return "Password should be at least 6 characters.\n";

    if (password != retype)
        return "Passwords do not match.\n";
    return "";
}

function validateEmail() {
    var email = valueOf("Email");
    var retype = document.getElementsByName("retype_email")[0].value;

    if (email.indexOf('@') == -1)
        return "Email should be a valid address.\n";

    if (email != retype)
        return "Email addresses do not match.\n";
    return "";
}

function validateTerms() {
    var terms = document.getElementsByName("terms")[0];
    if (!terms.checked)
        return "Please accept the Terms of Service and Privacy Policy";
    return "";
}

function valueOf(name) {
    return document.getElementsByName(name)[0].value;
}
