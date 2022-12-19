
/**
 * �M���Yid���
 * @param {any} id
 */
function clearInputById(id) {
    document.getElementById(id).innerHTML = "";
}

/**
 * �M���Ҧ�input���
 **/
function clearAllInput() {
    let inputs = document.getElementsByTagName('input');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].value = "";
    }
}

/**
 * �N�Ҧ���input����ƥ]���@�Ӫ����Kpost��get
 **/
function getAllInputValue() {
    let inputs = document.getElementsByTagName('input');
    let object = {};
    //���o�Ҧ�id�Mvalue ��z���@�Ӫ���
    for (var i = 0; i < inputs.length; i++) {
        var id = inputs[i].getAttribute('id');
        object[`'${id}'`] = document.getElementById(id).value;
    }
    return object;
}