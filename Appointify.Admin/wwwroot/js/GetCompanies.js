

$(document).ready(function () {

    var role = $('#roleId').val();

    if (role == 1) {
        getCompanyList();
        $('#companyListId').removeClass('d-none');
    } else {
        $('#companyListId').addClass('d-none');
    }


    $('#roleId').change(function (e) {
        var role = $('#roleId').val();
        console.log("Role: ", role);

        if (role == 1) {
            getCompanyList();
            $('#companyListId').removeClass('d-none');
        } else {
            $('#companyListId').addClass('d-none');
        }
    });


});






function getCompanyList() {
    $.get("/users/getCompanies", function (data) {
        $('#companyId').find('option').remove();
        $(data).each(function (index, item) {
            $('#companyId').append(
                '<option value="' + item.id + '">' + item.name + '</option>'
            );
        });
    });
}



//$.ajax({
//    url: '@Url.Action("GetCompanies","Users")',
//    type: 'GET',
//    success: function (data) {
//        $('#companyId').find('option').remove();
//        $(data).each(function (index, item) {
//            $('#companyId').append(
//                '<option value="' + item.id + '">' + item.name + '</option>'
//            );
//        });
//    },
//    error: function () { },
//});
//  };
