function pageReady() {
    var grid = $('#grid').dataTableExtention({
        "filterForm": "#searchContatiner",
        "sAjaxSource": "/SewerInfo/QueryDB",
        "lockLastColumn": true,
        "aoColumns": [{ "mDataProp": "RoadName", "mMaxWidth": "150px", "sWidth": "250px" },
            { "mDataProp": "RoadBetween", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "ConnectingPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "SamllRainPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "MiddleRainPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "BigRainPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "BiggerRainPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "RainPipeDeepth", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "DirtyWaterPipeLength", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "DirtyWaterPipeDeepth", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "RainInspectionShaftCount", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "DiryWaternspectionShaftCount", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "WaterInletCount", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "LeaderCode", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "CreateUser", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "CreateTime", "mMaxWidth": "150px", "sWidth": "250px" },
{ "mDataProp": "LastModifiedUser", "mMaxWidth": "110px", "sWidth": "250px" },
{ "mDataProp": "LastModifiedTime", "mMaxWidth": "150px", "sWidth": "250px" },
{ "mDataProp": null, "mMaxWidth": "110px", "sWidth": "250px" }],
        "columnDefs": [
            {
                'orderable': false,
                "targets": -1,
                "data": null,
                "render": function (data, type, row) {
                    var html = [];
                    if ($("#hidIsEdit").val() == "True") {
                        html.push("<a href=\"/ImgInfo/Index?moule=SewerInfo&moduleId={0}&RoadID={1}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 图片管理 </a>");
                        html.push("<a href=\"/SewerInfo/Edit/{0}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 编辑 </a>");
                    }
                    else {
                        html.push("<label class=\"btn default btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-edit\"></i> 编辑 </label>");
                    }
                    if ($("#hidIsDelete").val() == "True")
                        html.push("<a href=\"javascript:;\" class=\"btn btn-xs green\" onclick=\"del(this,{0})\"><i class=\"fa fa-trash-o\"></i> 删除 </a>");
                    else {
                        html.push("<label class=\"btn btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-trash-o\"></i> 删除 </label>");
                    }
                    return html.join('').replace(/\{0\}/g, row.ID).replace(/\{1\}/g, row.RoadID);
                }
            },
            {
                'targets': [15, 17],
                "render": function (data, type, row) {
                    if (data) {
                        return moment(data).format("YYYY-MM-DD HH:mm:ss");
                    } else {
                        return '';
                    }
                }
            }
        ]
    });

    $("#btnImportTemplate").click(function () {
        window.open("/SewerInfo/ImportTemplate");
    });

    $("#btnImport").click(function () {
        $("#basic").data("actionRrl", "/SewerInfo/Import");
        $("#basic").data("errorRrl", "/SewerInfo/GetImportError");
    });

    $("#btnExport").click(function () {
        var str = FormTools.getQueryStringFromForm("#searchContatiner");
        window.open("/SewerInfo/Export?" + str);
    });
}

function del(el, id) {
    bootbox.confirm("确定删除,删除以后将无法恢复该数据和相关联的数据?", function (result) {
        if (result === true) {
            $.ajax({
                url: "/SewerInfo/Delete/" + id,
                type: "POST",
                dataType: "json",
                success: function (data) {
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
                error: function (XMLHttpRequest, textStatus, errorThrown) {
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