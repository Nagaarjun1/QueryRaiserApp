function resentview(id) {
    console.log('hello');
    $.ajax({
        url: '/QueryResponse/resentviewscript?id=' + id,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {

            $('#modalform1').modal('show');

            $('#queryid').val(response.userqueryid);
            $('#reviewer').val(response.submittedby);
           
  
        },
        error: function (response) {

        }
    })
}

function update() {
    var queryid = $('#queryid').val();
    var reviewer = $('#reviewer').val();
    var review = $('#reviewtext').val();
  
    let data = new Object();
    data.queryid = queryid;
    data.review = review;
    data.reviewer = reviewer;

    $.ajax({
        url: '/QueryResponse/reviewpost',
        type: 'post',
        data: data,
    
        success: function (response) {

            swal("Good job! Successfully sent a message", "You clicked the button!", "success");

            $('#modalform1').modal('hide');

  


        },
        error: function (response) {

        }
    })
}

function hidemodel1() {


    $('#modalform1').modal('hide');

}