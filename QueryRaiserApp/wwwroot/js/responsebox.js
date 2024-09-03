
function review(id) {
    $.ajax({
        url: '/QueryResponse/ResponseGet?id='+id,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {

            if (data == "Message Status is Closed") {
                swal(data);
            }
            else if (data =="Database Error"){
                swal(data);
            }
            else {

                $('#queryid').val(data.userqueryid);
                $('#response').val(data.responses);

                $('#reviewer').val(data.submittedby);

                $('#modalform').modal('show');
            }
        },

        error: function (data) {
            alert('Unable to read the data');
        }
    })
}

function update() {
    
    var form = new Object();
    form.queryid = $('#queryid').val();
    form.response = $('#response').val();
    form.review = $('#review').val();
    form.reviewer = $('#reviewer').val();
    var indicator = validateresponse();
    if (indicator) {
        swal("Correctly fill the Message Box");
        hidemodel();
        return indicator;
    }
    $.ajax({
        url: '/QueryResponse/reviewpost',
        data: form,
        type: 'post',
        success: function (response) {
            if (response == "Database Error") {
                swal(response);
            }
            else {

                swal("Good job! Successfully sent a message", "You clicked the button!", "success");
                hidemodel();
              
               
            }
        },
        error: function (response) {
            alert('2Unable to read the data');
        }

    });
}

function validateresponse() {
    var indicate = false;


    if ($('#queryid').val().trim() == "") {
        indicate = true;
    }
  
    if ($('#review').val().trim() == "") {
        indicate = true;
    }
    if ($('#reviewer').val().trim() == "") {
        indicate = true;
    }
    return indicate;
}

function hidemodel() {

     $('#queryid').val("");
    $('#response').val("");
    $('#review').val("");
    $('#reviewer').val("");

    $('#modalform').modal('hide');

}
