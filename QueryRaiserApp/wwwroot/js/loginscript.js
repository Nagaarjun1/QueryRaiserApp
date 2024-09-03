$(document).ready(function () {
    var otpgen = Math.floor(Math.random() * (99999 - 11111 + 1)) + 11111;
    $('#otpset').val(otpgen);
}
);


function submitlogin() {


   

    var Email = $('#email').val();
    var Pass = $('#password').val();

    var indic = validatelogin();
    if (indic) {
        swal("Correctly fill the Loginform");
        return indic;
    }
    var ab = new Object();
    ab.email = Email;
    ab.password = Pass;
    $.ajax({
        url: '/Account/hellofun',
        data: ab,
        type: 'post',

        success: function (response) {
            if ((response.usertypeid == 107) || (response.usertypeid == 108) || (response.usertypeid == 109) || (response.usertypeid == 110) || (response.usertypeid == 111) || (response.usertypeid == 112))
            {
                var email = response.emailid;
                
                
                var role = response.usertypeid;
                window.location.href = '/Home/Student/CC'.replace("CC",email);
            }
            if ((response.usertypeid == 101) || (response.usertypeid == 102) || (response.usertypeid == 103) || (response.usertypeid == 104) || (response.usertypeid == 105) || (response.usertypeid == 106)) 
            {
                var emailemp = response.emailid;
                window.location.href = '/Home/Employ/CC'.replace("CC", emailemp);
            }
          
            
            if (response == "Incorrect Email" || response == "Incorrect Password" || response =="Database Problem"){
                swal(response);
            }
            

              
            
        },
        error: function (response) {
            alert('2Unable to read the data');
        }
    })

}

function validatelogin() {
    var indicator = false;
    if ($('#email').val().trim() == "") {
        indicator = true;
    }
    if ($('#password').val().trim() == "") {
        indicator = true;
    }
    if ($('#otpget').val().trim() == $('#otpset').val().trim()) {
        indicator = false;
    }
    else {

        indicator = true;
    }

    return indicator;
}