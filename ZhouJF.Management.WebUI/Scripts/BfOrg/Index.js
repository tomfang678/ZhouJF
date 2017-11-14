function pageReady() {
    var grid = $('#grid').dataTableExtention({
        "filterForm": "#searchContatiner",
        "sAjaxSource": "/BfOrg/QueryBfOrg",
        "lockLastColumn": true,
        "aoColumns": [
    { "mDataProp": "CODE", "mMaxWidth": "250px", "sWidth": "250px" },
{ "mDataProp": "NAME", "mMaxWidth": "250px", "sWidth": "250px" },
{ "mDataProp": "OWNER_NAME", "mMaxWidth": "250px", "sWidth": "250px" },
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
                            if ($("#hidIsEdit").val() == "True") {
                                html.push("<a href=\"/BfOrg/Edit/{0}\" class=\"btn default btn-xs green\"><i class=\"fa fa-edit\"></i> 编辑 </a>");
                            } else {
                                html.push("<label class=\"btn default btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-edit\"></i> 编辑 </label>");
                            }
                            if ($("#hidIsDelete").val() == "True") {
                                html.push("<a href=\"javascript:;\" class=\"btn btn-xs green\" onclick=\"del(this,{0})\"><i class=\"fa fa-trash-o\"></i> 删除 </a>");
                            } else {
                                html.push("<label class=\"btn btn-xs green\" style=\"cursor:default\"><i class=\"fa fa-trash-o\"></i> 删除 </label>");
                            }
                            
                            return html.join('').replace(/\{0\}/g, row.ID);
                        }
                    },
                    {
                        'targets': [3],
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
}

function del(el, id) {
    bootbox.confirm("确定删除,删除以后将无法恢复该数据和相关联的数据?", function(result) {
        if (result === true) {
            $.ajax({
                url: "/BfOrg/Delete/" + id,
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

$("input.BfOrgSelector").select2({
    placeholder: "请选择上级部门...",
    allowClear: true,
    minimumInputLength: 1,
    ajax: {
        url: "/PlugIn/_BfOrgSelector",
    dataType: 'json',
    quietMillis: 100,
    data: function(term, page) {
        return { limit: 10, key: term };
    },
    results: function(data, page) {
        return { results: data }
    }
},
    formatResult: function(d) {
        return d.name;
    },
formatSelection: function(d) {
    return d.name;;
},
initSelection: function(element, callback) {

}
});