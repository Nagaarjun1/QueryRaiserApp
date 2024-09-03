function reportsubmit() {
   
  
    var submittedby= $('#reportmail').text();

    var usertypeid = $('#studentQueryposition').val();
   
    var priorityid = $('#priorityQueryposition').val();
    var Query = $('#StudentQuery').val();

    var indicate = validator();

    if (indicate) {
        swal("Correctly fill the Queryform");
        return indicate;
    }

    var data = new Object();

    data.submittedby = submittedby;
    data.usertypeid = usertypeid;
    data.priorityid = priorityid;
    data.query = Query;

    $.ajax({
        url: '/QueryResponse/reportsave',
        type: 'post',
        data: data,
        success: function (response) {
            //   $('#reportcontainer').prepend('<div class="alert container alert-success alert-dismissible fade show">    <strong>Successfully Sent Query !</strong>  <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><span aria-hidden="true"></span></button>  </div>');
            if (response == "sucess") {
                swal("Good job! Successfully raise your qurey", "You clicked the button!", "success");
                $('#StudentQuery').val("");
            }
            else {
                swal(response);
            }
        },
        error:function(response){

        }
    })
}

function validator() {
    var indication = false;

    if ($('#StudentQuery').val().trim() == "") {
        indication = true;
    }
    return indication;
}

