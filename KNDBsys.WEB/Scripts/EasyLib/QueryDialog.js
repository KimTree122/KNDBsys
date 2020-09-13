(function () {

    using = function (namespace) {
        var arr = namespace.split(".");
        var len = arr.length;
        var runJS = "";
        for (var i = 0; i < len; i++) {
            if (i >= 1) {
                runJS += ".";
            }
            runJS += arr[i];
            eval("if(typeof(" + runJS + ") === 'undefined'){" + runJS + " = function(){}}");//运行js代码, 实现变量初始化
        }
    };

    using("QueryDialog.EasySearchText");
    EasySearchText = function (params) {

        //fillControlArr    当前面板需填充控件           [$('#abc'),$('#cdf')]
        //dialogFieldArr    弹出框对应字段               ['abc','cdf']
        //dialogPath        弹出框地址                   ["./url"]
        //queryAPI          按确定后直接填充数据API地址  ["./api"]
        //queryParams       API所需参数                  ["aaa","bbb"]
        //queryValues       API提供参数控件              [$('#abc'),"bbb"]

        var dom = {
            wintype: "easyui-windowSearchText",
            winiFrame: "search-iframe"
        };

        let FillcontrolArrLen = params.fillControlArr.length;
        let FieldArrLen = params.dialogFieldArr.length;

        //检测输入数据
        let checkmsg = checkParams(FillcontrolArrLen, FieldArrLen);
        if (checkmsg) {
            $.messager.alert('警告', checkmsg + '控件绑定失效');
            return;
        }

        let querytxb = params.fillControlArr[0];

        if (querytxb.hasClass('easyui-textbox')) {
            //console.log('test');
        }

        $.extend($.fn.textbox.methods, {
            addQueryBtn: function (jq) {
                return jq.each(function () {
                    var t = $(this);
                    var opts = t.textbox('options');
                    opts.icons = [];
                    opts.icons.unshift({
                        iconCls: 'icon-clear',
                        handler: function (e) {
                            $(e.data.target).textbox('clear').textbox('textbox').focus();
                            $(this).css('visibility', 'hidden');
                            CleanControllData();
                        }
                    }, {
                            iconCls: 'icon-search',
                            handler: function (e) {
                                let txb = $(e.data.target).textbox('getValue');
                                PostData(txb);
                            }
                        });
                    t.textbox();

                    if (!t.textbox('getText')) {
                        t.textbox('getIcon', 0).css('visibility', 'hidden');
                    }
                    t.textbox('textbox').bind('keyup', function (event) {

                        switch (event.keyCode) {
                            case 13://确定
                                let txb = querytxb.textbox('getValue');
                                if (txb.length > 0 && params.queryParams.length > 0) {
                                    PostData(txb);
                                }
                                break;
                            case 8: //返回
                                CleanControllData();
                                break;
                            default:
                        }

                        var icon = t.textbox('getIcon', 0);
                        if ($(this).val()) {
                            icon.css('visibility', 'visible');
                        } else {
                            icon.css('visibility', 'hidden');
                        }

                    });

                });
            }
        });

        querytxb.textbox().textbox('addQueryBtn');

        //querytxb.textbox({
        //    inputEvents: $.extend({}, $.fn.textbox.defaults.inputEvents, {
        //        keyup: function (event) {
        //            switch (event.keyCode) {
        //                //确定
        //                case 13:
        //                    let txb = querytxb.textbox('getValue');
        //                    if (txb.length > 0 && params.queryParams.length > 0) {
        //                        PostData(txb);
        //                    }
        //                    break;
        //                    //返回
        //                case 8:
        //                    CleanControllData();
        //                    break;
        //                default:
        //            }
        //        }
        //    })
        //});

        function CleanControllData() {
            for (var i = 1; i < FillcontrolArrLen; i++) {
                var txb = params.fillControlArr[i];
                txb.textbox('setValue', '');
            }
        }

        function PostData(txbvalue) {
            let paramstr = '{';
            for (var i in params.queryParams) {
                let key = '"' + params.queryParams[i] + '":';
                if (typeof params.queryValues[i] == 'string') {
                    let value = '"' + params.queryValues[i] + '",';
                    paramstr += key + value;
                } else {
                    let value = '"' + params.queryValues[i].textbox('getValue') + '",';
                    paramstr += key + value;
                }

            }
            paramstr = paramstr.substr(0, paramstr.length - 1) + '}';
            let paramjson = JSON.parse(paramstr);
            if (txbvalue.length > 0) {
                $.get(params.queryAPI, paramjson, function (result) {
                    if (result.Row.length == 1) {
                        var entity = result.Row[0];
                        for (var i = 0; i < FieldArrLen; i++) {
                            var fieldName = params.dialogFieldArr[i]
                            for (var val in entity) {
                                if (val === fieldName) {
                                    params.fillControlArr[i].textbox('setValue', entity[val]);
                                }
                            }
                        }
                    } else {
                        showDialog();
                    }
                }, 'json');
            } else {
                showDialog();
            }
        }

        function showDialog() {

            $("." + dom.wintype).remove();
            //$("." + dom.wintype + "").window("close", true);

            var content = "<iframe  width='100%' height='99%' height='300px',frameborder='0' src='" + params.dialogPath + "' name='" + dom.winiFrame + "'></iframe>";
            $("body").append("<div class='" + dom.wintype + "' >" + content + "</div>");

            $("." + dom.wintype).window({
                title: params.title ? params.title : " 请选择",
                width: params.width ? params.width : 600,
                height: params.height ? params.height : 400,
                modal: true,
                onOpen: function () {
                    var iframe = window.frames[dom.winiFrame];
                    $(iframe).load(function () {

                        iframe.$(document).ajaxStop(function () {
                            dbclickRow($(iframe.document));
                        });

                        setTimeout(function () {
                            dbclickRow($(iframe.document));
                        }, 500);
                    });

                }
            });
        }

        function dbclickRow(framewin) {

            var $row = framewin.find("body .datagrid-body").find("tr");
            if ($row.length) {
                $row.on("dblclick", function () {
                    for (var i = 0; i < FieldArrLen; i++) {
                        var fieldName = params.dialogFieldArr[i];
                        var dbclicktext = $(this).find("td[field='" + fieldName + "']").text();
                        params.fillControlArr[i].textbox('setValue', dbclicktext);
                    }
                    $("." + dom.wintype + "").window("close", true);
                    params.success && params.success();
                });
            }

            var $row_1 = framewin.find('body').find("span");
            if ($row_1.length) {
                $row_1.on('dbclick', function () {
                    for (var i_1 = 0; i_1 < FieldArrLen; i_1++) {
                        var fieldName_1 = params.dialogFieldArr[i];
                        var dbclicktext = $(this).attr(fieldName_1);
                        params.fillControlArr[i].textbox('setValue', dbclicktext);
                    }
                    $("." + dom.wintype + "").window("close", true);
                    params.success && params.success();
                });
            }
        }

        function checkParams(fillLen, fieldLen) {
            let msg = "";
            let checkparams = false;
            if (fillLen == 0 || fieldLen == 0) {
                checkparams = true;
            } else if (fieldLen !== fillLen) {
                checkparams = true;
            } else if (params.dialogPath === null) {
                checkparams = true;
            }
            if (checkparams) {
                msg = params.fillControlArr[0].attr('label');
            }
            return msg;
        }

    };

    using("QueryDialog.EasyTextBox");
    EasyTextBox = function (params) {

        //fillControlArr    当前面板需填充控件           [$('#abc'),$('#cdf')]
        //dialogFieldArr    弹出框对应字段               ['abc','cdf']
        //dialogPath        弹出框地址                   ["./url"]
        //queryAPI          按确定后直接填充数据API地址  ["./api"]
        //queryParams       API所需参数                  ["aaa","bbb"]
        //queryValues       API提供参数控件              [$('#abc'),"bbb"]

        var dom = {
            wintype: "easyui-window",
            winiFrame: "search-iframe"
        };

        let FillcontrolArrLen = params.fillControlArr.length;
        let FieldArrLen = params.dialogFieldArr.length;

        //检测输入数据
        let checkmsg = checkParams(FillcontrolArrLen, FieldArrLen);
        if (checkmsg) {
            $.messager.alert('警告', checkmsg + '控件绑定失效');
            return;
        }

        let querytxb = params.fillControlArr[0];

        if (querytxb.hasClass('easyui-textbox')) {
            //console.log('test');
        }

        $.extend($.fn.textbox.methods, {
            addQueryBtn: function (jq) {
                return jq.each(function () {
                    var t = $(this);
                    var opts = t.textbox('options');
                    opts.icons = [];
                    opts.icons.unshift({
                        iconCls: 'icon-clear',
                        handler: function (e) {
                            $(e.data.target).textbox('clear').textbox('textbox').focus();
                            $(this).css('visibility', 'hidden');
                            CleanControllData();
                        }
                    }, {
                            iconCls: 'icon-search',
                            handler: function (e) {
                                let txb = $(e.data.target).textbox('getValue');
                                PostData(txb);
                            }
                        });
                    t.textbox();
                    if (!t.textbox('getText')) {
                        t.textbox('getIcon', 0).css('visibility', 'hidden');
                    }
                    t.textbox('textbox').bind('keyup', function () {
                        var icon = t.textbox('getIcon', 0);
                        if ($(this).val()) {
                            icon.css('visibility', 'visible');
                        } else {
                            icon.css('visibility', 'hidden');
                        }
                    });
                });
            }
        });

        querytxb.textbox().textbox('addQueryBtn');

        querytxb.textbox('textbox').keydown(function (e) {

            switch (e.keyCode) {
                //确定
                case 13:
                    let txb = querytxb.textbox('getValue');
                    PostData(txb);
                    break;
                //返回
                case 8:
                    CleanControllData();
                    break;
                default:
            }
        });

        function CleanControllData() {
            for (var i = 1; i < FillcontrolArrLen; i++) {
                var txb = params.fillControlArr[i];
                txb.textbox('setValue', '');
            }
        }

        function PostData(txbvalue) {
            let paramstr = '{';
            for (var i in params.queryParams) {
                let key = '"' + params.queryParams[i] + '":';
                let value = '"' + params.queryValues[i].textbox('getValue') + '",';
                paramstr += key + value;
            }
            paramstr = paramstr.substr(0, paramstr.length - 1) + '}';
            let paramjson = JSON.parse(paramstr);
            if (txbvalue.length > 0) {
                $.post(params.queryAPI, paramjson, function (result) {
                    if (result.total == 1) {
                        var entity = result.Row;
                        for (var i = 0; i < FieldArrLen; i++) {
                            var fieldName = params.dialogFieldArr[i]
                            for (var val in entity) {
                                if (val === fieldName) {
                                    params.fillControlArr[i].textbox('setValue', entity[val]);
                                }
                            }
                            CallBack();
                        }
                    } else {
                        showDialog();
                    }
                }, 'json');
            } else {
                showDialog();
            }
        }

        function showDialog() {

            $("." + dom.wintype).remove();

            var content = "<iframe  width='100%' height='99%' height='300px',frameborder='0' src='" + params.dialogPath + "' name='" + dom.winiFrame + "'></iframe>";
            $("body").append("<div class='" + dom.wintype + "' >" + content + "</div>");

            $("." + dom.wintype).window({
                title: params.title ? params.title : " 请选择",
                width: params.width ? params.width : 600,
                height: params.height ? params.height : 400,
                modal: true,
                onOpen: function () {
                    var iframe = window.frames[dom.winiFrame];
                    $(iframe).load(function () {
                        setTimeout(function () {
                            var $row = $(iframe.document).find("body .datagrid-body").find("tr");
                            if ($row.length) {
                                $row.on("dblclick", function () {

                                    for (var i = 0; i < FieldArrLen; i++) {
                                        var fieldName = params.dialogFieldArr[i];
                                        var dcclicktext = $(this).find("td[field='" + fieldName + "']").text();
                                        params.fillControlArr[i].textbox('setValue', dcclicktext);
                                    }
                                    CallBack();
                                    $("." + dom.wintype + "").window("close", true);
                                    params.success && params.success();
                                });
                            }
                        }, 500);
                    });
                }
            });
        }

        function CallBack() {
            console.log('js dc');
            if (params.CallBack) {
                params.CallBack();
            }
        }

        function checkParams(fillLen, fieldLen) {
            let msg = "";
            let checkparams = false;
            if (fillLen === 0 || fieldLen === 0) {
                checkparams = true;
            } else if (fieldLen !== fillLen) {
                checkparams = true;
            } else if (params.dialogPath === null) {
                checkparams = true;
            }
            if (checkparams) {
                msg = params.fillControlArr[0].attr('label');
            }
            return msg;
        }

    };

})();