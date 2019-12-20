$(document).ready(function () {
    var index = new Index();
})
class Index {
    constructor() {
        $('#addexamperiod').on('click', this.AddExamPeriod.bind(this));
        this.GetExamIsActive();
        $('#changeexamperiod').on('click', this.ChangeExamActive.bind(this));
    }
/*
*Kiểm tra các trường nhập đúng chưa
*/
    checkInvalidInput() {
        var check1 = true, check2 = true;
        $('#ExamPeriodNameSpan').empty();
        $('#ExamPeriodYearSpan').empty();
        var getperiodname = $('#getexamname').val();
        var getperiodyear = $('#getexamperiod').val();
        if (getperiodname == "") {
            $('#ExamPeriodNameSpan').append("<p> Không được để trống</p>");
            check1 = false;
        }
        if (getperiodyear == "Chọn kì học") {
            $('#ExamPeriodYearSpan').append("<p> Không được để trống</p>");
            check2 = false;
        }
        if (check1 && check2) {
            return true;
        }
        else {
            return false;

        }
    }
/*
*Thêm một kì thi
*/
    AddExamPeriod() {
        var me = this;
        var x = this.checkInvalidInput();
        if (x == true) {
            var Data = [];
            var list = [];
            var getperiodyear = $('#getexamperiod').val();
            var begin = getperiodyear.substring(0, 4);
            var end = getperiodyear.substring(5, 9);
            Data.push($('#getexamname').val());
            Data.push(begin);
            Data.push(end);
            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: '/admin/home/addexamperiod',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(Data),
                async: false,
                success: function (res) {
                    list = res.data;
                    var a = '<div class="col-xl-3 col-sm-6 mb-3"' + 'id=' + list[0] + '>'+
                        '<div class="card text-white bg-warning o-hidden h-100">'+
                            '<div class="card-body">'+
                                '<div class="card-body-icon">'+
                                    '<i class="fas fa-fw fa-list"></i>'+
                                '</div>'+
                                '<div class="mr-5">'+list[2]+'-'+list[3]+'</div>'+
                            '</div>'+
                            '<a class="card-footer text-white clearfix small z-1" href="#">'+
                                '<span class="float-left">'+list[1]+'</span>'+
                                '<span class="float-right">'+
                                    '<i class="fas fa-angle-right"></i>'+
                                '</span>'+
                            '</a>'+
                        '</div>'+
                        '</div>'
                    $('.flex-sm-nowrap').append(a);
                    var mess = res.Message;
                },
                error: function (res) {
                    alert("Khong ket noi duoc");
                }
            })
        }
    }
/*
*Lấy từ server kì thi đang tiến hành
*/
    GetExamIsActive() {
        var data = [];
        $.ajax({
            method: 'GET',
            dataType: 'json',
            url: '/admin/home/getexamsactive',
            async: false,
            success: function (res) {
                var data = res.data;
                var x = $('.examperiodactive');
                x.empty();
                x[0].setAttribute('id', data[0]["examid"]);
                x[0].innerHTML = data[0]["examname"];
            },
            error: function (res) {
                alert("abcd");
            }
        })
    }
/*
*Lấy từ server các mã của kì thi
*/
    getExamsIds() {
        var id;
        var exams = $('#getexamactive').val();
        var data = $('#getexamactive option');
        $.each(data, function (index, item) {

            if (item.value == exams) {
                id = item.getAttribute('id');

            }
        })
        return id;
    }
/*
*Sửa kì thi đang tiến hành
*/
    ChangeExamActive() {
        var me = this;
        var x = this.getExamsIds();
        $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/admin/home/updateexamsactive',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(x),
            async: false,
            success: function (res) {
                alert("Thành công");
                me.GetExamIsActive();
            },
            error: function (res) {
                alert("abcd");
            }
        })
    }
    }