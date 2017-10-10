/// <reference path="metronic.js" />
/// <reference path="../Plugins/jquery-1.11.0.min.js" />
/// <reference path="../Plugins/datatables/media/js/jquery.dataTables.min.js" />

(function ($) {
    jQuery.fn.dataTableExtention = function (settings) {
        var $dataTable = this;

        var defaultSetting = {
            "bProcessing": true,
            "initLoadFormParams": true,
            "bServerSide": true,
            "lengthChange": false,
            "bFilter": false,
            "sAjaxSource": null,
            "sServerMethod": "POST",
            "aoColumns": null,
            "pageLength": 10,
            "pagingType": "bootstrap_full_number",
            "language": {
                "sProcessing": "处理中...",
                "sLengthMenu": "显示 _MENU_ 项结果",
                "sZeroRecords": "没有匹配结果",
                "sInfo": "显示第 _START_ 至 _END_ 项结果，共 _TOTAL_ 项",
                "sInfoEmpty": "显示第 0 至 0 项结果，共 0 项",
                "sInfoFiltered": "(由 _MAX_ 项结果过滤)",
                "sInfoPostFix": "",
                "sSearch": "搜索:",
                "sUrl": "",
                "sEmptyTable": "表中数据为空",
                "sLoadingRecords": "载入中...",
                "sInfoThousands": ",",
                "oPaginate": {
                    "sFirst": "首页",
                    "sPrevious": "上页",
                    "sNext": "下页",
                    "sLast": "末页"
                },
                "oAria": {
                    "sSortAscending": ": 以升序排列此列",
                    "sSortDescending": ": 以降序排列此列"
                }
            },
            "columnDefs": [],
            "order": [
                [0, "asc"]
            ],
            "fnRowCallback": function (nRow) {
                $("a", nRow).each(function () {
                    var gridStage = $dataTable.data("gridStage");
                    if (gridStage && this.href.length > 1 && this.href.indexOf("javascript:") == -1) {
                        this.href = jQuery.param.querystring(this.href, $.param({ gridStage: JSON.stringify(gridStage) }));
                    }
                });

                $('td', nRow).each(function (index) {
                    var columnField = settings.aoColumns[index].mDataProp;
                    //最大宽度 自动截取添加... 点击显示明细
                    var maxWidth = settings.aoColumns[index].mMaxWidth;
                    var sWidth = settings.aoColumns[index].sWidth;
                    var mFormat = settings.aoColumns[index].mFormat;
                    var copyPaste = settings.aoColumns[index].copyPaste;
                    var formatVal = content = $(this).text();

                    if (columnField === null || columnField === '') {
                        return;
                    }
                    if (copyPaste == "false") {
                        $(this).bind("cut copy paste", function (e) {
                            e.preventDefault();
                        });
                    }
                    if (mFormat === "int") {
                        //formatVal = numberFormat(parseInt(content), 0, '.', ',');
                        //$(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        if ($(this).attr("data-val")) {
                            formatVal = numberFormat(parseInt($(this).attr("data-val")), 0, '.', ',');
                            $(this).css("text-align", "right").text(formatVal);
                        }
                        else {
                            formatVal = numberFormat(parseInt(content), 0, '.', ',');
                            $(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        }
                    }
                    else if (mFormat === "money") {
                        if ($(this).attr("data-val")) {
                            formatVal = numberFormat(parseFloat($(this).attr("data-val")), 2, '.', ',');
                            $(this).css("text-align", "right").text(formatVal);
                        }
                        else {
                            formatVal = numberFormat(parseFloat(content), 2, '.', ',');
                            $(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        }
                    }
                    else if (mFormat === "percentage") {
                        if ($(this).attr("data-val")) {
                            formatVal = numberFormat(parseFloat($(this).attr("data-val")) * 100, 2, '.', ',') + '%';
                            $(this).css("text-align", "right").text(formatVal);
                        }
                        else {
                            formatVal = numberFormat(parseFloat(content) * 100, 2, '.', ',') + '%';
                            $(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        }
                    }
                    else if (mFormat && mFormat.indexOf('float') === 0) {
                        var dec = parseInt(mFormat.substring(5), 10);
                        if ($(this).attr("data-val")) {
                            formatVal = numberFormat(parseFloat($(this).attr("data-val")), dec, '.', ',');
                            $(this).css("text-align", "right").attr("data-val", $(this).attr("data-val")).text(formatVal);
                        }
                        else {
                            formatVal = numberFormat(parseFloat(content), dec, '.', ',');
                            $(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        }
                    }
                    else if (mFormat === "percentage%") {
                        if ($(this).attr("data-val")) {
                            formatVal = $(this).attr("data-val") + "%";
                            $(this).css("text-align", "right").attr("data-val", $(this).attr("data-val")).text(formatVal);
                        }
                        else {
                            formatVal = content ? content + "%" : 0 + "%";
                            $(this).css("text-align", "right").attr("data-val", content).text(formatVal);
                        }
                    }
                    if (maxWidth) {
                        var title = $dataTable.find("thead tr:last th").eq(index).text();
                        if (title && title.length > 0) {
                            title = $.trim(title);
                        }

                        var el = $("<div class='gridcell' data-trigger='manual' data-placement='right'></div>").attr("data-content", content).attr("onclick", "popup(this)").attr("data-original-title", title).css("max-width", maxWidth).text(formatVal);
                        if (sWidth) {
                            el.css("width", sWidth);
                        }
                        $(this).empty().append(el);
                    }
                    else if (sWidth) {
                        var el = $("<div ></div>").css("width", sWidth).text(formatVal);
                        $(this).empty().append(el);
                    }

                });
                return nRow;
            }
        };

        var getValueByName = function (arrObj, name) {
            if ($.isArray(arrObj) && arrObj.length > 0) {
                for (var i = 0; i < arrObj.length; i++) {
                    if (arrObj[i]["name"] && arrObj[i]["name"] === name) {
                        return arrObj[i]["value"];
                    }
                }
            }
            return null;
        };


        var filterForm = settings["filterForm"];

        var queryStringObj = jQuery.deparam.querystring();
        if (queryStringObj && queryStringObj["gridStage"]) {
            var gridStage = JSON.parse(queryStringObj["gridStage"]);

            if (gridStage && gridStage.tableId === $dataTable.attr("id")) {
                settings.displayStart = gridStage.startIndex;
                settings._iDisplayStart = gridStage.startIndex;
                settings.pageLength = gridStage.pageSize;
                settings.order = [[gridStage.sortFieldIndex, gridStage.sortDiection]];

                FormTools.restoreStage(filterForm, gridStage.paramArray);
            }
        }

        if (filterForm && $(filterForm).length > 0) {
            settings["fnServerParams"] = function (aoData) {
                var gridStage = {
                    "tableId": $dataTable.attr("id"),
                    "filterForm": filterForm,
                    "paramArray": [],
                    "startIndex": 0,
                    "pageSize": 0,
                    "sortFieldIndex": 0,
                    "sortDiection": "asc"
                };

                var paramArray = $dataTable.data("filterFormArg");

                if ($.isArray(paramArray) && paramArray.length > 0) {
                    $(paramArray).each(function () { aoData.push(this); });

                    gridStage.paramArray = paramArray;
                }

                var iDisplayStart = getValueByName(aoData, "iDisplayStart");
                if (iDisplayStart && iDisplayStart >= 0) {
                    gridStage.startIndex = iDisplayStart;
                }

                var iDisplayLength = getValueByName(aoData, "iDisplayLength");
                if (iDisplayLength && iDisplayLength >= 0) {
                    gridStage.pageSize = iDisplayLength;
                }

                var iSortingCols = getValueByName(aoData, "iSortingCols");
                if (iSortingCols && iSortingCols === 1) {
                    gridStage.sortFieldIndex = getValueByName(aoData, "iSortCol_0");
                    gridStage.sortDiection = getValueByName(aoData, "sSortDir_0");
                }

                console.log(JSON.stringify(gridStage));
                $dataTable.data("gridStage", gridStage);
            };

            $(filterForm).on("submit", function () {
                var formValue = FormTools.getStage(filterForm);// $(filterForm).serializeArray();
                $dataTable.data("filterFormArg", formValue).dataTable().fnDraw();
                //固定最后一列的时候，解决没有数据到有数据时，thead没有重新绘制的问题
                if (settings["lockLastColumn"])
                {
                    var tableId = $dataTable.attr("id");
                    var containerId = tableId + "_wrapper";
                    if ($("#" + containerId).length>0)
                    {
                        $("#" +containerId + " " + ".dataTables_scrollHeadInner").find("thead").find("tr").css("height", "auto");
                        $("#" + containerId + " " + ".DTFC_RightHeadWrapper").find("thead").find("tr").css("height", "auto");
                    }
                }
                return false;
            });


            $(filterForm).on("click", "button:reset", function () {
                $(filterForm)[0].reset();
                var formValue = FormTools.getStage(filterForm);// $(filterForm).serializeArray();
                $dataTable.data("filterFormArg", formValue).dataTable().fnDraw();
                return false;
            });
        }
        else {
            settings["fnServerParams"] = function (aoData) {
                var gridStage = {
                    "tableId": $dataTable.attr("id"),
                    "filterForm": null,
                    "paramArray": [],
                    "startIndex": 0,
                    "pageSize": 0,
                    "sortFieldIndex": 0,
                    "sortDiection": "asc"
                };

                var iDisplayStart = getValueByName(aoData, "iDisplayStart");
                if (iDisplayStart && iDisplayStart >= 0) {
                    gridStage.startIndex = iDisplayStart;
                }

                var iDisplayLength = getValueByName(aoData, "iDisplayLength");
                if (iDisplayLength && iDisplayLength >= 0) {
                    gridStage.pageSize = iDisplayLength;
                }

                var iSortingCols = getValueByName(aoData, "iSortingCols");
                if (iSortingCols && iSortingCols === 1) {
                    gridStage.sortFieldIndex = getValueByName(aoData, "iSortCol_0");
                    gridStage.sortDiection = getValueByName(aoData, "sSortDir_0");
                }

                //console.log(JSON.stringify(gridStage));
                $dataTable.data("gridStage", gridStage);
            };
        }


        settings = jQuery.extend(defaultSetting, settings);

        var initLoadFormParams = settings["initLoadFormParams"];
        if (initLoadFormParams) {
            var initParam = FormTools.getStage(filterForm);
            $dataTable.data("filterFormArg", initParam);
        }


        if (settings["lockLastColumn"]) {
            settings["scrollX"] = "100%";
            //settings["bAutoWidth"] = true;
            settings["sScrollXInner"] = "120%";
            settings["scrollCollapse"] = true;

            var grid = $dataTable.dataTable(settings);
            new $.fn.dataTable.FixedColumns(grid, {
                leftColumns: 0,
                rightColumns: 1
            });
            return grid
        }
        else {
            return $dataTable.dataTable(settings);
        }
    };
}(jQuery));

function popup(obj) {

    //$(obj).popover('destroy');
    $(obj).popover('show');
    //if ($('.popover-title').find('button').length==0) {
    //$('.popover-title').append('<button id="popovercloseid" type="button" class="close">&times;</button>');
    //}

    //关闭popover窗口
    $(document).click(function (e) {
        //if (e.target.id == "popovercloseid") {
        //    $(obj).popover('destroy');
        //}
        if ($(e.target).closest("div.popover").length == 0 && $(e.target).closest("div[onclick='popup(this)']").length == 0) {
            $(obj).popover('destroy');
        }

    });
    //    $(obj).click(function (e) {

    //});
    //$(obj).popover({ placement: 'right', trigger: 'click' });
}


FormTools = (function () {
    var getStage = function (selector) {
        /// <summary>获取表单的数据</summary>
        /// <param name="formName" type="String">表单的的jquery selector</param>
        /// <returns type="Array">表单的数据 like [{"name":"","value":""},{"name":"","value":""}]</returns>
        if ($(selector).size() > 0) {
            var paramArray = $(selector).serializeArray();
            var nameArray = [];
            var submitArray = [];
            if ($.isArray(paramArray) && paramArray.length > 0) {
                $(paramArray).each(function (index, data) {
                    if ($.inArray(data.name, nameArray) < 0) {
                        nameArray.push(data.name);
                        submitArray.push(data);
                    } else {
                        for (var i = 0; i < submitArray.length; i++) {
                            if (submitArray[i].name == data.name) {
                                submitArray[i].value = submitArray[i].value + "," + data.value;
                            }
                        }
                    }
                });
            }

            return submitArray;
        }
        else {
            return [];
        }
    };

    var getQueryStringFromForm = function (selector) {
        var array = getStage(selector);
        var paramString = "";
        if (array.length > 0) {
            array.forEach(function (obj) {
                paramString += obj.name + "=" + encodeURIComponent(obj.value) + "&";
            });
            paramString = paramString.slice(0, paramString.length - 1);
        }
        return paramString;
    }

    var restoreStage = function (selector, formDataArray) {
        /// <summary>根据表单的数据还原表单状态</summary>
        /// <param name="selector" type="String">表单的的jquery selector</param>
        /// <param name="formDataArray" type="Array">表单的数据 like [{"name":"","value":""},{"name":"","value":""}]</param>
        /// <returns type="bool"></returns>

        if ($(selector).size() > 0) {
            var f = $(selector);

            if (formDataArray.length > 0) {
                for (var i = 0; i < formDataArray.length; i++) {
                    var item = formDataArray[i];
                    if ($("[name='" + item.name + "']", f).attr("multiple") == "multiple") {
                        $("[name='" + item.name + "']", f).val(item.value.split(','));
                    } else {
                        $("[name='" + item.name + "']", f).val(item.value);
                    }
                }
            }
        }
        else {
            return [];
        }
    };

    return {
        getQueryStringFromForm: getQueryStringFromForm,
        getStage: getStage,
        restoreStage: restoreStage
    };

})();

ProgressBarTool = (function () {
    var generateProgressBar = function (selectorBarContainer, selectorBarLabel) {
        var progressbar = $(selectorBarContainer);
        var progressLabel = $(selectorBarLabel);

        return progressbar.progressbar({
            value: false,
            //change: function () {
            //    var num = progressbar.progressbar("value");
            //    var str = "";
            //    if (num < 30) {
            //        str = "数据初始化";
            //    }
            //    else if (num > 30 && num < 60) {
            //        str = "正在从数据库读取数据";
            //    }
            //    else if (num > 60 && num < 100) {
            //        str = "导出Excel";
            //    }
            //    progressLabel.text(str);
            //},
            //complete: function () {
            //    progressLabel.text("完成！");
            //}
        });
    }
    var updateProgressBarValue = function (selectorBarContainer, value) {
        var progressbar = $(selectorBarContainer);
        return progressbar.progressbar("value", value);
    }
    var updateProgressBarLabelText = function (selectorBarLabel, txt) {
        $(selectorBarLabel).text(txt);
    }
    return {
        generateProgressBar: generateProgressBar,
        updateProgressBarValue: updateProgressBarValue,
        updateProgressBarLabelText: updateProgressBarLabelText
    };
})();


ExportUITool = (function () {
    var timeIntervalId;
    var generateUI = function (exportUrl, stateUrl, finishMark) {
        var container = $("#exportUI");

        if (container.length > 0) {

            $("#exportUI iframe").attr('src', exportUrl);
        }
        else {
            var htmls = [],
                htmlStr = "";
            htmls.push('<div class="modal" id="exportUI" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="false">');
            htmls.push('<div class="modal-dialog">');
            htmls.push('<div class="modal-content">');
            htmls.push('<div class="modal-header">');
            htmls.push('<h4 class="modal-title">导出</h4>');
            htmls.push('</div>');
            htmls.push('<div class="modal-body">');
            htmls.push('<div id="progressbar"><div class="progress-label">从数据库导出中...</div></div>');
            htmls.push("<iframe id=\"iframeDown\" src=\"{0}\" style=\"display:none\" onload=\"alert(1)\"/>".replace(/\{0\}/g, exportUrl));
            htmls.push('</div>');
            htmls.push('<div class="modal-footer">');
            htmls.push('<button type="button" class="btn default" data-dismiss="modal" id="btnCancel2">转到后台</button>');
            htmls.push('</div>');
            htmls.push('</div>');
            htmls.push('</div>');
            htmls.push('</div>');
            htmlStr = htmls.join("\r\n");
            $(htmlStr).appendTo("body");
        }

        $('#exportUI').modal('show');

        ProgressBarTool.generateProgressBar("#progressbar", ".progress-label");
        //timeIntervalId = setInterval(checkState(stateUrl, finishMark), 500);
        //$("<iframe id=\"iframeDown\" src=\"{0}\" style=\"display:none\" onload=\"alert(1)\"/>".replace(/\{0\}/g, exportUrl)).appendTo("#exportUI .modal-body");
        timeIntervalId = setInterval(function () { checkState(stateUrl, finishMark); }, 1000);

    };
    var getState = function (stateUrl) {
        var result = "";
        $.ajax({
            url: stateUrl,
            type: "POST",
            dataType: "text",
            //async: false,
            success: function (data) {
                if (data) {
                    result = data;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("error:" + this.url);
            }
        });
        return result;
    }
    var checkState = function (stateUrl, finishMark) {
        //var state = getState(stateUrl);
        $.ajax({
            url: stateUrl,
            type: "POST",
            dataType: "text",
            //async: false,
            success: function (data) {
                state = data;
                if (state === finishMark) {
                    console.log(state);
                    removeAll();
                    clearInterval(timeIntervalId);
                } else {
                    console.log(state);
                    ProgressBarTool.updateProgressBarLabelText(".progress-label", state);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("error:" + this.url);
            }
        });
        //setTimeout(checkState(stateUrl, finishMark), 1000);


    }
    var removeAll = function () {
        $("#exportUI").remove();
    }

    return {
        generateUI: generateUI
    };
})();
