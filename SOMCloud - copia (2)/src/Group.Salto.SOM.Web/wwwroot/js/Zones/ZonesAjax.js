app.zonesajax = (function () {
    var res = null;
    function ajaxCallBack(response) {
        res = response;
        return res;
    }
     var makeQuery = function (idzone) {       
         $.ajax({
             url: app.config.Urls.getPostalCodes,
            method: 'GET',
            contentType: 'application/json',
            dataType: 'json',
            async: false,
            data: { id: idzone },
            success: function (data) {
                ajaxCallBack(data);
            }
           
         });
         return res;      
    }
    
    return {       
        MakeQuery : makeQuery
    }
})();