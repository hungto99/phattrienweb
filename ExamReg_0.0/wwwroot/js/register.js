
$(document).ready(function () {
    var x = new Register();
    
})
/*
 * 
 */
class Register {
    constructor() {
        $('#getalltestcaseid').on('click', this.getAllDataInTable.bind(this));
        checkCanJoin();
        $('#print').on('click', PrintElem);
        $('#tableResult tbody').on('click', '#btn_delete', function () {
            var didConfirm = confirm("Bạn có chắc muốn xóa môn thi này?");
            if (didConfirm == true) {
                this.closest('tr').remove();
                $('#tableResult tbody tr').each(function (index) {
                    $(this).find('span.sn').html(index + 1);
                    
                });
                checkCanJoin();
                return true;

            } else {
                return false;
            }
        });
        $("#registerTable tbody").on('click', 'tr', function () {
            var join = $(this)[0].getAttribute('join');
            if (join == "1") {
                var tds = $(this).children();
                var fieldsRegister = $('#registerTable th[fieldName]');
                var data = {};
                $.each(fieldsRegister, function (index, field) {

                    var value = field.getAttribute('fieldName');

                    data[value] = $(tds[index]).text();
                });

                var fieldsResult = $('#tableResult th[fieldName]');
                var row = $('<tr></tr>');
                row[0].setAttribute('roomId', $(this)[0].getAttribute('roomId'));
                row[0].setAttribute('subjectId', $(this)[0].getAttribute('subjectId'));
                row[0].setAttribute('testCaseId', $(this)[0].getAttribute('testCaseId'));
                row[0].setAttribute('examOrder', $(this)[0].getAttribute('examOrder'));
                row[0].setAttribute('subjectName', $(this)[0].getAttribute('subjectName'));
                row[0].setAttribute('date', $(this)[0].getAttribute('date'));
                $.each(fieldsResult, function (index, field) {
                    var valueOfField = field.getAttribute('fieldName');
                    var valueText = data[valueOfField];
                    if (valueOfField !== "cancel" && valueOfField !== "") {
                        row.append('<td>' + valueText + '</td>');
                    }
                    else if (valueOfField === "cancel") {
                        row.append("<td><i class ='fa fa-trash' id='btn_delete'></i></td>");
                    }
                    else {
                        var stt = $('#tableResult > tbody > tr').length + 1;
                        row.append("<td><span class='sn'>" + stt + '</span></td>');
                    }
                });
                $('#tableResult tbody').append(row);
                checkCanJoin();
            }
        });
    }

/*
* Hiện thị tất các ca thi của sinh viên
*/
    getAllDataInTable() {
        var Data = [];
        var trtag = $('#resultofJoin tr');
        $.each(trtag, function (index, item) {
            var ExamCheck = {
                "subjectId": parseInt(item.getAttribute('subjectId')),
                "subjectName": item.getAttribute('subjectName').toString(),
                "caseTestId": parseInt(item.getAttribute('testCaseId')),
                "time": item.getAttribute('date').toString(),
                "examOrder": parseInt(item.getAttribute('examOrder'))
            }
            Data.push(ExamCheck);

        }
        );
        var x = Data;
        $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/student/home/getjoinexam',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Data),
            async: false,
            success: function (res) {
                alert(res.message);
  
            },
            error: function (res) {
                alert("abcd");
            }
        });
    }
    
    
}

/*
 *Kiểm tra xem có thể đăng ký vào được không
 */
function checkCanJoin() {
    var listsresult = $("#resultofJoin").children();
    var listsubject = $('#listSubjectToJoin').children();
    $.each(listsresult, function (index, item) {
        $.each(listsubject, function (index2, item2) {
            var name1 = item.getAttribute('subjectName');
            var name2 = item2.getAttribute('subjectName');
            if (name1 == name2) {
                item2.setAttribute('join', '0');
            }
            var date1 = item.getAttribute('date');
            var date2 = item2.getAttribute('date');
            var order1 = item.getAttribute('examOrder');
            var order2 = item2.getAttribute('examOrder');
            if (date1 == date2 && order1 == order2) {
                item2.setAttribute('join', '0');
            }
        });
    });
    $.each(listsubject, function (index, item) {
        var count = item.getAttribute('computercount');
        var number = item.getAttribute('computernumber');
        if (count >= number) {
            item.setAttribute('join', '0');1
        }
        var check = true, check2 = true;
        $.each(listsresult, function (index2, item2) {
            var id1 = item.getAttribute('subjectId');
            var id2 = item2.getAttribute('subjectId');
            if (id1 == id2) {
                check = false;
            }
            var date1 = item.getAttribute('date');
            var date2 = item2.getAttribute('date');
            var order1 = item.getAttribute('examOrder');
            var order2 = item2.getAttribute('examOrder');
            if (date1 == date2 && order1 == order2) {
                check2 = false;
            }
        });
        if (check == true && check2 == true) {
            item.setAttribute('join', '1');
        }
    });
    var listsubject2 = $('#listSubjectToJoin').children();
    $.each(listsubject2, function (index, item) {
        var join = item.getAttribute('join');
        if (join == "0") {
            item.style.backgroundColor = "yellow";
        }
        else {
            item.style.backgroundColor = "white";
        }
    });
}
/*
 *In kết quả thi
 */
function Print() {
    window.print();
    window.close();
}
function PrintElem() {
    var mywindow = window.open('', 'PRINT', 'height=400,width=600');
    var x = $('.myDivToPrint')[0];
    var Data = GetStudentInfo();
    var div1 = '<div style="display:flex">'+'<div style="position:relative; top:0px; left:0px">' +
        '<p> Họ Và Tên: ' + Data["studentName"] + '</p>' +
        '<p> Mã Số Sinh Viên: ' + Data["studentId"] + '</p>' +
        '<p> Tên Lớp: ' + Data["studentClass"] + '</p>' +
        '</div>' +
        '<div style="position:relative; top:0px; left:300px">' +
        '<p> Giới Tính: ' + Data["sex"] + '</p>' +
        '<p> Nơi Sinh: ' + Data["birthDay"] + '</p>' +
        '<p> Ngày Sinh: ' + Data["bornPlace"] + '</p>' +
        
        '</div>' +
        '</div>'
    var y = $('#ExamPeriodInfo #ExamsPeriod')[0].innerHTML;
    mywindow.document.write('<html><head><title>' + document.title + '</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<style> table { border - collapse: collapse;} table, th, td {border: 1px solid black;} td:last-child,th:last-child{display: none;}</style>')
    mywindow.document.write('<h1>' + "Phiếu dự thi" + '</h1>');
    mywindow.document.write('<h2> Kì Thi: ' + y + '</h2>');

    mywindow.document.write(div1);
    //mywindow.document.write("<p> Họ và Tên:" + Data["studentName"]+"Mã Số Sinh Viên:" + Data["studentId"] +"</p>");
    //mywindow.document.write("<span> Ngày Sinh:" + Data["bornPlace"] + "</span>");
    //mywindow.document.write("<p> Lớp:" + Data["studentClass"] + "</p>");

    mywindow.document.write('<div>'+x.outerHTML+'</div>');
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}
/*
 *Lấy thông tin sinh viên đã đắng ky
 */
function GetStudentInfo() {
    var Data;
    $.ajax({
        method: 'GET',
        dataType: 'json',
        url: '/student/home/getstudentinfo',
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