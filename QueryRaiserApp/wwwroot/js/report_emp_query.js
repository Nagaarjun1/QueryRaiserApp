function empreportsubmit() {
    
    
    var submittedby = $('#reportmail_emp').text();

    var usertypeid = $('#empQueryposition').val();
    var priorityid = $('#emppriorityQueryposition').val();
    var query = $('#empQuery').val();
    var indicate = validate();

    if (indicate) {
        swal("Correctly fill the Queryform");
        return indicate;
    }
    var data = new Object();
    data.submittedby = submittedby;
    data.usertypeid = usertypeid;
    data.priorityid = priorityid;
    data.query = query;

    $.ajax({
        url: '/QueryResponse/reportsaveemp',
        type: 'post',
        data: data,
        success: function (response) {
            //  $('#reportcontainer').prepend('<div class="alert container alert-success alert-dismissible fade show">    <strong>Successfully Sent Query !</strong>  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><span aria-hidden="true"></span></button>  </div>');
            if (response == "sucess") {
                swal("Good job! Successfully raise your qurey", "You clicked the button!", "success");
                $('#empQuery').val("");
            }
            else {
                swal(response);
            }
        },
        error: function (response) {

        }
    })
}

function validate() {
    var validater = false;

    if ($('#empQuery').val().trim() == "") {
        validater = true;
    }
    return validater;
}
