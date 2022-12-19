/**
* Set Cookie
* @param {*} obj 
*/
function setCookie(obj) {
          var { name, value, day } = obj;

          var cookie = name + "=" + encodeURIComponent(value);

          if (typeof day === "number") {
                    cookie += `; max-age= + ${ day * 24 * 60 * 60 } ;SameSite=strict`;
          }

          document.cookie = cookie;
}

/**
 * Get Cookie
 * @param {*} cname 
 * @returns 
 */
function getCookie(cname) {
          let name = cname + "=";
          let decodedCookie = decodeURIComponent(document.cookie);
          let ca = decodedCookie.split(";");
          for (let i = 0; i < ca.length; i++) {
                    let c = ca[i];
                    while (c.charAt(0) == " ") {
                              c = c.substring(1);
                    }
                    if (c.indexOf(name) == 0) {
                              return c.substring(name.length, c.length);
                    }
          }
          return "";
}

/**
 * Delete Cookie
 * @param {*} name 
 */
function deleteCookie(name) {
          document.cookie = ` ${ name }= ''; max-age= 0;`;
}

//Useage
//設定Cookie
// var obj = {
//   name:'user',
//   value:'stan',
//   day:1
// }
// setCookie(obj)

//取得Cookie
// console.log("user: " + getCookie('user'))

//刪除Cookie
// deleteCookie('user');