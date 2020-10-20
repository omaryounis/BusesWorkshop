function onlyNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

// Except only numbers and dot (.) for salary textbox
function onlyDotsAndNumbers(event) {
    var charCode = (event.which) ? event.which : event.keyCode
    if (charCode == 46) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
var nullText = "----اختر----";

function OnLostFocus(s, e) {
    if (s.GetValue() != "" && s.GetValue() != null)
        return;

    var input = s.GetInputElement();
    input.style.color = "gray";
    input.value = nullText;
}

function OnGotFocus(s, e) {
    var input = s.GetInputElement();
    if (input.value == nullText) {
        input.value = "";
        input.style.color = "black";
    }
}

function OnInit(s, e) {
    OnLostFocus(s, e);
}