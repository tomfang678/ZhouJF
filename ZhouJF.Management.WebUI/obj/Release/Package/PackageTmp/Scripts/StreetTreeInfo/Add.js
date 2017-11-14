function pageReady() {
    var form1 = $('#DataAdd');
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        ignore: "",
        messages: {
            Owner: {
                required: "业主必填"
            },
            Section: {
                required: "标段必填"
            }
        },
        rules:
        {
            Owner: {
                required: true
            },
            Section: {
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
        if ($('#DataAdd').validate().form()) {
            var checkResult = true;
            //唯一性验证
            $.ajax({
                url: "/StreetTreeInfo/CheckCode?CODE=" + $("#txtRoadCode").val(),
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
    $.ajax({
        cache: true,
        type: "POST",
        url: "/StreetTreeInfo/Add",
        data: $('#DataAdd').serialize(),
        async: false,
        error: function (request) {
            bootbox.alert("error");
        },
        success: function (data) {
            if (data.IsSuccess) {
                window.location.href = "/StreetTreeInfo/Index";
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