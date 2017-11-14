function pageReady() {
    var form1 = $('#BfCodeAdd');
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        errorElement: 'span',
        errorClass: 'help-block help-block-error',
        focusInvalid: false,
        ignore: "",
        messages: {
            CODE: {
                required: "编码必填"
            },
            DESCRIPTION: {
                required: "描述必填"
            }
        },
        rules:
        {
            CODE: {
                required: true
            },
            DESCRIPTION: {
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
        if ($('#BfCodeAdd').validate().form()) {
            var checkResult = true;
            //唯一性验证
            $.ajax({
                url: "/BfCode/CheckCode?CODE=" + $("#txtCODE").val(),
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

            var PdCodes = [];
            var PdNames = [];
            var SortNos = [];
            var error = false;
            var repeat = false;
            $("input[name=PdCode]").each(function (a, b) {
                var pdCode = $(this).val();
                if (pdCode == "") {
                    error = true;
                    return false;
                } else if (PdCodes.indexOf(pdCode) >= 0) {
                    repeat = true;
                    return false;
                } else
                    PdCodes.push(pdCode);

            });
            if (error)
                alert("明细编号不能为空");
            if (repeat)
                alert("明细编号重复");
            else {
                $("input[name=PdName]").each(function (a, b) {
                    var pdValue = $(this).val();
                    PdNames.push(pdValue);
                });
                $("input[name=SortNo]").each(function (a, b) {
                    var sortNo = $(this).val();
                    if ($.trim(sortNo) == '')
                        sortNo = 0;
                    SortNos.push(sortNo);
                });
                $("#SortNos").val(SortNos);
                $("#PdCodes").val(PdCodes);
                $("#PdNames").val(PdNames);
                CommitForm();
            }
                    
        }
    });
}

function CommitForm() {
    $.ajax({
        cache: true,
        type: "POST",
        url: "/BfCode/Add",
        data: $('#BfCodeAdd').serialize(),
    async: false,
    error: function(request) {
        bootbox.alert("error");
    },
    success: function(data) {
        if (data.IsSuccess) {
            window.location.href = "/BfCode/Index";
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