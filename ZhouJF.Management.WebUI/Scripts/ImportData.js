$("#btnUpload").on("click", function () {
    var url = $("#basic").data("actionRrl");
    var errorUrl = $("#basic").data("errorRrl");
    SubmitExcel(url,errorUrl);
});

function SubmitExcel(url,errorUrl) {
    var filePath = $("#upload").val();
    if (filePath != null && filePath != "") {
        var fileText = filePath.substring(filePath.lastIndexOf("."), filePath.length);
        if (fileText == ".xls") {
            $("#btnCancel").click();
            $("#bkDiv").find('a').trigger('click');
            var obj = {
                url: "",
                dataType: "json",
                success: function (data) {
                    var data = JSON.parse(data);
                    if (data.Success) {
                        $("#btn-ajax-background").click();
                        alert(data.Msg);
                        $('#grid').DataTable().ajax.reload(null, false);
                    } else {
                        $("#btn-ajax-background").click();
                        var word = "导出=>导出所有错误数据<br>取消=>取消导出<br>请选择：";
                        bootbox.dialog({
                            message: word,
                            title: "导出错误数据",
                            buttons: {
                                success: {
                                    label: "导出",
                                    className: "red",
                                    callback: function () {
                                        window.open(errorUrl + "?cacheKey=" + data.ErrorKey);

                                    }
                                },
                                main: {
                                    label: "取消",
                                    className: "blue",
                                    callback: function () {

                                    }
                                }
                            }
                        });
                    }
                },
                error: function (e) {
                    alert(e);
                }
            };
            if (url) {
                obj.url = url;
            }
            $("#uploadFile").form("submit", obj);
        } else {
            alert("请先导入正确的excel！");
            return false;
        }
    } else {
        alert("请上传excel！");
        return false;
    }
}

function setData(actionRrl) {
    $("#basic").data("actionRrl", actionRrl);
}

