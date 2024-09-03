function Feedback(id) {
    $.ajax({
        url: '/QueryResponse/FeedbackGet?id=' + id,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (data) {


            $('#queryid').val(data.queryid);
            $('#response').val(data.response);

            $('#reviewer').val(data.reviewer);
            $('#review').val(data.review);
            $('#modalform').modal('show');
        },

        error: function (data) {
            alert('Unable to read the data');
        }
    })

}

function hidemodel() {
    $('#Id').val('');
    $('#Name').val('');
    $('#City').val('');
    $('#Name').css('border-color', 'Lightgrey');
    $('#City').css('border-color', 'Lightgrey');

    $('#modalform').modal('hide');

}
