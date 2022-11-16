async function jqHttpPost(apiUrl,data) {
    return new Promise(async function (resolve, reject) {
        try {
            const rawResponse =  await fetch(apiUrl, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            resolve(rawResponse.json()); // 正確完成的回傳方法
        } catch (e) {
            //console.log(e)
            reject(`${apiUrl} post失敗 發生錯誤`);  // 失敗的回傳方法
        }
    });
}
async function jqHttpGet(apiUrl, params) {
    return new Promise(function (resolve, reject) {
        try {

            let paramsStr = "";
            Object.keys(params).forEach(key => {
                paramsStr += key + "=" + params[key] + "&";
            });
            apiUrl += paramsStr;
            const rawResponse =  fetch(apiUrl, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            });
            resolve(rawResponse.json());
        } catch (e) {
            reject(`${apiUrl} get失敗 發生錯誤`)
        }
    });
}
/**
*  Call JQuery Ajax
* @param {any} parameters
*/
function jqCallJQAjax(parameters) {
          var { url, type, data } = parameters;

          return new Promise((resolve, reject) => {
                    $.ajax({
                              url: url,
                              type: type,
                              dataType: 'json',
                              data: data,
                              success: function (Response) {
                                        return resolve(Response);
                              }
                    });
          });
}
