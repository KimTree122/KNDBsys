(function ($) {
    //1.得到$.ajax的对象
    var _ajax = $.ajax;
    $.ajax = function (options) {
        //2.每次调用发送ajax请求的时候定义默认的error处理方法
        var fn = {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                toastr.error(XMLHttpRequest.responseText, '错误消息', { closeButton: true, timeOut: 0, positionClass: 'toast-top-full-width' });
            },
            success: function (data, textStatus) { },
            beforeSend: function (XHR) { },
            complete: function (XHR, TS) { }
        };
        //3.如果在调用的时候写了error的处理方法，就不用默认的
        if (options.error) {
            fn.error = options.error;
        }
        if (options.success) {
            fn.success = options.success;
        }
        if (options.beforeSend) {
            fn.beforeSend = options.beforeSend;
        }
        if (options.complete) {
            fn.complete = options.complete;
        }
        //4.扩展原生的$.ajax方法，返回最新的参数
        var _options = $.extend(options, {
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                fn.error(XMLHttpRequest, textStatus, errorThrown);
            },
            success: function (data, textStatus) {
                fn.success(data, textStatus);
            },
            beforeSend: function (XHR) {
                fn.beforeSend(XHR);
            },
            complete: function (XHR, TS) {
                fn.complete(XHR, TS);
            }
        });
        //5.将最新的参数传回ajax对象
        _ajax(_options);
    };
})(jQuery);

(function (window) {
    // 封装一个把对象转化为字符串的函数，
    // 因为如果是POST请求方式时send()中如果是传递的是对象的话就要把它转换为字符串
    function serialize(params) {
        // 要把对象转换为字符串 key=value&key=value
        var arr = [];
        for (var key in params) {
            arr.push(key + '=' + params[key])
        }
        return arr.join('&')
    }
    // window
    var ajax = {
        ajax: function (opt) {
            // 1 先设置相关参数的默认值
            var
                url = opt.url || '',
                type = opt.type || 'GET',
                data = opt.data || '',
                dataType = opt.dataType || 'TEXT',
                success = opt.success || null,
                error = opt.error || null

            // 判断data传递的是对象还是字符串
            if (typeof data !== 'string') {
                data = serialize(data)
            }

            // 2 把GET和POST请求相同的内容写出来
            var xhr = new XMLHttpRequest()
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    // 表示成功 判断dataType的值，给success函数传入对应类型的值，
                    // 即判断后端传来的数据的类型根据相应的类型做出相应的处理
                    dataType = dataType.toUpperCase()
                    if (dataType === 'TEXT') {
                        success && success(xhr.responseText)
                    } else if (dataType === 'JSON') {
                        success && success(JSON.parse(xhr.responseText))
                    } else if (dataType === "XML") {
                        success && success(xhr.responseXML)
                    }
                } else if (xhr.readyState === 4 && xhr.status === 404) {
                    error && error(xhr.statusText) //判断失败是执行的回调函数
                }
            }

            // 3 根据使用者传递的type值判断是使用GET还是使用POST
            if (type.toUpperCase() === "GET") {
                // 处理url 如果用户传递了data就拼接，没有传递则还是url
                url = data ? url + "?" + data : url
                // 按GET请求发送
                xhr.open('GET', url, true)
                xhr.send()
            } else if (type.toUpperCase() === "POST") {
                // 按POST请求发送
                xhr.open('POST', url, true)
                xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded')
                xhr.send(data)
            }
        }
    }

    window.$ = ajax  // 把ajax对象暴露到全局中,这里的$就是将window中添加一个属性 在外部调用时直接 $.ajax({参数：参数值}) 即可。
}(window));

function ajax(url, data, _success, _cache, _alone, _async, _type, _dataType, _error) {
    var type = _type || 'post';//请求类型
    var dataType = _dataType || 'json';//接收数据类型
    var async = _async || true;//异步请求
    var alone = _alone || false;//独立提交（一次有效的提交）
    var cache = _cache || false;//浏览器历史缓存
    var success = _success || function (data) {
        /*console.log('请求成功');*/
        setTimeout(function () {
            layer.msg(data.msg);//通过layer插件来进行提示信息
        }, 500);
        if (data.status) {//服务器处理成功
            setTimeout(function () {
                if (data.url) {
                    location.replace(data.url);
                } else {
                    location.reload(true);
                }
            }, 1500);
        } else {//服务器处理失败
            if (alone) {//改变ajax提交状态
                ajaxStatus = true;
            }
        }
    };
    var error = _error || function (data) {
        /*console.error('请求成功失败');*/
        /*data.status;//错误状态吗*/
        layer.closeAll('loading');
        setTimeout(function () {
            if (data.status === 404) {
                layer.msg('请求失败，请求未找到');
            } else if (data.status === 503) {
                layer.msg('请求失败，服务器内部错误');
            } else {
                layer.msg('请求失败,网络连接超时');
            }
            ajaxStatus = true;
        }, 500);
    };
    /*判断是否可以发送请求*/
    if (!ajaxStatus) {
        return false;
    }
    ajaxStatus = false;//禁用ajax请求
    /*正常情况下1秒后可以再次多个异步请求，为true时只可以有一次有效请求（例如添加数据）*/
    if (!alone) {
        setTimeout(function () {
            ajaxStatus = true;
        }, 1000);
    }
    $.ajax({
        'url': url,
        'data': data,
        'type': type,
        'dataType': dataType,
        'async': async,
        'success': success,
        'error': error,
        'jsonpCallback': 'jsonp' + (new Date()).valueOf().toString().substr(-4),
        'beforeSend': function () {
            layer.msg('加载中', {
                icon: 16,
                shade: 0.01
            });
        }
    });
}

// submitAjax(post方式提交)
function submitAjax(_form, success, cache, alone) {
    cache = cache || true;
    var form = $(_form);
    var url = form.attr('action');
    var data = form.serialize();
    ajax(url, data, success, cache, alone, false, 'post', 'json');
}
/*//调用实例
$(function () {
    $('#form-login').submit(function () {
        submitAjax('#form-login');
        return false;
    });
});*/

// ajax提交(post方式提交)
function post(url, data, success, cache, alone) {
    ajax(url, data, success, cache, alone, false, 'post', 'json');
}

// ajax提交(get方式提交)
function get(url, success, cache, alone) {
    ajax(url, {}, success, alone, false, 'get', 'json');
}

// jsonp跨域请求(get方式提交)
function jsonp(url, success, cache, alone) {
    ajax(url, {}, success, cache, alone, false, 'get', 'jsonp');
}
