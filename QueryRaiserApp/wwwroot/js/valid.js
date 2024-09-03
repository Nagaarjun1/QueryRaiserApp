
$(document).ready(function () {
    $('.emailvalidation').keypress(function () {
        console.log('hello');
        let a = $('#email').val();
        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if (pattern.test(a)) {
            $('#email').removeClass('is-invalid');
            $('#email').addClass('is-valid');
        }
        else {
            $('#email').removeClass('is-valid');
            $('#email').addClass('is-invalid');
        }

    });

    $('#password').keypress(function () {

        let passWord = $('#password').val();
        var passwordPattern = /^(?=.*\d)(?=(.*\W){2})(?=.*[a-zA-Z])(?!.*\s).{1,15}$/;

        if (passwordPattern.test(passWord)) {
            $('#password').removeClass('is-invalid');
            $('#password').addClass('is-valid');
        }
        else {
            $('#password').removeClass('is-valid');
            $('#password').addClass('is-invalid');
        }

    });

});