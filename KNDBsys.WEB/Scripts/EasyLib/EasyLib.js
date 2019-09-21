(function () {


    //模拟引入命名空间的方法  
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
    }


    //基础模块
    using("EasyLib");
    EasyLib.AutoCenter = function ($obj) {
        var w = $obj.width();
        var h = $obj.height();
        var wW = $(window).width();
        var wH = $(window).height();
        $obj.css({
            "left": (wW - w) / 2 + "px",
            "top": (wH - h) / 2 + "px"
        }).show();
    } //显示在屏幕中间 例子： EasyLib.AutoCenter($(".divLogin"));
    EasyLib.Decimal2Binary = function (num) {
        num = parseInt(num);
        var result = num.toString(2);//10 -> 2
        return result;
    } //十进制转二进制
    EasyLib.Binary2Decimal = function (num) {
        var result = parseInt(num, 2);//2 -> 10
        return result;
    } //二进制转十进制
    EasyLib.Str2Json = function (str) {
        return jQuery.parseJSON(str);
    }  //字符转json
    EasyLib.Str2Unicode = function (str) {
        var result = [];
        var len = str.length;
        for (var i = 0; i < len; i++) {
            //由于 需要转义 所以存储 两个 \  
            result.push('\\u');
            //保留4位  
            result.push(('0000' + str.charCodeAt(i).toString(16)).slice(-4));//10 转 16  
        }
        return result.join('');
    }  //字符转Unicode
    EasyLib.IsMoveTerminal = function(){
        var userAgent = window.navigator.userAgent;
        userAgent = userAgent.toLowerCase();
        if (userAgent.indexOf("linux") !== -1
            || userAgent.indexOf("android") !== -1
            || userAgent.indexOf("iphone") !== -1
            || userAgent.indexOf("ipod") !== -1
            || userAgent.indexOf("ipad") !== -1) {
            return true;
        }
        return false;
    } //检查是否移动终端
    EasyLib.Dict2JsonStr = function (dict) {
        var str = [];
        str.push("{");
        for (var k in dict) {
            str.push('"' + k + '":' + '"' + dict[k] + '",');
        }
        str = str.join("");
        str = str.substr(0, str.length - 1);//去掉最后的 ,
        str = str + "}";
        return str;
    }

    //cookies模块
    using("EasyLib.Cookies");
    EasyLib.Cookies.Set = function (name, value, day) {
        value = encodeURIComponent(value);
        var exp = new Date();
        exp.setTime(exp.getTime() + day * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + value + ";expires=" + exp.toGMTString() + ";path=/";
    }
    EasyLib.Cookies.Get = function (name) {
        var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
        if (arr) {
            return decodeURIComponent(arr[2]);
        }
    }


    //url模块
    using("EasyLib.Url");
    EasyLib.Url.GetRootPath = function (index) {
        var protocol = window.location.protocol;//http:
        var host = window.location.host;//"localhost:14571"
        var pathname = window.location.pathname;// /xxx/xxx/controller/action/id
        var arr = pathname.split("/");

        var result = protocol + "//" + host;
        if (index) {
            for (var i = 0; i < index; i++) {
                if (arr[i]) {
                    result += "/" + arr[i];
                }
            }
        }
        return result;
    } //根据当前页面返回路径 如：EasyLib.Url.GetRootPath(3) 返回http://localhost:45380/cloud/iSCM
    EasyLib.Url.Get = function (name, str) {
        var url = window.location.search;
        if (str) {
            url = str;
        }
        url = url.replace("?", "");//去掉最前面的 ?
        var arr = url.split("&");
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].indexOf(name + "=") != -1) {
                return decodeURIComponent(arr[i].replace(name + "=", ""));
                break;
            }
        }
    }
    EasyLib.Url.RmQueryParam = function (str) {
        var url = window.location.href;
        if (str) {
            url = str;
        }
        url = url.replace(/\?.*/, "");//去掉查询参数
        return url;
    }
    EasyLib.Url.GetLastNode = function (str) {
        var url = window.location.href;
        if (str) {
            url = str;
        }
        url = EasyLib.Url.RmQueryParam(url);
        var tmp = url.split("/")
        var last = tmp.length;
        return tmp[last - 1];
    }


    //正则模块
    using("EasyLib.Reg");
    EasyLib.Reg.IsCN = function (str) {
        var flag = true;
        for (var i = 0; i < str.length; i++) {
            if (!/[\u4e00-\u9fa5]/.test(str[i])) {
                flag = false;//含有非中文
            }
        }
        return flag;
    }//纯中文
    EasyLib.Reg.IsPInt = function (str)
    { return /^[1-9]+[0-9]*$/.test(str); }//正整数
    EasyLib.Reg.IsNInt = function (str)
    { return /^-[1-9]+[0-9]*$/.test(str); }//负整数
    EasyLib.Reg.IsPDec = function (str)
    { return /^[1-9]+[0-9]*\.[0-9]+$/.test(str) || /^0\.[0-9]+$/.test(str); }//正小数
    EasyLib.Reg.IsNDec = function (str)
    { return /^-[1-9]+[0-9]*\.[0-9]+$/.test(str) || /^-0\.[0-9]+$/.test(str); }//负小数
    EasyLib.Reg.IsNum = function (str) {
        return str && (str === "0" || EasyLib.Reg.IsPInt(str) || EasyLib.Reg.IsNInt(str)
    		|| EasyLib.Reg.IsPDec(str) || EasyLib.Reg.IsNDec(str));
    }//合法数字


    //日期模块
    using("EasyLib.Date");
    EasyLib.Date.Get = function (timeDiff, separator) {
        var date = new Date();
        if (timeDiff) {
            timeDiff = date.getTime() + 1000 * 60 * 60 * 24 * timeDiff;//时间差
            date.setTime(timeDiff);
        }
        var st = "-";
        if (separator) {
            st = separator;
        }

        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        if (m.toString().length === 1) {
            m = "0" + m.toString();//只有一位时补0
        }
        if (d.toString().length === 1) {
            d = "0" + d.toString();
        }
        return y + st + m + st + d;
    }//返回日期,例子:EasyLib.Date.Get() 返回 2016-07-15， EasyLib.Date.Get(-7)
    EasyLib.Date.CreateWithParam = function (str) {//javascript中IE浏览器不支持NEW DATE()带参数的解决方法
        str = str.replace(/\//g, "-");

        var strArr = str.split(" ");
        if (strArr.length === 2) {//长日期
            var y_m_d = strArr[0].split("-");
            var date = new Date();
            date.setUTCFullYear(y_m_d[0], y_m_d[1] - 1, y_m_d[2]);

            var h_mi_s = strArr[1].split(":");
            date.setUTCHours(h_mi_s[0], h_mi_s[1], h_mi_s[2]);
        }
        else {//短日期
            var y_m_d = strArr[0].split("-");
            var date = new Date();
            date.setUTCFullYear(y_m_d[0], y_m_d[1] - 1, y_m_d[2]);

            date.setUTCHours(0, 0, 0, 0);
        }
        return date;
    }
    EasyLib.Date.FormatLongDataTime = function (dateStr) {
        if (dateStr.indexOf("/Date(") != '-1') {
            var td = dateStr.replace("/Date(", "").replace(")/", "");
            var date = new Date(Number(td));

            var y = date.getUTCFullYear();
            var m = date.getUTCMonth() + 1;
            var d = date.getUTCDate();
            var h = date.getUTCHours();
            var mi = date.getUTCMinutes();
            var s = date.getUTCSeconds();

            if (m.toString().length === 1) {
                m = "0" + m.toString();//只有一位时补0
            }
            if (d.toString().length === 1) {
                d = "0" + d.toString();
            }
            if (h.toString().length === 1) {
                h = "0" + h.toString();
            }
            if (mi.toString().length === 1) {
                mi = "0" + mi.toString();
            }
            if (s.toString().length === 1) {
                s = "0" + s.toString();
            }

            return y + "-" + m + "-" + d + " " + h + ":" + mi + ":" + s;
        }
        return dateStr;
    }

    EasyLib.Date.FormatShortDataTime = function (dateStr) {
        var dateStr = EasyLib.Date.FormatLongDataTime(dateStr);
        var date = dateStr.split(" ");
        return date[0];
    }

    EasyLib.Date.FormatShortData = function (dateStr) {
        var date = dateStr.split(" ");
        return date[0];
    }

    EasyLib.Date.DateObj2Str = function (str) {
        return EasyLib.Date.FormatLongDataTime(str);
    }
    EasyLib.Date.Str2DateObj = function (str) {
        var d = EasyLib.Date.CreateWithParam(str);//ie new Date方法带参数有问题
        var ms = d.getTime();
        return "/Date(" + ms + ")/";
    }
    EasyLib.Date.NowTime = function () {
        var d = new Date()
        var vYear = d.getFullYear()
        var vMon = d.getMonth() + 1
        var vDay = d.getDate()
        var h = d.getHours();
        var m = d.getMinutes();
        var se = d.getSeconds();
        s = vYear + "-" + (vMon < 10 ? "0" + vMon : vMon) + "-" + (vDay < 10 ? "0" + vDay : vDay) + " " + (h < 10 ? "0" + h : h) + ":" + (m < 10 ? "0" + m : m) + ":" + (se < 10 ? "0" + se : se);
        return s;

    }   //获得当前年月日  返回:2016-07-18 22:50:19   liangweijian 20160719 add   
    EasyLib.Date.GetStaDay=function () {
        var nowdate = new Date();
        var firstDate = new Date(nowdate);
        firstDate.setDate(1);//上个月第一天

        var st = "-";

        var y = firstDate.getFullYear();
        var m = firstDate.getMonth();
        var d = firstDate.getDate();
        if (m.toString().length === 1) {
            m = "0" + m.toString();//只有一位时补0
        }
        if (d.toString().length === 1) {
            d = "0" + d.toString();
        }
        return y + st + m + st + d;
    }   //当月第一天    
    EasyLib.Date.GetEndDay = function () {
        var nowdate = new Date();
        var st = "-";
        var day = new Date(nowdate.getYear(), nowdate.getMonth(), 0);
        var y = nowdate.getFullYear();
        var m = nowdate.getMonth();
        var d = day.getDate();
        if (m.toString().length === 1) {
            m = "0" + m.toString();//只有一位时补0
        }
        if (d.toString().length === 1) {
            d = "0" + d.toString();
        }
        return y + st + m + st + d;
    }  //当月最后一GetEndDay天


    //字符模块
    using("EasyLib.Str");
    EasyLib.Str.Substr = function (str, n) {
        if (str.replace(/[\u4e00-\u9fa5]/g, "**").length <= n) {
            return str;
        }
        else {
            var len = 0;
            var tmpStr = "";
            for (var i = 0; i < str.length; i++) {//遍历字符串
                if (/[\u4e00-\u9fa5]/.test(str[i])) {//中文 长度为两字节
                    len += 2;
                }
                else {
                    len += 1;
                }
                if (len > n) {
                    break;
                }
                else {
                    tmpStr += str[i];
                }
            }
            return tmpStr + " ...";
        }
    }  //对应substring() 提取字符,包含对中文处理
    EasyLib.Str.RmUselessZeroInNum = function (num, n) {
        if (EasyLib.Reg.IsPDec(num) || EasyLib.Reg.IsNDec(num)) {//小数
            if (n && EasyLib.Reg.IsPInt(n)) {
                num = Number(num).toFixed(n).toString();//保留n位小数并且四舍五入
                return num;
            }
            if (n === 0) {
                num = Number(num).toFixed(n).toString();//保留n位小数并且四舍五入
                return num;
            }

            var len = num.length;
            var index = -1;
            for (var i = len - 1; i > 0; i--) {
                if (num[i] !== "0") {
                    index = i;
                    break;
                }
            }
            if (index !== -1) {
                if (num[i] === ".") {
                    return num.substring(0, index);
                }
                return num.substring(0, index + 1);
            }
            return num;
        }
        return num;
    }//删除数字多余的0,保留n位小数并且四舍五入
    EasyLib.Str.FormatMoney = function (num, n) {
        if (!EasyLib.Reg.IsNum(num)) {//非合法数字
            return num;
        }

        var num = num.toString();
        var tmpNum = num;
        tmpNum = tmpNum.replace("-", "");//先忽略负数 的 符号位
        var rnum;//小数点右边
        var lnum;//小数点左边
        var numArr = tmpNum.split(".");
        if (numArr.length === 2) {//带小数
            rnum = numArr[0];
            lnum = numArr[1];
        }
        else {
            rnum = numArr[0];
        }

        var n = n ? n : 3;
        var str = [];
        var len = rnum.length;
        for (var i = len - 1; i >= 0; i = i - n) {
            var tmpStr = [];
            for (var j = 0; j < n; j++) {
                tmpStr.push(rnum[i - j]);
            }
            if (tmpStr.length) {
                str.push(tmpStr.join(""));
            }
        }

        var result = str.join(",");
        result = result.split("");//字符串 转 数组
        result = result.reverse();//数组 倒置

        var symbol = "";
        if (num.indexOf("-") !== -1) {//负数
            symbol = "-";
        }
        if (lnum) {//带小数
            return symbol + result.join("") + "." + lnum;
        }
        else {
            return symbol + result.join("");
        }
    }  //用逗号隔开显示 金额,返回：10,222,33
    EasyLib.Str.ResetMoney = function (num) {
        var num = num.toString();
        return num.replace(/,/g, "");
    }      //不知什么鬼用，没找到那里有使用过
    EasyLib.Str.RightTrim = function (str, char) {
        if (str[str.length - 1] === char) {
            return str.substring(0, str.lastIndexOf(char));
        }
        return str;
    }   //这几个Trim 没什么鬼用，只能去一个字符 ，没找到那里有使用过
    EasyLib.Str.LeftTrim = function (str, char) {
        str = str.split("");//字符串 转 数组
        str = str.reverse();//数组 倒置
        str = str.join("");//数组 转 字符串
        str = EasyLib.Str.RightTrim(str, char);
        str = str.split("");//字符串 转 数组
        str = str.reverse();//数组 倒置
        str = str.join("");//数组 转 字符串
        return str;
    }
    EasyLib.Str.AllTrim = function (str, char) {
        str = EasyLib.Str.RightTrim(str, char);
        str = EasyLib.Str.LeftTrim(str, char);
        return str;
    }    
    EasyLib.Str.SetStrArr = function (str, filstr) {
        var i = 0;
        var myarr = new Array();
        if (parseInt(str.indexOf(filstr)) <= 0)
            myarr[0] = str;

        while (parseInt(str.indexOf(filstr)) > 0) {
            myarr[i] = str.substring(0, str.indexOf(filstr));

            str = str.substring(str.indexOf(filstr) + 1);
            i++;
        }
        myarr[i] = str;
        return myarr;
    }   //拆分字符串反回数组 liangweijian 20160719 add

    //用于表格列格式化   liangweijian 20160719 add
    using("EasyLib.ColumnFormat");
    EasyLib.ColumnFormat.FormatDate = function (datetime) {
        if (datetime) {
            return datetime.split(" ")[0];
        }
    }   //2016-07-18 22:50:19  -->2016-07-18
    EasyLib.ColumnFormat.FormatInt =function (value, row, index) {
    return EasyLib.Str.RmUselessZeroInNum(value, 0);
    }   //不保留小数  即取整
    EasyLib.ColumnFormat.FormatInt2 = function (value, row, index) {
        return EasyLib.Str.RmUselessZeroInNum(value, 2);
    } //保留2位小数 
    EasyLib.ColumnFormat.Int2 = function (value, row, index) {
        return EasyLib.Str.RmUselessZeroInNum(value, 2);
    } //保留2位小数(非formatter用)
    EasyLib.ColumnFormat.FormatInt3 = function (value, row, index) {
        return EasyLib.Str.RmUselessZeroInNum(value, 2);
    } //保留3位小数 
    EasyLib.ColumnFormat.FormatMoney2 = function (val, row, rowindex) {
        val = EasyLib.Str.RmUselessZeroInNum(val, 2);
        val = EasyLib.Str.FormatMoney(val);
        return val;
    }  //金额字段，逗号隔开 保留2位小数, 例：10,000.25 
    EasyLib.ColumnFormat.FormatMoney3 = function (val, row, rowindex) {
        val = EasyLib.Str.RmUselessZeroInNum(val, 3);
        val = EasyLib.Str.FormatMoney(val);
        return val;
    }  //金额字段，逗号隔开 保留3位小数, 例：10,000.251 
    EasyLib.ColumnFormat.FormatYesNo= function (val) {
        if (val == "1") {
            return "是"

        } else {
            return "否"
        }
    }  //是否关系替换 1是 2否


    //Ajax模块
    using("EasyLib.Ajax");
    EasyLib.Ajax.Get = function (url) {//GET 同步
        return $.ajax({
            type: "GET",
            async: false,
            url: url
        }).responseText;
    }
    EasyLib.Ajax.Post = function (url, data) {//POST 同步
        return $.ajax({
            type: "POST",
            async: false,
            url: url,
            data: data//url参数形式
        }).responseText;
    }
    EasyLib.Ajax.AsyncGet = function (url, success, error) {//GET 异步
        $.ajax({
            type: "GET",
            async: true,
            url: url,
            success: function (data) {
                success && success(data);
            },
            error: function () {
                error && error();
            }
        })
    }
    EasyLib.Ajax.AsyncPost = function (url, data, success, error) {//POST 异步
        $.ajax({
            type: "POST",
            async: true,
            url: url,
            data: data,//url参数形式
            success: function (data) {
                success && success(data);
            },
            error: function () {
                error && error();
            }
        })
    }


    //自定义控件模块 依赖easyui
    using("EasyLib.Ctl");
    EasyLib.Ctl.SearchTxt = function (dict) {
        var dom = {
            text: "EasyLib-SearchTxt",
            icon: "EasyLib-SearchTxt-Icon",
            win: "EasyLib-SearchTxt-Win",
            WinIframe: "EasyLib-SearchTxt-Win-Iframe"
        };

        var FieldArrLen = dict.FieldArr.length;
        if (dict.$DomArr.length !== FieldArrLen) {
            alert("三点控件渲染失败!原因是:提供的参数:$DomArr 和 FieldArr 数量不匹配,请检查!");
            return;
        }

        var moreFn = function (status) {
            //根据多个文本框查询
            var $MoreValArr = dict.$MoreValArr ? dict.$MoreValArr : "";
            var MoreKeyArr = dict.MoreKeyArr ? dict.MoreKeyArr : "";
            var moreParam = "";
            if ($MoreValArr && MoreKeyArr) {
                if (dict.$MoreValArr.length !== dict.MoreKeyArr.length) {
                    alert("更多查询参数不匹配!原因是:提供的参数:$MoreValArr 和 MoreKeyArr 数量不匹配,请检查!");
                    return;
                }
                for (var i = 0; i < dict.$MoreValArr.length; i++) {
                    moreParam += "&" + dict.MoreKeyArr[i] + "=" + encodeURIComponent(dict.$MoreValArr[i].val());
                }
                if (status === 0) {
                    if (dict.WinUrl.indexOf("?") === -1) {//不包含 ?
                        moreParam = "?EasyLibCtlSearchTxtParam=" + moreParam;
                    }
                }
            }
            return moreParam;
        }
        var $MustValArr = dict.$MustValArr ? dict.$MustValArr : "";//判断必填参数
        var $CountValArr = dict.$CountValArr ? dict.$CountValArr : "";//执行指定事件参数
        dict.$DomArr[0].next("." + dom.icon).remove();
        dict.$DomArr[0].addClass(dom.text).after("<span class='" + dom.icon + "'></span>");
        dict.$DomArr[0].next("." + dom.icon).on("click", function () {

            ////判断必填参数
            if (dict.$MustValArr != "" && dict.$MustValArr != undefined && dict.$MoreValArr != "" && dict.$MoreValArr != undefined) {
                if (dict.$MoreValArr[0].val() == "" || dict.$MoreValArr[0].val() == undefined) {
                    alert(dict.$MustValArr[0]);
                    return;
                }
            }

            $("." + dom.win).remove();
            var searchParam = "";
            if (dict.$search) {
                searchParam = encodeURIComponent(dict.$search.val());
            }
            var content = "<iframe width='100%' height='99%' height='300px' frameborder='0' src='"
                + dict.WinUrl + searchParam + moreFn(0) + "' name='" + dom.WinIframe + "'></iframe>";
            $("body").append("<div class='" + dom.win + "'>" + content + "</div>");
            $("." + dom.win + "").window({
                title: dict.title ? dict.title : " 请选择",
                width: dict.width ? dict.width : 600,
                height: dict.height ? dict.height : 400,
                modal: true,
                onOpen: function () {
                    var iframe = window.frames[dom.WinIframe];
                    $(iframe).load(function () {//iframe加载成功
                        iframe.$(document).ajaxStop(function () {//ajax完成
                            var $row = $(iframe.document).find("body .datagrid-body").find("tr");
                            if ($row.length) {
                                $row.on("dblclick", function () {
                                    for (var i = 0; i < FieldArrLen; i++) {
                                        var fieldName = dict.FieldArr[i];
                                        dict.$DomArr[i].val($(this).find("td[field='" + fieldName + "']").text());
                                    }
                                    if ($CountValArr != "" && $CountValArr != undefined) {//执行指定事件
                                        var envenstr = $CountValArr + "('" + dict.$MoreValArr[0].val() + "')";
                                        setTimeout(envenstr, 300)
                                    }

                                    $("." + dom.win + "").window("close", true);
                                    dict.success && dict.success();
                                });
                            }

                            var $row_1 = $(iframe.document).find("body").find("span");
                            if ($row_1.length) {
                                $row_1.on("dblclick", function () {
                                    for (var i_1 = 0; i_1 < FieldArrLen; i_1++) {
                                        var fieldName_1 = dict.FieldArr[i_1];
                                        dict.$DomArr[i_1].val($(this).attr(fieldName_1));
                                    }
                                    if ($CountValArr != "" && $CountValArr != undefined) {//执行指定事件
                                        var envenstr = $CountValArr + "('" + dict.$MoreValArr[0].val() + "')";
                                        setTimeout(envenstr, 300)
                                    }
                                    $("." + dom.win + "").window("close", true);
                                    dict.success && dict.success();
                                });
                            }
                        });

                        setTimeout(function () {
                            var $row = $(iframe.document).find("body .datagrid-body").find("tr");
                            if ($row.length) {
                                $row.on("dblclick", function () {
                                    for (var i = 0; i < FieldArrLen; i++) {
                                        var fieldName = dict.FieldArr[i];
                                        dict.$DomArr[i].val($(this).find("td[field='" + fieldName + "']").text());
                                    }
                                    if ($CountValArr != "" && $CountValArr != undefined) {//执行指定事件
                                        var envenstr = $CountValArr + "('" + dict.$MoreValArr[0].val() + "')";
                                        setTimeout(envenstr, 300)
                                    }
                                    $("." + dom.win + "").window("close", true);
                                    dict.success && dict.success();
                                });
                            }

                            var $row_1 = $(iframe.document).find("body").find("span");
                            if ($row_1.length) {
                                $row_1.on("dblclick", function () {
                                    for (var i_1 = 0; i_1 < FieldArrLen; i_1++) {
                                        var fieldName_1 = dict.FieldArr[i_1];
                                        dict.$DomArr[i_1].val($(this).attr(fieldName_1));
                                    }
                                    if ($CountValArr != "" && $CountValArr != undefined) {//执行指定事件
                                        var envenstr = $CountValArr + "('" + dict.$MoreValArr[0].val() + "')";
                                        setTimeout(envenstr, 300)
                                    }
                                    $("." + dom.win + "").window("close", true);
                                    dict.success && dict.success();
                                });
                            }
                        }, 3000);
                    });
                }
            });
        });

        var isKeypressTrigger = false;//是否已触发enter键
        dict.$DomArr[0].on("keypress", function (e) {//回车键
            if (dict.$DomArr[0].val()) {
                var e = e || window.event;
                if (e.keyCode == 13) {
                    $.getJSON(dict.AjaxUrl + encodeURIComponent(dict.$DomArr[0].val()) + moreFn(), function (data) {
                        data = data.Row;
                        if (data.length === 0) {
                            for (var i = 1; i < FieldArrLen; i++) {
                                dict.$DomArr[i].val("");
                            }
                            alert("找不到相关数据!");
                        }
                        else {
                            for (var i = 1; i < FieldArrLen; i++) {
                                var fieldName = dict.FieldArr[i];
                                dict.$DomArr[i].val(data[0][fieldName]);
                            }
                        }
                        dict.success && dict.success();
                    });
                    isKeypressTrigger = true;
                }
            }
        });
        dict.$DomArr[0].on("keyup", function (e) {//按键弹起
            var e = e || window.event;
            if (e.keyCode !== 13) {
                for (var i = 1; i < FieldArrLen; i++) {
                    dict.$DomArr[i].val("");
                }
            }
        });
        dict.$DomArr[0].on("blur", function (e) {//光标离开
            if (isKeypressTrigger) {
                isKeypressTrigger = false;
                return;
            }
            if (dict.$DomArr[0].val()) {
                $.getJSON(dict.AjaxUrl + encodeURIComponent(dict.$DomArr[0].val()) + moreFn(), function (data) {
                    data = data.Row;
                    if (data.length === 0) {
                        for (var i = 1; i < FieldArrLen; i++) {
                            dict.$DomArr[i].val("");
                        }
                        alert("找不到相关数据!");
                    }
                    else {
                        for (var i = 1; i < FieldArrLen; i++) {
                            var fieldName = dict.FieldArr[i];
                            dict.$DomArr[i].val(data[0][fieldName]);
                        }
                    }
                    dict.success && dict.success();
                });
            }
        });

        if (dict.cancelEvent) {//禁用所有事件
            dict.$DomArr[0].next("." + dom.icon).unbind("click");
            dict.$DomArr[0].unbind("keypress");
            dict.$DomArr[0].unbind("keyup");
            dict.$DomArr[0].unbind("blur");
        }
    }   //三点控件
    EasyLib.Ctl.SpanComm = function ($obj, dict, DomClass, handler, handler1) {
        var str = [];
        var index = 0;
        for (var k in dict) {
            if (index === 0) {
                str.push("<span class='" + DomClass.span + " " + DomClass.selected + "' data-val='" + dict[k] + "'>");
            }
            else {
                str.push("<span class='" + DomClass.span + "' data-val='" + dict[k] + "'>");
            }
            str.push(k);
            str.push("</span>");
            index++;
        }
        str.push("<span class='" + DomClass.result + "'></span>");
        $obj.html(str.join("")).css({ "width": "950px" });

        handler & handler(DomClass);//设置默认值

        //点击事件
        $obj.find("." + DomClass.span).on("click", function () {
            $(this).parent().find("." + DomClass.selected + "").removeClass(DomClass.selected);
            $(this).addClass(DomClass.selected);

            handler1 && handler1(DomClass, $(this), index);//更新值以及其他
        });
    }
    EasyLib.Ctl.DateSpan = function ($obj, $CusParam) {
        var dict = {
            "今天": "0",
            "三天内": "3",
            "一个星期内": "7",
            "半个月内": "15",
            "一个月内": "30",
            "其他日期 >>>": "0"
        };
        if ($CusParam && $CusParam.WithAll) {
            dict = {
                "所有": "365000",
                "今天": "0",
                "三天内": "3",
                "一个星期内": "7",
                "半个月内": "15",
                "一个月内": "30",
                "其他日期 >>>": "0"
            };
        }

        var DomClass = {
            span: "EasyLib-DateSpan",
            selected: "EasyLib-DateSpan-Selected",
            result: "EasyLib-DateSpan-Selected-Result",
            dateboxContainer: "EasyLib-DateSpan-DateBox",
            sdatebox: "EasyLib-DateSpan-StartDate",
            edatebox: "EasyLib-DateSpan-EndDate"
        };
        var SdParamName = "sd";
        if ($CusParam && $CusParam.SdParamName) {
            SdParamName = $CusParam.SdParamName;
        }
        var EdParamName = "ed";
        if ($CusParam && $CusParam.EdParamName) {
            EdParamName = $CusParam.EdParamName;
        }

        EasyLib.Ctl.SpanComm($obj, dict, DomClass, function (DomClass) {

            var result = "";
            result += "&" + SdParamName + "=" + EasyLib.Date.Get(-$("." + DomClass.selected).attr("data-val"));
            result += "&" + EdParamName + "=" + EasyLib.Date.Get(1);
            $obj.find("." + DomClass.result).attr("data-val", result);

        }, function (DomClass, $this, index) {

            var $parent = $this.parent();
            var result = "";
            result += "&" + SdParamName + "=" + EasyLib.Date.Get(-$parent.find("." + DomClass.selected).attr("data-val"));
            result += "&" + EdParamName + "=" + EasyLib.Date.Get(1);
            $parent.find("." + DomClass.result).attr("data-val", result);

            $parent.find("." + DomClass.dateboxContainer).remove();//移除日历控件
            if ($this.index() === index - 1) {//最后一个span
                $this.after(
                    "<span class='" + DomClass.dateboxContainer + "'>"
                    + "<input type='text' class='easyui-datebox " + DomClass.sdatebox + "' />"
                    + " 至: "
                    + "<input type='text' class='easyui-datebox " + DomClass.edatebox + "' />"
                    + "</span>");
                $obj.find("." + DomClass.sdatebox).datebox({
                    onSelect: function () {

                        var result = "";
                        result += "&" + SdParamName + "=" + $parent.find("." + DomClass.sdatebox).datebox("getValue");
                        result += "&" + EdParamName + "=" + $parent.find("." + DomClass.edatebox).datebox("getValue");
                        $parent.find("." + DomClass.result).attr("data-val", result);

                    }
                }).datebox("setValue", EasyLib.Date.Get(0));
                $obj.find("." + DomClass.edatebox).datebox({
                    onSelect: function () {

                        var result = "";
                        result += "&" + SdParamName + "=" + $parent.find("." + DomClass.sdatebox).datebox("getValue");
                        result += "&" + EdParamName + "=" + $parent.find("." + DomClass.edatebox).datebox("getValue");
                        $parent.find("." + DomClass.result).attr("data-val", result);

                    }
                }).datebox("setValue", EasyLib.Date.Get(1));
                $obj.find(".textbox-text").on("blur", function () {

                    var result = "";
                    result += "&" + SdParamName + "=" + $parent.find("." + DomClass.sdatebox).datebox("getValue");
                    result += "&" + EdParamName + "=" + $parent.find("." + DomClass.edatebox).datebox("getValue");
                    $parent.find("." + DomClass.result).attr("data-val", result);

                })
            }
        });
    }
    EasyLib.Ctl.DateSpan.GetValue = function ($obj) {
        return $obj.find(".EasyLib-DateSpan-Selected-Result").attr("data-val");
    }
    EasyLib.Ctl.CusSpan = function ($obj, dict) {
        var DomClass = {
            span: "EasyLib-CusSpan",
            selected: "EasyLib-CusSpan-Selected",
            result: "EasyLib-CusSpan-Selected-Result"
        };

        EasyLib.Ctl.SpanComm($obj, dict, DomClass, function (DomClass) {
            var result = $obj.find("." + DomClass.selected).attr("data-val");
            $obj.find("." + DomClass.result).attr("data-val", result);
        }, function (DomClass, $this, index) {
            var $parent = $this.parent();
            var result = $parent.find("." + DomClass.selected).attr("data-val");
            $parent.find("." + DomClass.result).attr("data-val", result);
        });
    }    // 产生下拉选项
    EasyLib.Ctl.CusSpan.GetValue = function ($obj) {
        return $obj.find(".EasyLib-CusSpan-Selected-Result").attr("data-val");
    }
    EasyLib.Ctl.StatusSpan = function ($obj) {
        EasyLib.Ctl.CusSpan($obj, {
            "不限": "不限",
            "0": "0",
            "50": "50",
            "80": "80",
            "100": "100",
            "120": "120",
            "150": "150"
        });
    }
    EasyLib.Ctl.StatusSpan.GetValue = function ($obj) {
        return EasyLib.Ctl.CusSpan.GetValue($obj);
    }
    EasyLib.Ctl.Select = function (url, $obj, success, fail) {
        $.getJSON(url, function (data) {
            data = data.Row;
            var str = [];
            str.push("<select class='EasyLib-Select'>");
            for (var i = 0; i < data.length; i++) {
                str.push("<option value='" + data[i].id + "'>" + data[i].name + "</option>");
            }
            str.push("</select>");
            $obj.html(str.join(""));
            success && success();
            if (!data.length) {
                alert("下拉框没有数据!");
            }
        }).fail(function () {
            fail && fail();
        });
    }
    EasyLib.Ctl.SelectWithAll = function (url, $obj, success, fail) {
        $.getJSON(url, function (data) {
            data = data.Row;
            var str = [];
            str.push("<select class='EasyLib-Select'>");
            str.push("<option value='' selected='selected'>全部</option>");
            for (var i = 0; i < data.length; i++) {
                str.push("<option value='" + data[i].id + "'>" + data[i].name + "</option>");
            }
            str.push("</select>");
            $obj.html(str.join(""));
            success && success();
            if (!data.length) {
                alert("下拉框没有数据!");
            }
        }).fail(function () {
            fail && fail();
        });
    }
    EasyLib.Ctl.MsgTable = function (n) {//生成标准消息列表表格字符串
        var str = "<table>";
        for (var i = 0; i < n; i++) {
            str += "<tr>"
                + "<td style='width:65%;text-align:left;padding-left:5px;'><a target='_blank'></a></td>"
                + "<td style='width:35%;text-align:right;padding-right:5px;'><span></span></td>"
                + "</tr>";
        }
        str += "<tr><td colspan='2' style='text-align:right'><a title='查看更多' target='_blank' class='EasyLib-MsgTable-A-More'>更多>></a></td></tr></table>";
        return str;
    }  //生成标准消息列表表格字符串
    EasyLib.Ctl.IndexMsgBlock = function (dict) {
        $.getJSON(dict.url, function (data) {
            data = data.Row;
            var rowNum = 12;
            dict.$dom.html(EasyLib.Ctl.MsgTable(rowNum));

            if (data.length < 12) {
                rowNum = data.length;
            }
            else {
                if (dict.rows) {
                    rowNum = dict.rows;
                }
            }

            for (var i = 0; i < rowNum; i++) {
                dict.$dom.find("a:eq(" + i + ")").attr("href", dict.RowHref + data[i][dict.SearchId]);
                var title = data[i][dict.title];
                title = dict.TitleLen ? EasyLib.Str.Substr(title, dict.TitleLen) : title;//按照指定长度截断字符串
                dict.$dom.find("a:eq(" + i + ")").html(i + 1 + "、" + title);
                var mark = "";
                if (dict.isread) {
                    if (!data[i][dict.isread]) {//未读
                        dict.$dom.find("a:eq(" + i + ")").css("color", "#f00");
                    }
                }
                if (dict.formatTime === true) {
                    dict.$dom.find("span:eq(" + i + ")").html(EasyLib.Date.FormatLongDataTime(data[i][dict.time]));
                }
                else {
                    dict.$dom.find("span:eq(" + i + ")").html(data[i][dict.time]);
                }
            }
            dict.$dom.find(".EasyLib-MsgTable-A-More").attr("href", dict.MoreHref);
            dict.success && dict.success();
        });
    }
    EasyLib.Ctl.Chart = function (dict) {
        //往jquery的script节点后面动态插入图表插件js
        var basescript = document.getElementsByTagName("script");
        for (var i = 0; i < basescript.length; i++) {
            if (basescript[i].getAttribute("src").toLowerCase().indexOf("jquery") !== -1) {
                var chartscript = document.createElement("script");
                chartscript.setAttribute("type", "text/javascript");
                chartscript.setAttribute("src", "../../Statics/js/highcharts.min.js");//图表插件

                var parent = basescript[i].parentNode;
                if (parent.lastChild == basescript[i]) {//如果父节点的最后一个节点是指定节点,则直接添加
                    parent.appendChild(chartscript);
                }
                else {//如果不是,则在指定节点的下一个节点前面插入
                    parent.insertBefore(chartscript, basescript[i].nextSibling);
                }
                break;
            }
        }

        window.setTimeout(function () {
            dict.$dom.highcharts({
                chart: {//图表类型
                    type: "spline"
                },
                title: {//标题
                    text: ""
                },
                tooltip: {//提示框
                    crosshairs: true,
                    shared: true,
                    valueSuffix: dict.TooltipValueSuffix//值后缀
                },
                xAxis: {
                    categories: dict.xAxisData//X轴 数据
                },
                yAxis: {
                    title: {
                        text: dict.yAxisTitle//Y轴 标题
                    }
                },
                series: [{
                    data: dict.yAxisData,//Y轴 数据
                    name: dict.xAxisTitle//X轴 标题
                }]
            });
        }, 1000);
    }
    EasyLib.Ctl.SetDataGridColumn = function (dict) {

        var $row1 = $(".datagrid-view > div").last().find(".datagrid-header .datagrid-header-row").first();
        var $row2 = $(".datagrid-view > div").last().find(".datagrid-header .datagrid-header-row").last();
        var $btable = $(".datagrid-view > div").last().find(".datagrid-body .datagrid-btable");

        //一个页面使用多个 datagrid 时进行范围限制
        if (dict.$obj) {
            $row1 = dict.$obj.find(".datagrid-view > div").last().find(".datagrid-header .datagrid-header-row").first();
            $row2 = dict.$obj.find(".datagrid-view > div").last().find(".datagrid-header .datagrid-header-row").last();
            $btable = dict.$obj.find(".datagrid-view > div").last().find(".datagrid-body .datagrid-btable");
        }

        //格线第1行
        if ($row1.length) {

            //统计合并列数据
            var colspanArr = [];
            for (var i = 0; i < $row1.find("td").length; i++) {
                var $td = $row1.find("td:eq(" + i + ")");
                var colspan = $td.attr("colspan");
                if ($td.attr("colspan")) {
                    colspanArr.push(Number(colspan));
                }
                else {
                    colspanArr.push(1);
                }
            }

            //根据指定索引计算第1行的真实索引
            var Row1StartIndex = -1;
            var Row1EndIndex = -1;
            if (Row1StartIndex === -1) {
                if (dict.StartIndex === 0) {
                    Row1StartIndex = 0;
                }
                else {
                    var TotalIndex = 0;
                    for (var i = 0; i < colspanArr.length; i++) {
                        TotalIndex += colspanArr[i];
                        if (TotalIndex >= dict.StartIndex) {
                            Row1StartIndex = i + 1;
                            break;
                        }
                    }
                }
            }
            if (Row1EndIndex === -1) {
                if (dict.EndIndex === -1) {
                    Row1EndIndex = $row1.find("td").length - 1;
                }
                else {
                    var TotalIndex = 0;
                    for (var i = 0; i < colspanArr.length; i++) {
                        TotalIndex += colspanArr[i];
                        if (TotalIndex > dict.EndIndex) {
                            Row1EndIndex = i;
                            break;
                        }
                    }
                }
            }

            for (var i = Row1StartIndex; i <= Row1EndIndex; i++) {
                if (dict.Visibility === "show") {
                    $row1.find("td:eq(" + i + ")").show();
                }
                else if (dict.Visibility === "hide") {
                    $row1.find("td:eq(" + i + ")").hide();
                }
            }

        }

        if (dict.EndIndex === -1) {//当结束索引为-1时的处理
            dict.EndIndex = $row2.find("td").length
            if (dict.$obj) {
                if (dict.$obj.find(".datagrid-td-rownumber").length) {//是否显示序号
                    dict.EndIndex = dict.EndIndex + 1;
                }
            }
            else {
                if ($(".datagrid-td-rownumber").length) {//是否显示序号
                    dict.EndIndex = dict.EndIndex + 1;
                }
            }
            dict = dict;
        }
        //格线第2行
        if ($row2.length) {
            for (var i = dict.StartIndex; i <= dict.EndIndex; i++) {
                if (dict.Visibility === "show") {
                    $row2.find("td:eq(" + i + ")").show();
                }
                else if (dict.Visibility === "hide") {
                    $row2.find("td:eq(" + i + ")").hide();
                }
            }
        }

        //格线具体内容
        if ($btable.length) {
            var fieldArr = [];
            for (var i = 0; i < $btable.find("tr").first().find("td").length; i++) {
                fieldArr.push($btable.find("td:eq(" + i + ")").attr("field"));
            }

            for (var i = dict.StartIndex; i <= dict.EndIndex; i++) {
                if (dict.$obj) {
                    if (dict.Visibility === "show") {
                        dict.$obj.find("[field='" + fieldArr[i] + "']").show();
                    }
                    else if (dict.Visibility === "hide") {
                        dict.$obj.find("[field='" + fieldArr[i] + "']").hide();
                    }
                }
                else {
                    if (dict.Visibility === "show") {
                        $("[field='" + fieldArr[i] + "']").show();
                    }
                    else if (dict.Visibility === "hide") {
                        $("[field='" + fieldArr[i] + "']").hide();
                    }
                }
            }
        }
    }
    EasyLib.Ctl.GetDataGridColumn = function ($obj) {
        var columns = $obj.datagrid("options").columns;//title行(非冻结列)
        var frozenColumns = $obj.datagrid("options").frozenColumns;//title行(冻结列)
        if (frozenColumns && frozenColumns.length) {
            columns = frozenColumns.concat(columns);//合并数组
        }

        var rows = $obj.datagrid("getRows");//内容行
        var footerRows = $obj.datagrid("getFooterRows");//汇总行
        if (footerRows && footerRows.length) {
            rows = rows.concat(footerRows);//合并数组
        }

        var nameList = [];
        var result = [];
        result.push("<table cellspacing='0'>");

        if (columns && columns.length) {
            for (var i = 0; i < columns.length; i++) {
                result.push("<tr>");
                for (var j = 0; j < columns[i].length; j++) {
                    var tmp_columns = columns[i][j];
                    if (!tmp_columns.hidden) {//显示的列

                        if (tmp_columns.field) {
                            nameList.push(tmp_columns);
                        }

                        result.push("<th");
                        if (tmp_columns.rowspan) {//合并行
                            result.push(" rowspan='" + tmp_columns.rowspan + "'");
                        }
                        if (tmp_columns.colspan) {//合并列
                            result.push(" colspan='" + tmp_columns.colspan + "'");
                        }
                        result.push(">");

                        if (tmp_columns.title) {
                            result.push(tmp_columns.title);
                        }
                        result.push("</th>");
                    }
                }
                result.push("</tr>");
            }
        }

        if (rows && rows.length) {
            for (var i = 0; i < rows.length; i++) {
                result.push("<tr style='text-align:left;'>");
                for (var j = 0; j < nameList.length; j++) {
                    result.push("<td>");
                    if (rows[i][nameList[j].field]) {
                        result.push(rows[i][nameList[j].field]);
                    }
                    result.push("</td>");
                }
                result.push("</tr>");
            }
        }

        result.push("</table>");
        return result.join("");
    }
    EasyLib.Ctl.MaskShow = function ($obj) {
        $obj.addClass("EasyLib-Mask-Container").append("<div class='EasyLib-Mask'></div>");
        $obj.find(".EasyLib-Mask").css({ "width": $obj.width() + "px", "height": $obj.height() + "px" });
    }
    EasyLib.Ctl.MaskHide = function ($obj) {
        $obj.removeClass("EasyLib-Mask-Container");
        $obj.find(".EasyLib-Mask").remove();
    }
    EasyLib.Ctl.FormEnterNext = function ($txt, success) {
        var attName = "data-EasyLib-Form-Seq";
        for (var i = 0; i < $txt.length; i++) {//动态添加索引
            $txt.eq(i).attr(attName, i);
        }
        $txt.on("keyup", function (e) {//监听回车
            var e = e || window.event;
            if (e.keyCode === 13) {
                var curIndex = $(this).attr(attName);
                curIndex = Number(curIndex) + 1;
                var $tmp = $txt.eq(curIndex);
                if ($tmp.length) {
                    $tmp.focus();
                }
                else {
                    success && success();
                }
            }
        });
        $txt.on("focus", function () {
            var className = "EasyLib-Form-Focus";
            $txt.removeClass(className);
            $(this).addClass(className);
        });
        $txt.eq(0).focus();
    };
    EasyLib.Ctl.Layer = function (dict) {
        var DomClass = {
            Layer: "EasyLib-Layer",
            Mask: "EasyLib-Layer-Mask",
            Container: "EasyLib-Layer-Container",
            Title: "EasyLib-Layer-Title",
            Content: "EasyLib-Layer-Content",
            Iframe: "EasyLib-Layer-Iframe"
        };
        $("." + DomClass.Layer).remove();

        var title = dict.title ? dict.title : "新窗口";
        var width = dict.width ? dict.width : "600px";
        var height = dict.height ? dict.height : "400px";
        var content = "";
        if (dict.url) {
            content = "<iframe frameborder='0' src='" + dict.url + "' class='" + DomClass.Iframe + "'></iframe>";
        }
        if (dict.html) {
            content = dict.html;
        }

        var str = [];
        str.push("<div class='" + DomClass.Layer + "'>")
        str.push("<div class='" + DomClass.Mask + "'></div>");
        str.push("<div class='" + DomClass.Container + "'>");
        str.push("<div class='" + DomClass.Title + "'>");

        //str.push(title);

        str.push("</div>");
        str.push("<div class='" + DomClass.Content + "'>");
        str.push(content);
        str.push("</div>");
        str.push("</div>");
        str.push("</div>");
        $("body").append(str.join(""));

        var $Mask = $("." + DomClass.Mask);
        var $Container = $("." + DomClass.Container);
        var $Title = $("." + DomClass.Title);
        var $Content = $("." + DomClass.Content);
        var $Iframe = $("." + DomClass.Iframe);
       
        var setWH = function () {
            $Mask.css({
                "width": $(window).width(),
                "height": $(window).height()
            });
            $Container.css({
                "width": width,
                "height": height
            });
            EasyLib.AutoCenter($Container);

            $Iframe.css({
                "width": $Container.width(),
                "height": $Container.height()
            });
        }
        setWH();

        $("body").css({
            "overflow": "hidden"
        });

        $("body").on("click", "." + DomClass.Mask,function(){
            $("." + DomClass.Layer).remove();
            dict.close && dict.close();

            $("body").css({
                "overflow": "auto"
            });
        });
        $(window).resize(function () {
            setWH();
        });
    }
    EasyLib.Ctl.Layer.Close = function () {
        $(".EasyLib-Layer-Mask").trigger("click");
    }


})();

$(function () {
    try {
        //用于直接在html进行渲染
        EasyLib.Ctl.DateSpan($(".EasyLib-Ctl-DateSpan"));
        EasyLib.Ctl.StatusSpan($(".EasyLib-Ctl-StatusSpan"));
        EasyLib.Ctl.ProductionBaseSpan($(".EasyLib-Ctl-ProductionBaseSpan"));
        EasyLib.Ctl.SetCompanySpan($(".EasyLib-Ctl-SetCompanySpan"));
    }
    catch (e) {
        //alert(e.message);
    }

    //表单
    $(".EasyLib-Form-Disabled").prop("disabled", "disabled");
});


//扩展 字符串trim,统一写在这里免得写得到处都是,例子:kk.trim();
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/(^\s*)/g, "");
}
String.prototype.rtrim = function () {
    return this.replace(/(\s*$)/g, "");
}