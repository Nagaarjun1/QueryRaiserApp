$(document).ready(function () {
    var otpgen = Math.floor(Math.random() * (99999 - 11111 + 1)) + 11111;
    $('#otpset').val(otpgen);
}
);

function Create() {
    let regname = $('#regname').val();
    let regemail = $('#regemail').val();
    let regpassword = $('#regpassword').val();
    let conpassword = $('#conpassword').val();
    let role;
    if ($('#sturadio').is(":checked")) {
        role = $('#sturadio').val();
    }
    if ($('#empradio').is(":checked")) {
        role = $('#Employposition').val();
    }
 
    var otpget = $('#otpget').val();
    var otpset = $('#otpset').val();
    var valider = validationreg();
    if (valider === false) {
        swal("Correctly fill the Registerform");
        $('#regname').val("");
        $('#regemail').val("");
        $('#regpassword').val("");
        $('#conpassword').val("");
        return false;
    }
   
     
    
 

    var data = new Object();
    data.username = regname;
    data.emailid = regemail;
    data.userpassword = regpassword;
    data.usertypeid = role;

    $.ajax({
        url: '/Account/registerdata',
        data: data,
        type: 'post',

        success: function (response) {
            if (response == "Email Already Have Account" || response == "Data not insert") {
                swal(response);
                 $('#regname').val("");
                 $('#regemail').val("");
                 $('#regpassword').val("");
                $('#conpassword').val("");
            }
            else {
                window.location.href = '/Account/login';
            }
               

            
        },
        error: function (response) {
            alert('2Unable to read the data');
        }
    })
    
}

$('#empradio').click(function () {
    $('#empdrop').css('display', 'block');
    $('Employposition').prop("disabled",false);
});

$('#sturadio').click(function () {
    $('#empdrop').css('display', 'none');
    $('Employposition').prop("disabled");
});

function validationreg() {
    let regname = $('#regname').val();
    let regemail = $('#regemail').val();
    let regpassword = $('#regpassword').val();
    let conpassword = $('#conpassword').val();
    let sturadio = $('#sturadio').val();
    let empradio = $('#empradio').val();
    let emprole = $('#Employposition').val();

    var inticator;
    const namepattern = /^[0-9A-Za-z]{6,16}$/;
    var emailpattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;
    var passwordPattern = /^(?=.*\d)(?=(.*\W){2})(?=.*[a-zA-Z])(?!.*\s).{1,15}$/;
    if (namepattern.test(regname)) {
        inticator = true;
    }
    else {
        inticator = false;
    }

    if (emailpattern.test(regemail)) {
        inticator = true;
    }
    else {
        inticator = false;
    }
    if ($('#otpget').val().trim() === $('#otpset').val().trim()) {
        indicator = true;
    }
    else {

        indicator = false;
    }


    if (passwordPattern.test(regpassword) && (regpassword == conpassword)) {
        
            inticator = true;
        
        
    }
    else
    {
        inticator = false;
    }
  
  

    return inticator;

}


$(document).ready(function () {

    $('#regname').keypress(function () {
        console.log('hello');
        let namevalue = $('#regname').val();
        const namepattern1 = /^[0-9A-Za-z]{6,16}$/;

        if (namepattern1.test(namevalue)) {
            $('#regname').removeClass('is-invalid');
            $('#regname').addClass('is-valid');
        }
        else {
            $('#regname').removeClass('is-valid');
            $('#regname').addClass('is-invalid');
        }

    });


    $('#regemail').keypress(function () {
        console.log('hello');
        let emailvalue = $('#regemail').val();
        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if (pattern.test(emailvalue)) {
            $('#regemail').removeClass('is-invalid');
            $('#regemail').addClass('is-valid');
        }
        else {
            $('#regemail').removeClass('is-valid');
            $('#regemail').addClass('is-invalid');
        }

    });

    $('#regpassword').keypress(function () {

        let passWord = $('#regpassword').val();
        var passwordPattern = /^(?=.*\d)(?=(.*\W){2})(?=.*[a-zA-Z])(?!.*\s).{1,15}$/;

        if (passwordPattern.test(passWord)) {
            $('#regpassword').removeClass('is-invalid');
            $('#regpassword').addClass('is-valid');
        }
        else {
            $('#regpassword').removeClass('is-valid');
            $('#regpassword').addClass('is-invalid');
        }

    });

    $('#conpassword').keypress(function () {

        var passwordPattern1 = /^(?=.*\d)(?=(.*\W){2})(?=.*[a-zA-Z])(?!.*\s).{1,15}$/;
        var passwordnew = $('#conpassword').val();

        if (passwordPattern1.test(passwordnew)) {
            $('#conpassword').removeClass('is-invalid');
            $('#conpassword').addClass('is-valid');
        }
        else {
            $('#conpassword').removeClass('is-valid');
            $('#conpassword').addClass('is-invalid');
        }

    });

});