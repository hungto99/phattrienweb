function TickRow() {
    $(this).parent().toggleClass('tick');//click lần 1 chọn, click lần 2 bỏ chọn
    var tickclass = $('.tick');//xem có bao nhiêu tr được chọn
    if (tickclass.length == 0) {// nếu không có hàng nào được chọn
        $("#delete").attr("disabled", true);
        $("#edit").attr("disabled", true);
        $("#clone").attr("disabled", true);
        $("#add").attr("disabled", false);
    }
    else if (tickclass.length == 1) {//có một hàng được chọn
        $("#delete").attr("disabled", false);
        $("#edit").attr("disabled", false);
        $("#clone").attr("disabled", false);
        $("#add").attr("disabled", true);
    }
    else {//Nhiều hàng được chọn
        $("#delete").attr("disabled", false);
        $("#edit").attr("disabled", true);
        $("#clone").attr("disabled", true);
        $("#add").attr("disabled", true);
    }
}