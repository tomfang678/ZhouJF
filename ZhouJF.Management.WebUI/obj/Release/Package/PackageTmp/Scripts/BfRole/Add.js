function pageReady() {
    var form1 = $('#BfRoleAdd');
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        ignore: "",
        messages: {
            CODE: {
                required: "角色编码必填"
            },
            NAME: {
                required: "角色名称必填"
            }
        },
        rules:
        {
            CODE: {
                required: true
            },
            NAME: {
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
        if ($('#BfRoleAdd').validate().form()) {
            var checkResult = true;
            //唯一性验证
            $.ajax({
                url: "/BfRole/CheckCode?CODE=" + $("#txtCODE").val(),
                type: "post",
                dataType: "json",
                async: false,
                success: function (data) {
                    checkResult = data;
                }
            });
            if (checkResult == false) {
                bootbox.alert("错误信息,该信息已被添加！");
                return false;
            }
            BindHiddenOperation();
            BindHiddenOtherOperation();
            CommitForm();
        }
    });
}

function CommitForm() {
    $.ajax({
        cache: true,
        type: "POST",
        url: "/BfRole/Add",
        data: $('#BfRoleAdd').serialize(),
        async: false,
        error: function (request) {
            bootbox.alert("error");
        },
        success: function (data) {
            if (data.IsSuccess) {
                window.location.href = "/BfRole/Index";
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

$(document).ready(function () {
    $.ajax({
        type: "get",
        dataType: 'text',
        cache: false,
        url: '/BfRole/GetAllOperation',
        success: function (data) {
            var html = "";
            var resultData = eval(data);
            for (var i = 0; i < resultData.length; i++) {
                html += "<tr id = \"tr-" + resultData[i].ID + "\">";
                html += "<td style=\"text-align:center\"><input type=\"checkbox\" name=\"chkModule\" value=\"" + resultData[i].ID + "\" onChange=\"moduleChkClick(this);\"/></td>";
                html += "<td style=\"vertical-align:middle\">" + resultData[i].NAME + "</td>";
                html += "<td style=\"vertical-align:middle\">";
                //通用权限
                for (var j = 0; j < resultData[i].OperationItems.length; j++) {
                    var operationItem = resultData[i].OperationItems[j];
                    html += operationItem.MODULE_NAME + "<input type=\"checkbox\" name=\"chkOperation\" value=\"" + operationItem.ID + "\" />&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                html += "</td>";
                html += "<td style=\"vertical-align:middle\">";
                //特殊权限
                for (var j = 0; j < resultData[i].OtherOperationItems.length; j++) {
                    var otherOperationItem = resultData[i].OtherOperationItems[j];
                    html += otherOperationItem.NAME + "<input type=\"checkbox\" name=\"chkOtherOperation\" value=\"" + otherOperationItem.ID + "\" />&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                html += "</td>";
                html += "</tr>";
            }
            $("#dtlContent").html(html);
        },
        error: function () {

        }
    });
});


function allChkClick() {
    var flag = $("#allChk").is(":checked");
    $("#dtlContent").find(':checkbox').each(function () {
        if (flag)
            $(this).attr("checked",true);
        else {
            $(this).attr("checked", false);
        }
    });
}

function moduleChkClick(obj) {
    var flag = $(obj).is(":checked");
    $("#tr-"+$(obj).val()).find(':checkbox').each(function () {
        if (flag)
            $(this).attr("checked", true);
        else {
            $(this).attr("checked", false);
        }
    });
}

function BindHiddenOperation() {
    var operationValue = "";
    $("#dtlContent").find("input[name='chkOperation']").each(function () {
        if ($(this).is(":checked")) {
            operationValue += $(this).val() + ",";
        }
    });
    $("#hid_operations").val(operationValue);
}
function BindHiddenOtherOperation() {
    var otherOperationValue = "";
    $("#dtlContent").find("input[name='chkOtherOperation']").each(function () {
        if ($(this).is(":checked")) {
            otherOperationValue += $(this).val() + ",";
        }
    });
    $("#hid_otherOperations").val(otherOperationValue);
}