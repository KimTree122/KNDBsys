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

    using("QueryDialog.Query");
    QueryDialog.Query = function (params) {
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
        }

        let QuerySearchbox = params.fillControlArr[0];

        QuerySearchbox.searchbox({
            searcher: function (value, name) {
                let paramstr = '{';
                for (var i in params.queryParams) {
                    let key = '"' + params.queryParams[i] + '":';
                    let value = '"' + params.queryValues[i].textbox('getValue') + '",';
                    paramstr += key + value;
                }
                paramstr = paramstr.substr(0, paramstr.length - 1) + '}';
                let paramjson = JSON.parse(paramstr);
                if (value.length > 0) {
                    $.post(params.queryAPI, paramjson, function (result) {
                        if (result.total > 0) {
                            var entity = result.Entity;
                            for (var i = 0; i < FieldArrLen; i++) {
                                var fieldName = params.dialogFieldArr[i];
                                for (var val in entity) {
                                    if (val === fieldName) {
                                        params.fillControlArr[i].textbox('setValue', entity[val]);
                                    }
                                }
                            }
                        } else {
                            for (var j = 0; j < FieldArrLen; j++) {
                                params.fillControlArr[j].textbox('setValue', '');
                                params.fillControlArr[j].textbox('setValue', '');
                            }
                            showDialog();
                        }
                    }, 'json');
                } else {
                    showDialog();
                }
            }
        });

        QuerySearchbox.keyup(function () {
            //console.log('test');
        });

        function showDialog() {

            //$("." + dom.wintype).remove();
            var ifr = $('[name="' + dom.winiFrame + '"]');
            ifr.remove();

            var content = "<iframe  width='100%' height='99%' height='300px',frameborder='0' src='" + params.dialogPath + "' name='" + dom.winiFrame + "'></iframe>";
            $("body").append("<div class='" + dom.wintype + "' >" + content + "</div>");

            $("." + dom.wintype).window({
                closeable: true,
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

                                    $("." + dom.wintype + "").window("close", true);
                                    params.success && params.success();
                                });
                            }
                        }, 200);
                    });
                }
            });
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

    using("QueryDialog.EasyTextBox");
    QueryDialog.EasyTextBox = function (params) {
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
                    if (result.total > 0) {
                        var entity = result.Entity;
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

            //$("." + dom.wintype).remove();
            var ifr = $('[name="' + dom.winiFrame + '"]');
            ifr.remove();

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

                                    $("." + dom.wintype + "").window("close", true);
                                    params.success && params.success();
                                });
                            }
                        }, 200);
                    });
                }
            });
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