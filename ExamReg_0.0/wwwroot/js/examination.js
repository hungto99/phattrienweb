
class Examination {
        constructor() {
        $('#subjectTable tbody').on('click', 'td', TickRow);
        $('#deletebutton').on('click', this.ClickButtonDelete.bind(this));
        this.FilterExamPeriodLoad();
        $('.getperiod').change(this.GetSubjectWithPeriod.bind(this));
        this.loadDataSelectBox();
        $('#addsubject').on('click', this.AddSubject.bind(this));
        $('#addListSubjectStudent').on('click', this.AddListStudentSubject.bind(this));
    }
/*
* Hiển thị các môn thi của kì thi
*/
    loadData(begin, end, name) {
        var checkbox = '<td>' +
            '<span class="custom-checkbox">' +
            '<input type="checkbox" id="checkbox" name="options[]" value="1">' +
            '<label></label>' +
            '</span>' +
            '</td >';
        var data = this.getData(begin, end, name);
        var fields = $('#subjectTable th[fieldName]');
        $('#subjectTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr></tr>');
            rowHTML[0].setAttribute('id', item["subjectId"]);
            rowHTML.append(checkbox);
            $.each(fields, function (fieldIndex, fieldItem) {
                var fieldName = fieldItem.getAttribute('fieldName');
                var value = item[fieldName];
                rowHTML.append('<td class=' + fieldName + '>' + value + '</td>');
                
            });
            var column = $('<td></td>');
            var link = $('<a></a>');
            var herf = "https://localhost:44302/admin/courses/getlistclass/" + item["subjectId"];
            link[0].setAttribute('href', herf);
            link[0].innerHTML = "Xem danh sách sinh viên";
            column.append(link);
            rowHTML.append(column);
            $('#subjectTable tbody').append(rowHTML);
        });
        
    }
/*
*Lấy mã của môn thi trong kì thi
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
*Lấy ra tất cả môn thi của kì thi đã chọn
*/
    loadDataSelectBox(){
        
            var data = this.getData("0", "0", "0");
            $('#getidsubject').empty();
         $.each(data, function (index, item) {
            var option = $('<option></option>');
            option[0].setAttribute("id", item["subjectId"]);
            var selectsubject = $('#getidsubject');
            option.append(item['subjectName']);
            $('#getidsubject').append(option);
        });
    }
/*
*Lấy danh sách môn thi của kì thi từ server
*/
    getData(begin, end, name) {
        var Data = [];
        var listExamination = [];
        listExamination.push(begin);
        listExamination.push(end);
        listExamination.push(name);
        $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/admin/courses/getsubject', 
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(listExamination),
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
*Lấy danh sách học sinh của môn thi từ server
*/
    getListStudent(subjectid) {
        var id = $(this).getAttribute("id");

    }
/*
*Lấy phòng thi
*/
    FilterRoomLocationRequest() {
        var Data = []
        $.ajax({
            method: 'GET',
            dataType: 'json',
            url: '/admin/courses/examperiod',
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
    async FilterExamPeriodLoad() {
        var data = await this.FilterRoomLocationRequest();
        var selection = $('.selectexamperiod');
        selection.empty();
        selection.append('<option>Chọn tất cả</option>')
        $.each(data, function (index, item) {
            selection.append('<option class="optionexamperiod"' + "extbegin=" + item["examPeriodBegin"] +" extend=" + item["examPeriodEnd"] + ">" + item["examPeriodBegin"] + "-" + item["examPeriodEnd"]+ '</option>')
        })
    }
/*
*Lấy từ server danh sách môn cùng kì thi
*/
    GetSubjectWithPeriod() {
        var me = this;
        var select = $('.selectexamperiod').val();
        if (select == "Chọn tất cả") {
            this.loadData("0","0", "0");
        }
        else {
            var begin = select.substring(0, 4);
            var end = select.substring(5, 9);
            this.loadData(begin,end, "0");
        }
    }
/*
*Xóa một môn thi
*/
    ClickButtonDelete() {
        var me = this;
        var listSubjectId = [];
        var listSubject = $('.tick');
        $.each(listSubject, function (index, item) {
            var refid = item.getAttribute('id');
            listSubjectId.push(refid);
        });
        $.ajax({
            method: 'DELETE',
            dataType: "json",
            url: '/admin/courses/subjectid',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(listSubjectId),
            async: false,
            success: function (res) {
                alert("da thanh cong");
                me.loadData("0", "0", "0");
            },
            error: function (res) {
                alert("chua co gi  ca");
            }
        });
        $.each(listSubject, function (index, item) {
            var refid2 = item.getAttribute('id');
            listSubjectId.push(refid2);
        });
    }
/*
*Kiểm tra các trường nhập có hợp lệ không
*/
    CheckInvalidInput() {
        var check1 = true;
        $('#ExamPeriodSpan').empty();
        var examperiod = $('#selectexamperiod').val();
        if (examperiod == "Chọn tất cả") {
            $('#ExamPeriodSpan').append("<p> Không được để trống</p>");;
            check1 = false;
        }
        return check1;
    }
/*
*Kiểm tra các trường nhập có hợp lệ không
*/
    checkInvalid() {
        var check1 = true, check2 = true;
        var ListStudent = $('#jsonObject').text();
        var file = $('#fileUploader')[0].files.length;
        $('#FileSpan').empty();
        $('#SubjectSpan').empty();
        if (file == 0) {
            check1 = false;
            $('#FileSpan').append("<p>Chưa có file</p>");
        }
        var examperiod = $('#getexamperiod').val();
        if (examperiod == "Chọn tất cả") {
            $('#SubjectSpan').append("<p> Không được để trống</p>");
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
*Thêm một môn thi trong kì thi
*/
    AddSubject() {
        var me = this;
        var x = true;
        if (x == true) {
            var Subject = [];
            var subjectname = $('.subjectname').val();
            //var select = $('#selectexamperiod').val();
            Subject.push(subjectname);
            $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/admin/courses/addsubject', 
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(Subject),
            async: false,
            success: function (res) {
                if (res.message == "1") {
                    alert("Đã có môn học này");
                }
                if (res.message == "2") {
                    alert("Không có tên môn học này này");
                }
                if (res.message == "4") {
                    alert("Thêm thành công");
                    me.loadData("0", "0", "0");
                }
                if (res.message == "13") {
                    alert("Không có kì học này");
                }
            },
            error: function (res) {
                alert("abcd");
            }
        })
        }
    }
/*
*Thêm danh sách học sinh của môn thi
*/
    AddListStudentSubject() {
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
                    "studentClass": ListStudent1[i]["Lớp"]
              
                }
                ListStudent.push(Student);
            }
            var subjectId = parseInt(this.getSubjectId());
            var testAble = $('#testAble')[0].checked;
            Data = { "studentId": ListStudent, "testAble": testAble, "subjectId": subjectId };
            Data = JSON.stringify(Data);
            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: '/admin/courses/addliststudent',
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