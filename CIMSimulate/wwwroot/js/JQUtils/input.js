
/**
 * 清除某id欄位
 * @param {any} id
 */
function clearInputById(id) {
    document.getElementById(id).innerHTML = "";
}

/**
 * 清除所有input欄位
 **/
function clearAllInput() {
    let inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].value = "";
    }
}

/**
 * 將所有的input欄位資料包成一個物件方便post或get
 **/
function getAllInputValue() {
    let inputs = document.getElementsByTagName('input');
    let object = {};
    //取得所有id和value 整理成一個物件
    for (var i = 0; i < inputs.length; i++) {
        var id = inputs[i].getAttribute('id');
        object[`'${id}'`] = document.getElementById(id).value;
    }
    return object;
}