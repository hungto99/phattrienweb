$(document).ready(function () {
    var x = new Class();
})
class Class {
    constructor() {
        this.loadData();
        $('#classTable tbody').on('click', 'td', TickRow);
        $('#addbyexcel').on('click', this.AddListStudent.bind(this));
    }
/*
*Hiện thị về dữ liệu của lớp
*/
    loadData() {
        var stt = 1;
        var data = this.getData();
        $('#classTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr></tr>');
            rowHTML.append('<td>' + stt + '</td>');
            stt++;
            rowHTML.append('<td>' + item + '</td>');
            var column = $('<td></td>');
            var link = $('<a></a>');
            var herf = "https://localhost:44302/admin/students/getlistclass/" + item;
            link[0].setAttribute('href', herf);
            link[0].innerHTML = "Xem danh sách sinh viên";
            column.append(link);
            rowHTML.append(column);
            $('#classTable tbody').append(rowHTML);

        });
    }
/*
*Lấy dữ liệu của danh sách lớp từ server
*/
    getData() {
        var Data = [];
        $.ajax({
            method: 'GET',
            dataType: 'json',
            url: '/admin/students/class',
            async: false,
            success: function (res) {
                Data = res.data;
            },
            error: function (res) {
                alert("abcd");
            }
        })
        return Data;
    }
/*
*Lấy danh sách tất cả học sinh
*/
    getListStudent(subjectid) {
        var id = $(this).getAttribute("id");

    }
/*
*Kiểm tra file upload có hợp lệ không
*/
    checkInvalid() {
        var check1 = true, check2 = true;
        var ListStudent = $('#jsonObject').text();
        var file = $('#fileUploader')[0].files.length;
        var ClassName = $('#className').val();
        $('#FileSpan').empty();
        $('#ClassSpan').empty();
        if (file == 0) {
            check1 = false;
            $('#FileSpan').append("<p>Chưa có file</p>");
        }
        if (ClassName == '') {
            check2 = false;
            $('#ClassSpan').append("<p>Chưa có tên lớp</p>");
        }
        if (check1 && check2) {
            return true;
        }
        else {
            return false;
        }
    }
/*
*Thêm danh sách sinh viên từ excel
*/
    AddListStudent() {
        var me = this;
        var x = this.checkInvalid();
        if (x == true) {
            var Data = {};
            var ListStudent1 = $('#jsonObject').text();
            ListStudent1 = JSON.parse(ListStudent1);
            var ListStudent = [];
            
            for (var i = 0; i < ListStudent1.length; i++) {
                var Student = {
                    "studentId": ListStudent1[i]["Mã Học Sinh"], "studentName": ListStudent1[i]["Tên Học Sinh"],
                    "sex": ListStudent1[i]["Giới Tính"], "birthDay": ListStudent1[i]["Ngày Sinh"], "bornPlace": ListStudent1[i]["Nơi sinh"]
                }
                ListStudent.push(Student);
            }
            var ClassName = $('#className').val();
            Data = { "students": ListStudent, "className": ClassName };
            Data = JSON.stringify(Data);
            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: '/admin/students/addclass',
                contentType: "application/json; charset=utf-8",
                data: Data,
                async: false,
                success: function (res) {
                    alert(res.message);
                    me.loadData();
                },
                error: function (res) {
                    alert("Không kết nối được");
                }
            })
        }
    }
}
/*
 *Hàm lấy một file excel từ trong máy tính
 */
$("#fileUploader").change(function (evt) {
    var selectedFile = evt.target.files[0];
    var reader = new FileReader();
    reader.onload = function (event) {
        var data = event.target.result;
        var workbook = XLSX.read(data, {
            type: 'binary'
        });
        workbook.SheetNames.forEach(function (sheetName) {

            var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
            var json_object = JSON.stringify(XL_row_object);
            document.getElementById("jsonObject").innerHTML = json_object;
        })
    };
    reader.onerror = function (event) {
        console.error("File could not be read! Code " + event.target.error.code);
    };
    reader.readAsBinaryString(selectedFile);
});
function renameKeys(obj, newKeys) {
    const keyValues = Object.keys(obj).map(key => {
        const newKey = newKeys[key] || key;
        return { [newKey]: obj[key] };
    });
    return Object.assign({}, ...keyValues);
}