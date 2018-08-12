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

function isNotEmpty(text) {
    return /\S/.test(text);
}

$(document).on('click', '#btn-search', function () {

    var text = $("#search-input").val();

    if (!isNotEmpty(text)) {
        return;
    }

    $.ajax({
        url: "Story/Search",
        type: "POST",
        data: { searchText: text }
    });

});


function setImage(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imageHolder").attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$(document).on('change', '#file-upload', function () {
    var i = $(this).prev('label').clone();
    var file = $('#file-upload')[0].files[0].name;
    $(this).prev('label').text(file);

    setImage(this);
});

function logoff() {
    console.log("logoff");
    $("#logoutForm").submit();
}

$(document).ready(function () {

    $("time.created").timeago();

    //$('#file-upload').change(function () {
    //    debugger;
    //    var i = $(this).prev('label').clone();
    //    var file = $('#file-upload')[0].files[0].name;
    //    $(this).prev('label').text(file);
    //});
   
});