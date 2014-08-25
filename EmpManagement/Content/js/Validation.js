function validation() {
    var name = document.getElementById("Name");
    var Dept = document.getElementById("DeptID");
    var designation = document.getElementById("Designation");
    var contactno = document.getElementById("ContactNo");
    var emailid = document.getElementById("Emailid");
    alert("Hello");


    var letters = /^[a-zA-Z]+$/
    if (name.match.letters) {
        alert("Hello");
        document.getElementById("name").value = "Text only";
        document.getElementById("name").style.visibility = true;
    }

    if (designation.match.letters)
    {
        document.getElementById("designation").value = "Text only";
        document.getElementById("designation").style.visibility = true;
    }

    
    var email = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/

    if (emailid.match.email)
    {
        document.getElementById("emailid").value = "Numbers only";
        document.getElementById("emailid").style.visibility = true;
    }
    
}