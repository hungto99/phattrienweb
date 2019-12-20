
class ExamRoom {
    constructor() {
       
        $('#roomTable tbody').on('click', 'td', TickRow);
        $('#deletebutton').on('click', this.ClickButtonDelete.bind(this));
        this.FilterRoomLocationLoad();
        $('.selectlocation').change(this.GetRoomWithLocation.bind(this));
        $('.getroom').change(this.loadDataSelectBox.bind(this));
        $('#getidroom').change(this.showCountComputerNumber.bind(this));
        $('#addroom').on('click', this.AddRoom.bind(this));
    }
/*
*Hiển thị danh sách phòng khi lấy được
*/
    loadData(location, order) {
        var checkbox = '<td>' +
            '<span class="custom-checkbox">' +
            '<input type="checkbox" id="checkbox1" name="options[]" value="1">' +
            '<label for="checkbox1"></label>' +
            '</span>' +
            '</td >'
        var data = this.getData(location, order);
        var fields = $('#roomTable th[fieldName]');
        $('#roomTable tbody').empty();
        $.each(data, function (index, item) {
            var rowHTML = $('<tr></tr>');
            rowHTML[0].setAttribute('id', item["roomId"]);
            rowHTML.append(checkbox);
            $.each(fields, function (fieldIndex, fieldItem) {
                var fieldName = fieldItem.getAttribute('fieldName');
                var value = item[fieldName];
                rowHTML.append('<td class=' + fieldName + '>' + value + '</td>')
            })
            $('#roomTable tbody').append(rowHTML);
        });
    }
/*
*Lấy tất cả danh sách phòng thi từ server
*/
    getData(location, order) {
        var Data = [];
        var listRoom = [];
        listRoom.push(location);
        listRoom.push(order);
        var x = listRoom;
        var y = x;
        $.ajax({
            method: 'POST',
            dataType: 'json',
            url: '/admin/exams/examroom',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(listRoom),
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
*XÓa phòng thi
*/
    ClickButtonDelete() {
        var me = this;
        var listRoomId = [];
        var listRow = $('.tick');
        $.each(listRow, function (index, item) {
            var refid = item.getAttribute('id');
            listRoomId.push(refid);
        });
        $.ajax({
            method: 'DELETE',
            url: '/admin/exams/examroomid',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(listRoomId),
            success: function (res) {
                alert("da thanh cong");
                me.loadData("0", "0");
            },
            error: function (res) {
                alert("chua co gi  ca");
            }
        });

    }
/*
*Lấy phòng thi
*/
    FilterRoomLocationRequest() {
        var Data = []
        $.ajax({
            method: 'GET',
            dataType: 'json',
            url: '/admin/exams/examlocation',
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
    async FilterRoomLocationLoad() {
        var data = await  this.FilterRoomLocationRequest();
        var selection = $('.selectlocation');
        selection.empty();
        selection.append('<option>Chọn tất cả</option>');
        $.each(data, function (index, item) {
            selection.append('<option class="optionroomlocation">' + item + '</option>')
        });
    }
/*
*Lấy phòng thi có cùng địa điểm thi
*/
    GetRoomWithLocation() {
        var me = this
        var select = $('.selectlocation').val();
        if (select != "Chọn tất cả") {
            this.loadData(select, "0");
        }
        else {
            this.loadData("0", "0");
        }
    }
/*
*Kiểm tra các trường nhập đúng chưa
*/
    CheckInvalidInput() {
        var check1 = true, check2 = true, check3 = true;
        $('#RoomOrderSpan').empty();
        $('#ComputerNumberSpan').empty();
        $('#RoomLocationSpan').empty();
        var roomorder = $('#RoomOrder').val();
        var computernumber = $('#ComputerNumber').val();
        var selectlocation = $('#selectlocation').val();
        if (roomorder == "") {

            $('#RoomOrderSpan').append("<p> Không được để trống</p>");
            check1 = false;
        }
        if (computernumber == "" || computernumber < 0) {
            $('#ComputerNumberSpan').append("<p> Không hợp lệ</p>");
            check2 = false;
        }
        if (selectlocation == "Chọn tất cả") {
            $('#RoomLocationSpan').append("<p> Không được để trống</p>");;
            check3 = false;
        }
        if (check1 == true && check2 == true && check3 == true) {
            return true;
        }
        else {
            return false;
        }
    }
/*
*Hiển thị các phòng thi
*/
    loadDataSelectBox() {
        var select = $('.selectlocation').val();
        if (select != null) {
            var data = this.getData(select, "0");
            $('#getidroom').empty();
            $.each(data, function (index, item) {
                var option = $('<option></option>');
                option[0].setAttribute("id", item["roomId"]);
                option[0].setAttribute("computerNumber", item["computerNumber"]);
                option.append(item['roomOrder']);
                $('#getidroom').append(option);
            });
        }
    }
/*
*Thêm phòng thi
*/
    AddRoom() {
        var me = this;
        var x = this.CheckInvalidInput();
        if (x == true) {
            var Room = [];
            var roomorder = $('#RoomOrder').val();
            var computernumber = $('#ComputerNumber').val();
            var selectlocation = $('#selectlocation').val();
            Room.push(roomorder);
            Room.push(computernumber);
            Room.push(selectlocation);
            $.ajax({
                method: 'POST',
                dataType: 'json',
                url: '/admin/exams/addroom',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(Room),
                async: false,
                success: function (res) {
                    alert(res.message);
                    roomorder.empty();
                    computernumber.empty();
                    me.loadData("0", "0");
                },
                error: function (res) {
                    alert("Error");
                }
            })
        }
    }
/*
*Hiện thị số máy tính/số máy tính tổng
*/
    showCountComputerNumber() {
        var roomid = $('#getidroom').val();
        var data = $('#getidroom option');
        $.each(data, function (index, item) {

                        if (item.value == roomid) {
                            var count = item.getAttribute('computernumber');
                            $('#computercount').empty();
                $('#computercount').append('Số máy tính:' + count)
            }
        })
        
    }
}