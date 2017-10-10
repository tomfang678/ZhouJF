$(function () {
    $.ajax({
        type: "get",
        url: "/PlugIn/GetAllOrgByTree",
        dataType: "json",
        success: function (result) {
            $('#DepartmentTree').treeview({
                data: result,         // 数据源
                nodeIcon: 'glyphicon glyphicon-globe',
                emptyIcon: '',    //没有子节点的节点图标
                multiSelect: false,    //多选
                levels: 1,
                onNodeSelected: function (event, data) {
                    $("#hidTmpOrg").val(data.id + "|" + data.text);
                }
            });
        },
        error: function () {
            alert("树形结构加载失败！")
        }
    });
})

function chooseOrg() {
    var tmpOrg = $("#hidTmpOrg").val();
    if(tmpOrg.split("|").length!=2)
    {
        alert("部门选择有错");
        
    }else{
        var orgId = tmpOrg.split("|")[0];
        var orgName = tmpOrg.split("|")[1];
        $("#txtOwnerOrgId").val(orgId);
        $("#txtOwnerOrgName").val(orgName);
    }
    $("#btnClose").click();
}
