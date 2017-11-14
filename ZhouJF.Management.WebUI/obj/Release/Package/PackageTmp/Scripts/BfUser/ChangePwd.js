function pageReady() {
    var form1 = $('#BfUserChangePwd');
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
            OLDPASSWORD: {
                required: "原密码必填"
            },
            NEWPASSWORD: {
                required: "新密码必填"
            },
            AGAINPASSWORD: {
                required: "再次输入新密码必填"
            }
        },
        rules:
        {
            CODE: {
                required: true
            },
            OLDPASSWORD: {
                required: true
            },
            NEWPASSWORD: {
                required: true
            },
            AGAINPASSWORD: {
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
        if ($('#BfUserChangePwd').validate().form()) {
            if ($("#txtNewPassword").val() != $("#txtAgainPassword").val()) {
                bootbox.alert("两次输入的新密码不一致！");
                return false;
            }
            CommitForm();
        }
    });
}
function CommitForm() {
    $.ajax({
        cache: true,
        type: "POST",
        url: "/BfUser/ChangePwd",
        data: $('#BfUserChangePwd').serialize(),
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