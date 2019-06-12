


// Load danh sách quận huyện
$(document).ready(function () {
    var e = document.getElementById('input_city');
    e.addEventListener('change', function () {
        debugger
        var select_city_id = e.options[e.selectedIndex].value;
        $.ajax({
            url: '/GioHang/LoadData_District',
            type: "POST",
            data: { city_ID: select_city_id },
            dataType: "json",
            success: function (res) {
                if (res.status == true) {

                    var html = "<option value=\"\">Chọn Quận/Huyện</option>";
                    debugger
                    if (res.data != "") {
                        var data = JSON.parse(res.data);
                        for (var key in data) {
                            if (data.hasOwnProperty(key)) {
                                html += '<option value="' + data[key].code + '">' + data[key].name_with_type + '</option>';
                            }
                        }
                        $('#input_district').html(html);
                        $('#input_ward').html('<option value="">Chọn Phường/Xã</option>');
                    }
                    else {
                        $('#input_district').html(html);
                        var html1 = "<option value=\"\">Chọn Phường/Xã</option>";
                        $('#input_ward').html(html1);
                    }
                }
            }
        });
    });
});
// Load danh sách phường xã
function loadWard() {
    var e = document.getElementById('input_district');
    var select_district_id = e.value;
    $.ajax({
        url: '/GioHang/LoadData_Ward',
        type: "POST",
        data: { district_id: select_district_id },
        dataType: "json",
        success: function (res) {
            debugger
            if (res.status == true) {

                var html = "<option value=\"\">Chọn Phường/Xã</option>";
                debugger
                if (res.data != "") {
                    var data = JSON.parse(res.data);
                    for (var key in data) {
                        if (data.hasOwnProperty(key)) {
                            html += '<option value="' + data[key].code + '">' + data[key].name_with_type + '</option>';
                        }
                    }
                    $('#input_ward').html(html);
                }
                else {
                    $('#input_ward').html(html);
                }
            }
        }
    });
}

