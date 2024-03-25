$(document).ready(function () {
    $(".delete-department-link").click(function () {
        var deptId = $(this).data("deptid");
        var deleteUrl = "/department/delete/" + deptId;
        $("#deleteDepartmentLink").attr("href", deleteUrl);
        $("#deleteConfirmationModal").modal("show");
    });
});
function fun1(i) {
    $.ajax({
        url: '/student/details/' + i,
        type: 'GET',
        success: function (result) {
            $('#modal-content').html(result);
            $('#detailsModal').modal('show');
        }
    });
}

$(document).ready(function () {
    $(".delete-student-link").click(function () {
        var studentId = $(this).data("studentid");
        var deleteUrl = "/student/delete/" + studentId;
        $("#deleteStudentLink").attr("href", deleteUrl);
        $("#deleteStudentConfirmationModal").modal("show");
    });
});
