/**
 * setLocalStorage
 * @param {any} obj
 */
function setLocalStorage(obj) {
          var { key, value } = obj;
          localStorage.setItem(key, value);
}

/**
 * getLocalStorage
 * @param {any} key
 */
function getLocalStorage(key) {
          return localStorage.getItem(key);
}

/**
 * removeLocalStorage
 * @param {any} key
 */
function removeLocalStorage(key) {
          return localStorage.removeItem(key);
}

/**
 * clearAllLocalStorage
 * */
function clearAllLocalStorage() {
          localStorage.clear();
}