
$("#btnDtlAdd").click(function () {
    AddRow();
});

var addRowIndex = -1;
function AddRow() {
    var $table = $('#dtlTable');
    var html = "";
    html += "<tr id = \"tr-" + addRowIndex + "\">";
    html += "<input type='hidden' name='hiddenOldDtl' value='0'>";
    html += "<td><input type=\"text\" name=\"PdCode\" class=\"form-control input-medium\" value=\"\"></td>";
    html += "<td><input type=\"text\" name=\"PdName\" class=\"form-control input-medium\" value=\"\"></td>";
    html += "<td><input type=\"text\" name=\"SortNo\" class=\"form-control input-small SortNo\" value=\"\" onkeyup=\"this.value=this.value.replace(/\D/g,'')\"></td>";
    html += "<td><a class='delete' href='javascript:deleteRow(" + addRowIndex + ");'>删除</a></td></tr>";
    $table.append(html);
    addRowIndex--;
}
$(".SortNo").live("keyup", function () {  //keyup事件处理 
    $(this).val($(this).val().replace(/\D|^0/g, ''));
}).bind("paste", function () {  //CTR+V事件处理 
    $(this).val($(this).val().replace(/\D|^0/g, ''));
}).css("ime-mode", "disabled"); //CSS设置输入法不可用
function deleteRow(id) {
    if (id < 0) {
        $("#tr-" + id).remove();
    } else {
        bootbox.confirm("确定删除？删除后将无法恢复。", function (result) {
            if (result === true) {
                $.ajax({
                    type: "GET",
                    data: "id=" + id,
                    dataType: 'text',
                    cache: false,
                    url: '/SanitationInfo/DeleteDtlById',
                    success: function (data) {
                        $("#tr-" + id).remove();
                    },
                    error: function () {
                        alert("删除失败！");
                    }
                });
            }
        });
    }
}



function pageReady() {
    var form1 = $('#BfCodeEdit');
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        ignore: "",
        messages: {
            RoadID: {
                required: "道路编码必填"
            },
            LeaderCode: {
                required: "带班编码必填"
            }
        },
        rules:
        {
            RoadID: {
                required: true
            },
            LeaderCode: {
                required: true
            }
        },

        invalidHandler: function (event, validator) {
            success1.hide();
            error1.show();
            Metronic.scrollTo(error1, -200);
        },

        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },

        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },

        success: function (label) {
            label.closest('.form-group').removeClass('has-error');
        }
    });

    $("#sure").click(function () {
        if ($('#formEdit').validate().form()) {
            var checkResult = true;
            //唯一性验证
            //$.ajax({
            //    url: "/SanitationInfo/CheckCode?CODE=" + $("#txtSubContractorCode").val(),
            //    type: "post",
            //    dataType: "json",
            //    async: false,
            //    success: function (data) {
            //        checkResult = data;
            //    }
            //});
            //if (checkResult == false) {
            //    bootbox.alert("错误信息,该信息已被添加！");
            //    return false;
            //}
            CommitForm();
        }
    });
}

function CommitForm() {
    $.ajax({
        cache: true,
        type: "POST",
        url: "/SanitationInfo/Edit",
        data: $('#formEdit').serialize(),
        async: false,
        error: function (request) {
            bootbox.alert("error");
        },
        success: function (data) {
            if (data.IsSuccess) {
                window.location.href = "/SanitationInfo/Index";
            } else {
                if (data.ErrorCode == -1) {
                    bootbox.alert("删除出错:TOKEN错误，请重新登录！");
                } else {
                    bootbox.alert(data.Message);
                }
            }
        }
    });
}

//$(document).ready(function () {
//    $.ajax({
//        type: "post",
//        data: "mainId=" + $("#Id").val(),
//        dataType: 'text',
//        cache: false,
//        url: '/SubContractorInfo/GetBfCodeDtl',
//        success: function (data) {
//            $("#dtlContent").html(data);
//        },
//        error: function () {

//        }
//    });
//});