/**
 * 取得同樣name的數值 包成陣列
 * @param {any} name
 */
 function GetValueByName(name) {
    let elements = document.getElementsByName(name);
    let values = [];
    if (elements.length > 0) {
              elements.forEach(x => {
                        values.push(x.value)
              });
              return values;
    } else {
              return [];
    }
}

/**
* Hide Validate Text
* @param {any} id
*/
function hideValidateText(id) {
    document.getElementById(id).style.display = "none";
    document.getElementById(id).innerHTML = "";
}

/**
*  Show Validate Text
* @param {any} id
* @param {any} text
*/
function ShowValidateText(id, text) {
    document.getElementById(id).style.display = '';
    document.getElementById(id).innerHTML = text;
}

/**
* Hide Pointer Style
* @param {any} id
*/
function HidePointerEvent(id) {
    document.getElementById(id).style = "pointer-events:none";
}

/**
* Show Pointer Style
* @param {any} id
*/
function ShowPointerEvent(id) {
    document.getElementById(id).style = "pointer-events:all";
}

/**
* Add CSS Class
* @param {any} id
* @param {any} className
*/
function AddCSSClass(id, className) {
    document.getElementById(id).classList.add(className);
}

/**
* Remove CSS Class
* @param {any} id
* @param {any} className
*/
function RemoveCSSClass(id, className) {
    document.getElementById(id).classList.remove(className);
}

/**
* Let Element Enable
* @param {any} id
*/
function SetEnable(id) {
    document.getElementById(id).disabled = false;
}

/**
* Let Element Disable
* @param {any} id
*/
function SetDisable(id) {
    document.getElementById(id).disabled = true;
}

/**
* 判斷是否為空值
* @param {any} value
*/
function VaslidateNull(value) {
    if (_.isNull(value))
              return true;
    else
              return false;
}

/**
* Validate Length
* @param {any} value
* @param {any} min
* @param {any} max
*/
function ValidateLength(value, min, max) {
    if (value.length >= min && value.length <= max)
              return true;
    else
              return false;

    //return validator.isLength(value, { min: min, max: max });
}

/**
* Validate Type
* @param {any} value
*/
function ValidateType(value) {
    if (_.isString(value))
              return true;
    else
              return false;
}

/**
* Validate Email
* @param {any} value
*/
function ValidateEmail(value) {
    return validator.isEmail(value);

    //正規表達式
    //return string(value)
    //    .tolowercase()
    //    .match(
    //        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-za-z\-0-9]+\.)+[a-za-z]{2,}))$/
    //    );
}

/**
* Validate 中文
* @param {any} value
*/
function ValidatePureChinese(value) {
    var reg = /^[\u4E00-\u9FA5]+$/
    if (reg.test(value)) {
              return true
    } else {
              return false
    }
}

/**
* Validate Phone
* @param {any} value
*/
function ValidatePhone(value) {
    return String(value)
              .match(
                        /^09[0-9]{8}$/
              );
    //另外的表達式 /^09\d{2}(\d{6}|-\d{3}-\d{3})$/   
}

/**
* Validate  Password Match
* @param {any} password
* @param {any} cpassword
*/
function ValidatePasswordMatch(password, cpassword) {
    return _.isEqual(password, cpassword);
}

/**
* Validate Date
* @param {any} date
*/
function ValidateIsDate(date) {
    return validator.isDate(date);
}

/**
* 判別起訖日期規則
* @param {any} startdate
* @param {any} enddate
*/
function CheckDate(startdate, enddate) {
    var start = moment(startdate);
    var end = moment(enddate);

    if (end <= start) {
              return false;
    } else {
              return true;
    }
}

function beautifyFormatter(textareaid) {
    var ugly = document.getElementById(textareaid).value;
    var obj = JSON.parse(ugly);
    var pretty = JSON.stringify(obj, undefined, 4);
    document.getElementById(textareaid).value = pretty;
}

function GetAllInputValue() {
          var ids = document.getElementsByTagName('input');
          let data = {};
          for (var i = 0; i < ids.length; i++) {
                    if (!ids[i].disabled) {
                              data[ids[i].id] = ids[i].value;
                    }
          }
          return data;
}