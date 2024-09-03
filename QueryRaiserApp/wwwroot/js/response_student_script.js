$(document).ready(function () {

    information();
});

function information() {
    $.ajax({
        url: '/QueryResponse/response_student',
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {
            console.log(response);
            if (response == null || response == undefined || response.length == 0) {
                $('#tablebody').html('<tr><td class="text-center" colspan="5">Information not available</td></tr>');
            }
            else {
                var obj = '';
                $.each(response, function (index, item) {
             
                    obj += '<tr><td>' + item.queryid + '</td><td>' + item.role + '</td><td>' + item.username + '</td><td>' + item.email + '</td><td>' + item.priority + '</td><td>' + item.question + '</td><td> <a href="#" class="btn btn-primary btn-sm" onclick="edit(' + item.queryid + ')">Replay</a></tr>';
                });
                $('#tablebody').html(obj);

            }
        },

        error: function (response) {
            alert('Unable to read the data');
        }
    })
}
