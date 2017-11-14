$(function () {
    InitEvent();
});

function pageReady() {
var form1 = $('#BfUserAdd');
var error1 = $('.alert-danger', form1);
var success1 = $('.alert-success', form1);

form1.validate({
    errorElement: 'span',
    errorClass: 'help-block help-block-error',
    focusInvalid: false,
    ignore: "",
    messages: {
        CODE: {
            required: "部门编码必填"
        },
        NAME: {
            required: "部门名称必填"
        },
        OWNER_ID: {
            required: "所属部门必填"
        },
        EXPIRE_TIME: {
            required: "有效时间必填"
        }
    },
    rules:
    {
        CODE: {
            required: true
        },
        NAME: {
            required: true
        },
        OWNER_ID: {
            required: true
        },
        EXPIRE_TIME: {
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
    if ($('#BfUserAdd').validate().form()) {
        var checkResult = true;
        //唯一性验证
        $.ajax({
            url: "/BfUser/CheckCode?CODE=" + $("#txtCODE").val(),
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
        CommitForm();
    }
});
}

function CommitForm() {
    BindHiddenRole();
    $.ajax({
        cache: true,
        type: "POST",
        url: "/BfUser/Add",
        data: $('#BfUserAdd').serialize(),
    async: false,
    error: function (request) {
        bootbox.alert("error");
    },
    success: function (data) {
        if (data.IsSuccess) {
            window.location.href = "/BfUser/Index";
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

function BindHiddenRole() {
    var roleValue = "";
    $("#userRoleChkDiv").find(':checkbox').each(function () {
        if ($(this).is(":checked")) {
            roleValue += $(this).val() + ",";
        }
    });
    $("#hid_roles").val(roleValue);
}

function InitEvent() {
    BindRoleByData();
}

function BindRoleByData() {
    $.ajax({
        url: "/BfRole/GetAllRole",
        type: "get",
        dataType: "json",
        success: function (resultData) {
            if (resultData != null && resultData != "") {
                var htmlUserRoleChk = "";
                for (var i = 0; i < resultData.length; i++) {
                    htmlUserRoleChk += resultData[i].NAME + "<input type=\"checkbox\" id=\"chk_role_" + resultData[i].ID + "\" value=\"" + resultData[i].ID + "\"/>&nbsp;&nbsp;&nbsp;";
                }
                $("#userRoleChkDiv").html(htmlUserRoleChk);
            }
        }
    });
}