
$(function () {
    InitEvent();
    Query();
});

$("#btnDtlAdd").click(function () {
    AddRow();
});

var addRowIndex = -1;
function AddRow() {
    var $table = $('#dtlTable');
    var html = "";
    html += "<tr id = \"tr-" + addRowIndex + "\">";
    html += "<input type='hidden' name='hiddenOldDtl' value='0'>";
    html += "<td><input type=\"text\" name=\"OtherCode\" class=\"form-control input-medium\" value=\"\"></td>";
    html += "<td><input type=\"text\" name=\"OtherName\" class=\"form-control input-medium\" value=\"\"></td>";
    html += "<td><a class='delete' href='javascript:deleteRow(" + addRowIndex + ");'>删除</a></td></tr>";
    $table.append(html);
    addRowIndex--;
}

function deleteRow(id) {
    if (id < 0) {
        $("#tr-" + id).remove();
    } else {
        bootbox.confirm("确定删除？删除后将无法恢复。", function (result) {
            if (result === true) {
                $.ajax({
                    type: "GET",
                    data: "id=" + id,
                    dataType: 'text',
                    cache: false,
                    url: '/BfModule/DeleteOtherOperationById',
                    success: function (data) {
                        $("#tr-" + id).remove();
                    },
                    error: function () {
                        alert("删除失败！");
                    }
                });
            }
        });
    }
}

function InitEvent() {
    $('#bf_module_tree').on("changed.jstree", function (e, data) {
        if (data.selected != "") {
            $.ajax({
                url: "/BfModule/GetBfModuleById?id=" + data.node.id + "",
                type: "post",
                success: function (newData) {
                    eval("newData=" + newData);
                    if (newData != null && newData != "") {
                        if (data.node.parent == "#") {
                            document.getElementById("parent").innerHTML = "";
                        } else {
                            document.getElementById("parent").innerHTML = data.instance.get_node(data.node.parent).text;
                        }
                        document.getElementById("ID").value = newData.ID;
                        document.getElementById("PARENT_ID").value = newData.PARENT_ID;
                        document.getElementById("CODE").value = newData.CODE;
                        document.getElementById("NAME").value = newData.NAME;
                        document.getElementById("REMARK").value = newData.REMARK == null ? "" : newData.REMARK;
                        document.getElementById("CREATE_USER").value = newData.CREATE_USER;
                        document.getElementById("CREATE_DATE").value = newData.CREATE_DATE;
                        document.getElementById("URL").value = newData.URL;
                        document.getElementById("ORDER").value = newData.ORDER;
                        document.getElementById("ICON").value = newData.ICON == null ? "" : newData.ICON;
                        if (newData.IsNeedControl) {
                            $.uniform.update($("#IsNeedControl").attr("checked", true));
                        } else {
                            $.uniform.update($("#IsNeedControl").attr("checked", false));
                        }
                        if (newData.PARENT_ID !=null) {
                            $("#ICON").attr("disabled", "disabled");
                            $("#divOperator").show();
                        } else {
                            $("#ICON").removeAttr("disabled");
                            $("#divOperator").hide();
                        }
                        //重置模块操作弹出层
                        ResetOperateModule();
                        //模块操作弹出层赋值
                        BindOperateByData(newData);
                    }
                }
            });
        }
    });
}

//新增子级
$('#addChild').click(function () {
    var ref = $('#bf_module_tree').jstree(true),
         sel = ref.get_selected();
    if (!sel.length) {
        return false;
    }
    var obj = ref._model.data[Number(sel[0])];
    document.getElementById("parent").innerHTML = obj.text;
    document.getElementById("PARENT_ID").value = obj.id;
    if (obj.id > 0) {
        //有父级
        $("#ICON").attr("disabled", "disabled");
    } else {
        $("#ICON").removeAttr("disabled");
    }
    ReloadParameters();
});

//清空
$('#clear').click(function () {
    document.getElementById("parent").innerHTML = "";
    document.getElementById("PARENT_ID").value = "";
    document.getElementById("CREATE_USER").value = "";
    document.getElementById("CREATE_DATE").value = "";
    ReloadParameters();
    ResetOperateModule();
});

//删除
$('#delete').click(function () {

    var ref = $('#bf_module_tree').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) {
        return false;
    }
    var obj = ref._model.data[Number(sel[0])];
    var msg = "";
    if (obj.children.length > 0)
        msg = "你确定要删除此菜单以及子菜单吗？";
    else
        msg = "你确定要删除此菜单吗？";

    var conditions = {};
    conditions["id"] = obj.id;
    if (confirm(msg)) {
        $.ajax({
            url: "/BfModule/DeleteBfModule",
            type: "post",
            dataType: "json",
            async: false,
            data:conditions,
            success: function (data) {
                if (data.IsSuccess == true) {
                    alert("删除成功！");
                    ReloadTree();
                }
                else
                    alert(data.Message);
            }
        });
    }
});

//保存
$('#save').click(function () {
    SaveMenu(document.getElementById("PARENT_ID").value);
    ResetOperateModule();
});
$("#J-switch").unbind("click").click(function () {
    var orgTree = $('#bf_module_tree');

    if (this.value.indexOf(' - 收起') > -1) {
        orgTree.jstree('close_all');
        $(this).val(' + 展开');
    } else {
        orgTree.jstree('open_all');
        $(this).val(' - 收起');
    }
});

//查询数据
function Query() {
    $.ajax({
        url: "/BfModule/GetBfModuleJsTree",
        type: "post",
        dataType: "json",
        success: function (data) {
            $("#bf_module_tree").jstree({
                "async": false,
                "cache": false,
                "core": {
                    "themes": {
                        "responsive": true
                    },
                    // so that create works
                    "check_callback": true,
                    'data': data
                },
                "types": {
                    "default": {
                        "icon": "fa fa-folder icon-state-warning icon-lg",
                        "valid_children": ["file"]
                    },
                    "file": {
                        "icon": "fa fa-file icon-state-warning icon-lg",
                            "valid_children": []
                    }
                },
                "plugins": ["types"]
            });
        }
    });
}

function SaveMenu(parId) {
    if ($("#CODE").val() == "") {
        alert("模块编码不能为空");
        return false;
    } else if ($("#CODE").val().replace(/[^x00-xff]/ig, 'aa').length > 50) {
        alert("模块编码长度不能超过50(一个中文相当于两个)");
        return false;
    }
    if ($("#NAME").val() == "") {
        alert("模块名称不能为空");
        return false;
    } else if ($("#NAME").val().replace(/[^x00-xff]/ig, 'aa').length > 50) {
        alert("模块名称长度不能超过50(一个中文相当于两个)");
        return false;
    }
    if ($("#ICON").val().replace(/[^x00-xff]/ig, 'aa').length > 50) {
        alert("图标长度不能超过50(一个中文相当于两个)");
        return false;
    }
    if ($("#REMARK").val().replace(/[^x00-xff]/ig, 'aa').length > 100) {
        alert("备注长度不能超过100(一个中文相当于两个)");
        return false;
    }
    var conditions = {};
    var id = $("#ID").val();
    var code = $("#CODE").val();
    var name = $("#NAME").val();
    var parentId = parId;
    var remark = $("#REMARK").val();
    var createUser = $("#CREATE_USER").val();
    var createDate = $("#CREATE_DATE").val();
    var url = $("#URL").val();
    var order = $("#ORDER").val();
    var icon = $("#ICON").val();
    var isNeedControl = document.getElementById("IsNeedControl").checked;
    var operations = [];
    var tmpOperateCodes = $("#txtOperateCodes").val().split("$^%");
    if (tmpOperateCodes != "") {
        for (var i = 0; i < tmpOperateCodes.length; i++) {
            operations.push(tmpOperateCodes[i]);
        }
    }
    var otherOperCodes = [];
    var otherOperNames = [];
    var otherOperIds = [];
    var error = false;
    var repeat = false;
    var tmpOperOtherCodes = $("#txtOperOtherCodes").val().split("$^%");
    if (tmpOperOtherCodes != "") {
        for (var i = 0; i < tmpOperOtherCodes.length; i++) {
            var otherCode = tmpOperOtherCodes[i];
            if (otherCode == "") {
                error = true;
            } else if (otherOperCodes.indexOf(otherCode) >= 0) {
                repeat = true;
            } else
                otherOperCodes.push(otherCode);
        }
        if (error) {
            alert("明细编号不能为空");
            return;
        }
        if (repeat) {
            alert("明细编号重复");
            return;
        }
    }
    var tmpOperOtherNames = $("#txtOperOtherNames").val().split("$^%");
    if (tmpOperOtherNames != "") {
        for (var i = 0; i < tmpOperOtherNames.length; i++) {
            var otherName = tmpOperOtherNames[i];
            if (otherName == "") {
                error = true;
            } else
                otherOperNames.push(otherName);
        }
    }
    var tmpOperOtherIds = $("#txtOperateIds").val().split("$^%");
    if (tmpOperOtherIds != "") {
        for (var i = 0; i < tmpOperOtherIds.length; i++) {
            var otherId = tmpOperOtherIds[i];
            otherOperIds.push(otherId);
        }
    }

    if (error) {
        alert("明细名称不能为空");
        return;
    }

    $.ajax({
        url: "/BfModule/SetBfModule",
        type: "post",
        dataType: "json",
        async: false,
        data: "id=" + id + "&code=" + code + "&name=" + name + "&parent_Id=" + parentId + "&remark=" + remark +
            "&create_user=" + createUser + "&create_date=" + createDate + "&url="
            + url + "&order=" + order + "&icon=" + icon + "&isNeedControl=" + isNeedControl
            + "&operations=" + operations + "&otherOperCodes="
            + otherOperCodes + "&otherOperNames=" + otherOperNames + "&otherOperIds=" + otherOperIds,
        success: function (data) {
            if (data.IsSuccess == true) {
                alert("操作成功！");
                ReloadTree();
            }
            else
                alert(data.Message);
        }
    });
}

function ReloadTree() {
    $("#bf_module_tree").jstree(true).destroy();
    Query();
    InitEvent();
    document.getElementById("parent").innerHTML = "";
    document.getElementById("PARENT_ID").value = "";
    document.getElementById("CREATE_USER").value = "";
    document.getElementById("CREATE_DATE").value = "";
    ReloadParameters();
}

function ReloadParameters() {
    document.getElementById("ID").value = "";
    document.getElementById("CODE").value = "";
    document.getElementById("NAME").value = "";
    document.getElementById("REMARK").value = "";
    document.getElementById("URL").value = "";
    document.getElementById("ORDER").value = "";
    document.getElementById("ICON").value = "";
    $.uniform.update($("#IsNeedControl").attr("checked", true));
}

function ResetOperateModule() {
    var $table = $('#dtlContent');
    var html = "";
    $table.html(html);
    addRowIndex = -1;
    $.uniform.update($("input[name='operation']").attr("checked", false));
    $("#txtOperateCode").val("");
}

function BindOperateByData(data) {
    var operateValue = "";
    //模块标准操作
    if (data.OperationItems != null) {
        var operation = data.OperationItems;
        for (var i = 0; i < operation.length; i++) {
            if (operation[i].CODE == "OPEN") {
                $.uniform.update($("#CHK_OPEN").attr("checked", true));
            }
            if (operation[i].CODE == "ADDNEW") {
                $.uniform.update($("#CHK_ADDNEW").attr("checked", true));
            }
            if (operation[i].CODE == "DELETE") {
                $.uniform.update($("#CHK_DELETE").attr("checked", true));
            }
            if (operation[i].CODE == "SAVE") {
                $.uniform.update($("#CHK_SAVE").attr("checked", true));
            }
            if (operateValue != "")
                operateValue += "  ";
            operateValue += operation[i].CODE;
        }
    }
    //模块其他操作
    if (data.OtherOperationItems != null) {
        var other = data.OtherOperationItems;
        var $table = $('#dtlContent');
        var html = "";
        for (var i = 0; i < other.length; i++) {
            html += "<tr id = \"tr-" + other[i].ID + "\">";
            html += "<input type='hidden' name='hiddenOldDtl' value='" + other[i].ID + "'>";
            html += "<td><input type=\"text\" name=\"OtherCode\" class=\"form-control input-medium\" value='" + other[i].CODE + "'></td>";
            html += "<td><input type=\"text\" name=\"OtherName\" class=\"form-control input-medium\" value='" + other[i].NAME + "'></td>";
            html += "<td><a class='delete' href='javascript:deleteRow(" + other[i].ID + ");'>删除</a></td></tr>";
            $table.html(html);
            if (operateValue != "")
                operateValue += "  ";
            operateValue += other[i].CODE;
        }
    }

    $("#txtOperateCode").val(operateValue);
}

function SetModuleOperate() {
    var otherOperCodeStr = "";
    var otherOperNameStr = "";
    var otherOperIdStr = "";
    var operationsStr = "";
    var operateValue = "";
    $(":checkbox[name=operation]").each(function () {
        if (this.checked) {
            var operateCode = $(this).val();
            if (operateValue != "")
                operateValue += "  ";
            operateValue += operateCode;
            if (operationsStr != "")
                operationsStr += "$^%";
            operationsStr += operateCode;
        }
    });
    $("input[name=OtherCode]").each(function (a, b) {
        var otherCode = $(this).val();
        if (otherCode != "") {
            if (operateValue != "")
                operateValue += "  ";
            operateValue += otherCode;
            if (otherOperCodeStr != "")
                otherOperCodeStr += "$^%";
            otherOperCodeStr += otherCode;
        } 
    });
    $("input[name=OtherName]").each(function (a, b) {
        var otherName = $(this).val();
        if (otherName != "") {
            if (otherOperNameStr != "")
                otherOperNameStr += "$^%";
            otherOperNameStr += otherName;
        }
    });
    $("input[name=hiddenOldDtl]").each(function (a, b) {
        if (otherOperIdStr != "")
            otherOperIdStr += "$^%";
        otherOperIdStr += $(this).val();
    });


    $("#txtOperateCodes").val(operationsStr);
    $("#txtOperOtherCodes").val(otherOperCodeStr);
    $("#txtOperOtherNames").val(otherOperNameStr);
    $("#txtOperateIds").val(otherOperIdStr);

    $("#txtOperateCode").val(operateValue);
}