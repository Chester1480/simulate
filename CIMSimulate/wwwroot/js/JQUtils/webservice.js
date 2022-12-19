
var host = location.host;

/**
 *目前所有後端的api route 後端新增新的route 記得來這邊加入
 **/
const apisUrl = {
          //A
          AbnormalEventLogs: host + '/api/Abnormal/QueryEventLogs',
}

// 匯出相關的apiurl
const exportApiUrl = (apiAction) => {
          return host + '/api/' + apiAction + '/Export';
}

const getApiUrl = (apiCode) => {
          return apisUrl[apiCode];
}