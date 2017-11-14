function pageReady() {
    var grid = $('#grid').dataTableExtention({
        "filterForm": "#searchContatiner",
        "sAjaxSource": "/BfCode/QueryBfCode",
        "lockLastColumn": true,
        "aoColumns": [
    { "mDataProp": "CODE", "mMaxWidth": "250px", "sWidth": "250px" },
{ "mDataProp": "DESCRIPTION", "mMaxWidth": "250px", "sWidth": "250px" },
{ "mDataProp": "LAST_MODIFIED_DATE" },
{ "mDataProp": null, "mMaxWidth": "150px", "sWidth": "150px" }
],
"columnDefs": [
    {
        'orderable': false,
        "targets": -1,
        "data": null,
        "render": function(data, type, row) {
            var html = [];
            if ($("#hidIsEdit").val()=="True")
                html.push("<a href=\"/BfCode/Edit/{0}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 编辑 </a>");
            else {
                html.push("<label class=\"btn default btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-edit\"></i> 编辑 </label>");
            }
            if ($("#hidIsDelete").val() == "True")
                html.push("<a href=\"javascript:;\" class=\"btn btn-xs green\" onclick=\"del(this,{0})\"><i class=\"fa fa-trash-o\"></i> 删除 </a>");
            else {
                html.push("<label class=\"btn btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-trash-o\"></i> 删除 </label>");
            }
            return html.join('').replace(/\{0\}/g, row.ID);
        }
    },
    {
        'targets': [2],
        "render": function(data, type, row) {
            if (data) {
                return moment(data).format("YYYY-MM-DD HH:mm:ss");
            } else {
                return '';
            }
        }
    }
]
});

$("#btnImportTemplate").click(function() {
    window.open("/BfCode/ImportTemplate");
});

$("#btnImport").click(function() {
    $("#basic").data("actionRrl", "/BfCode/Import");
    $("#basic").data("errorRrl", "/BfCode/GetImportError");
});

$("#btnExport").click(function() {
    var str = FormTools.getQueryStringFromForm("#searchContatiner");
    window.open("/BfCode/Export?" + str);
});
}

function del(el, id) {
    bootbox.confirm("确定删除,删除以后将无法恢复该数据和相关联的数据?", function(result) {
        if (result === true) {
            $.ajax({
                url: "/BfCode/Delete/" + id,
                type: "POST",
            dataType: "json",
            success: function(data) {
                if (data.IsSuccess) {
                    $('#grid').dataTable().fnDraw();
                } else {
                    if (data.ErrorCode == -1) {
                        bootbox.alert("删除出错:TOKEN错误，请重新登录！");
                    } else {
                        bootbox.alert("删除出错:" + JSON.stringify(data.Message));
                    }
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                bootbox.alert("error:" + this.url);
            }
        });
    }
    });
}

//清除
function resetDtl() {
    $("table tbody").empty();
}

function selectCls(a) {
    $("#txtBoxSpecId").val($(a).attr("boxSpecClsId"));
    $("#txtBoxSpecName").val($(a).parent().parent().children().eq(0).text());
    $("#btnClose").click();
}