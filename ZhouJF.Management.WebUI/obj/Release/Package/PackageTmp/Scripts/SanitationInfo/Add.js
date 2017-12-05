function pageReady() {
    var form1 = $('#DataAdd');
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    $("#sure").click(function () {
        if ($('#DataAdd').validate().form()) {
            var checkResult = true;
            //唯一性验证
            //$.ajax({
            //    url: "/SanitationInfo/CheckCode?CODE=" + $("#txtRoadCode").val(),
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
        url: "/SanitationInfo/Add",
        data: $('#DataAdd').serialize(),
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