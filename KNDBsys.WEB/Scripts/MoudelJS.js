var MoudelJS = (function (mjs) {
    var _token;
    var _errormsg;
    
   function exPostData(url, params, success, dataType) {
        $.ajax({
            type: 'POST',
            url: url,
            data: params,
            dataType: dataType,
            headers: {
                auth: _token
            },
            beforeSend: function () {
                $.messager.progress({
                    title: 'Please waiting',
                    msg: 'Loading data...'
                });
            },
            success: function (data) {
                success(data);
            },
            error: function (r, s, e) {
                //$.messager.progress('close');
                //$.messager.alert('警告', e, 'error');
                _errormsg = e;
            },
            complete: function () {
                console.log('ajax complete');
                $.messager.progress('close');
            }
        });
    }

    exPostData.prototype = function error () {
         console.log('err');
    };

    mjs.exPost = exPostData;

    return mjs;
})(window.MoudelJS || {});