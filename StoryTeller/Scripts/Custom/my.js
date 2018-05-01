//import { debounce } from "../esm/popper-utils";


var scroll;

$(document).on('click', '.userDetails', function () {

    scroll = $(window).scrollTop();
    var url = $(this).attr('data-url');
    var userID = $(this).attr('data-userID');

    console.log(url + '/'+userID);

    $.ajax({
        url: url,
        data: { userID: userID },
        success: function (result) {
            $("#userDetails").html(result);
            $('#userModal').modal('toggle');
        },
        error: function (error) {
            alert(error);
        }
    }
    );
});

$(document).on('click', '#closeUserDetails', function () {
    $(document).scrollTop(scroll);
});

$(document).on('change', '#file-upload', function () {
    var i = $(this).prev('label').clone();
    var file = $('#file-upload')[0].files[0].name;
    $(this).prev('label').text(file);

});

$(document).ready(function () {

    //$('#file-upload').change(function () {
    //    debugger;
    //    var i = $(this).prev('label').clone();
    //    var file = $('#file-upload')[0].files[0].name;
    //    $(this).prev('label').text(file);
    //});
   
});