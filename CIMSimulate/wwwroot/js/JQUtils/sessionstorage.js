/**
 * setSessionStorage
 * @param {any} obj
 */
function setSessionStorage(obj) {
          var { key, value } = obj;
          sessionStorage.setItem(key, value);
}

/**
 * getSessionStorage
 * @param {any} key
 */
function getSessionStorage(key) {
          return sessionStorage.getItem(key);
}

/**
 * removeSessionStorage
 * @param {any} key
 */
function removeSessionStorage(key) {
          return sessionStorage.removeItem(key);
}

/**
 * clearAllSessionStorage
 * */
function clearAllSessionStorage() {
          sessionStorage.clear();
}