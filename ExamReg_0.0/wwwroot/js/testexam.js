$(document).ready(function () {
    var x = new TestExam();
})
class TestExam {
    constructor() {
        this.loadData("0");
        $('#examTimeTable tbody').on('click', 'td', TickRow);
        var y = new Examination();
        var x = new ExamRoom();
        $('#addTestExam').on('click', this.addTestExam.bind(this));
       
    }
/*
*Hiển thị các ca thi của môn thi trong kì thi đã lấy được
*/
    loadData(subject) {
        var checkbox = '<td>' +
            '<span class="custom-checkbox">' +
            '<input type="checkbox" id="checkbox1" name="options[]" value="1">' +
            '<label for="checkbox1"></label>' +
            '</span>' +
            '</td >';
        var data = this.getData(subject);
        var fields = $('#examTimeTable th[fieldName]');
        $('#examTimeTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr></tr>');
            rowHTML[0].setAttribute('id', item["caseTestId"]);
            rowHTML.append(checkbox);
            $.each(fields, function (fieldIndex, fieldItem) {
                var fieldName = fieldItem.getAttribute('fieldName');
                var value = item[fieldName];
                rowHTML.append('<td class=' + fieldName + '>' + value + '</td>')
            });
            var column = $('<td></td>');
            var link = $('<a></a>');
            var herf = "https://localhost:44302/admin/exams/getallstudentlist/" + item["caseTestId"];
            link[0].setAttribute('href', herf);
            link[0].innerHTML = "Xem danh sách sinh viên thi";
            column.append(link);
            rowHTML.append(column);
            $('#examTimeTable tbody').append(rowHTML);
        });
    }
/*
*Lấy các ca thi của môn thi trong kì thi từ server
*/
    getData(subject) {
        var Data = [];
        var listRoom = [];
        listRoom.push(subject);
        var x = listRoom;
        var y = x;
        $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/admin/exams/examtime',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(listRoom),
            async: false,
            success: function (res) {
                Data = res.data;
            },
            error: function (res) {
                alert("abcd");
            }
        });
        return Data;
    }
/*
*Kiểm tra các trường nhập đúng chưa
*/
    checkInvalid() {
        var check1 = true;
        $('#RoomLocationSpan').empty();
        $('#DateExamSpan').empty();
       
        var check2 = true;
        var selectlocation = $('#selectlocation').val();
        if (selectlocation == "Chọn tất cả") {
            $('#RoomLocationSpan').append("<p> Không được để trống</p>");
            check2 = false;
        }
        var check3 = true;
        var getdatetest = $('#getdateexam').val();
        if (getdatetest == "") {
            $('#DateExamSpan').append("<p>Không được để trống</p>");
            check3 = false;
        }
        if (check1 ==true && check2 ==true && check3 ==true) {
            return true;
        }
        else {
            return false;
        }
    }
/*
*Lấy mã phòng thi
*/
    getRoomId() {
        var id;
        var roomid = $('#getidroom').val();
        var data = $('#getidroom option');
        $.each(data, function (index, item) {

            if (item.value == roomid) {
                id = item.getAttribute('id');
                
            }
        })
        return id;
    }
/*
*Lấy mã môn thi
*/
    getSubjectId() {
        var id;
        var subjectid = $('#getidsubject').val();
        var data = $('#getidsubject option');
        $.each(data, function (index, item) {

            if (item.value == subjectid) {
                id = item.getAttribute('id');
                
            }
        })
        return id;
    }
/*
*Thêm ca thi
*/
    addTestExam() {
        var x = this.checkInvalid();
        if (x == true) {
            var Data = [];
            var roomid = this.getRoomId();
            var subjectid = this.getSubjectId();
            var order;
            var examorder = $('#ExamOrder').val();
            switch (examorder) {
                case "7h-9h":
                    order = 1;
                    break;
                case "9h-11h":
                    order = 2;
                    break;
                case "11h-13h":
                    order = 3;
                    break;
                case "13h-15h":
                    order = 4;
                    break;
                case "15h-17h":
                    order = 5;
                    break;
                case "17h-19h":
                    order = 6;
                    break;
            }
            
            var date = $('#getdateexam').val();
            var istest = $('#istest').checked;
            Data.push(roomid.toString());
            Data.push(subjectid.toString());
            Data.push(order.toString());
            Data.push(date.toString());
            Data.push('true');
            var x = Data;
            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: '/admin/exams/addtestexam',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(Data),
                async: false,
                success: function (res) {
                    alert("Thành công");

                },
                error: function (res) {
                    alert("abcd");
                }
            })
        }
    }
   
}
