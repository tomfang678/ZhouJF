function pageReady() {
    var grid = $('#grid').dataTableExtention({
        "filterForm": "#searchContatiner",
        "sAjaxSource": "/BridgeInfo/QueryDB",
        "lockLastColumn": true,
        "aoColumns": [{ "mDataProp": "RoadName", "mMaxWidth": "150px", "sWidth": "250px" },
{ "mDataProp": "BridgeName", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeStructureStyle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "InteractionAngle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeSpanCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeSpanStyle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeSquare", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeLength", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeWidth", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "DriveWayWidth", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "PavementWidth", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "MainBeamSize", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "MainBeamCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BearingStyle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BridgeRoadStructure", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "ExpansionStyle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "ExpansionCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "MainBridgeLongitudinalSlope", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "MainBridgeCrossSlope", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "RailLength", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "RailStructure", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "BankRevetmentStyle", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "CappingSize", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "PileFeature", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "WaterSupplyPipeCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "GasPipeCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "ElectricPowerCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "CommsCableCount", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "Images", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "LeaderCode", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "CreateUser", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "CreateTime", "mMaxWidth": "150px", "sWidth": "250px" },
{ "mDataProp": "LastModifiedUser", "mMaxWidth": "100px", "sWidth": "250px" },
{ "mDataProp": "LastModifiedTime", "mMaxWidth": "150px", "sWidth": "250px" },
{ "mDataProp": null, "mMaxWidth": "100px", "sWidth": "250px" }],
        "columnDefs": [
            {
                'orderable': false,
                "targets": -1,
                "data": null,
                "render": function (data, type, row) {
                    var html = [];
                    if ($("#hidIsEdit").val() == "True") {
                        html.push("<a href=\"/ImgInfo/Index?moule=BridgeInfo&moduleId={0}&RoadID={1}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 图片管理 </a>");
                        html.push("<a href=\"/BridgeInfo/Edit/{0}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 编辑 </a>");
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
                'targets': [31, 33],
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
        window.open("/BridgeInfo/ImportTemplate");
    });

    $("#btnImport").click(function () {
        $("#basic").data("actionRrl", "/BridgeInfo/Import");
        $("#basic").data("errorRrl", "/BridgeInfo/GetImportError");
    });

    $("#btnExport").click(function () {
        var str = FormTools.getQueryStringFromForm("#searchContatiner");
        window.open("/BridgeInfo/Export?" + str);
    });
}

function del(el, id) {
    bootbox.confirm("确定删除,删除以后将无法恢复该数据和相关联的数据?", function (result) {
        if (result === true) {
            $.ajax({
                url: "/BridgeInfo/Delete/" + id,
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