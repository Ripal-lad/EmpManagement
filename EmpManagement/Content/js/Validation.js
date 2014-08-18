function validation() {
    var name = document.getElementById("Name");
    var Dept = document.getElementById("DeptID");
    var designation = document.getElementById("Designation");
    var contactno = document.getElementById("ContactNo");
    var emailid = document.getElementById("Emailid");

   
}
var letters = /^[a-zA-Z]+$/
if (name.match.letters)
{
    alert("Hello");
    document.getElementById("name").value = "Text only";
    document.getElementById("name").style.visibility = true;
}