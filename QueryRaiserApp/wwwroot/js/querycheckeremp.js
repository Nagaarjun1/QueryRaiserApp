$(document).ready(function () {
    querycheckemp();
});
function querycheckemp() {
    var id = $('#reportmail_emp').text();

    $.ajax({
        url: '/QueryResponse/querychecklist?id=' + id,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;charset=utf-8',
        success: function (response) {

            $('#modalform').modal('show');

            var obj = '';
            $.each(response, function (index, item) {

                obj += '<tr><td>' + item.userqueryid + '</td><td>' + item.submittedby + '</td><td>' + item.query + '</td><td>' + item.typename + '</td><td> <a href="#" class="btn btn-primary btn-sm" onclick="resentview(' + item.userqueryid + ')">Notify</a></td></tr>';
            });
            $('#checkerinserttable').html(obj);

        },
        error: function (response) {

        }
    })
}
function hidemodel() {
    $('#modalform').modal('hide');
}