function pageReady() {

var form1 = $('#BfOrgEdit');
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

    invalidHandler: function(event, validator) {
        success1.hide();
        error1.show();
        Metronic.scrollTo(error1, -200);
    },

    highlight: function(element) {
        $(element).closest('.form-group').addClass('has-error');
    },

    unhighlight: function(element) {
        $(element).closest('.form-group').removeClass('has-error');
    },

    success: function(label) {
        label.closest('.form-group').removeClass('has-error');
    }
});

$("#sure").click(function() {
    if ($('#BfOrgEdit').validate().form()) {
        var r = /^\+?[1-9][0-9]*$/;

        var checkResult = true;
        //唯一性验证
        $.ajax({
            url: "/BfOrg/CheckCode?CODE=" + $("#txtCODE").val()
                + "&ID=" + $("#ID").val(),
            type: "post",
            dataType: "json",
            async: false,
            success: function(data) {
                checkResult = data;
            }
        });
        if (checkResult == false) {
            bootbox.alert("错误信息,该信息已被添加！");
            return false;
        }
        if ($("#txtOwnerOrgId").val() == $("#ID").val())
        {
            bootbox.alert("不能添加当前部门为上级部门！");
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
        url: "/BfOrg/Edit",
        data: $('#BfOrgEdit').serialize(),
    async: false,
    error: function(request) {
        bootbox.alert("error");
    },
    success: function(data) {
        if (data.IsSuccess) {
            window.location.href = "/BfOrg/Index";
        }else {
            if (data.ErrorCode == -1) {
                bootbox.alert("删除出错:TOKEN错误，请重新登录！");
            } else {
                bootbox.alert(data.Message);
            }
        }
    }
});
}