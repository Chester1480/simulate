
var host = location.host;

/**
 *�ثe�Ҧ���ݪ�api route ��ݷs�W�s��route �O�o�ӳo��[�J
 **/
const apisUrl = {
          //A
          AbnormalEventLogs: host + '/api/Abnormal/QueryEventLogs',
}

// �ץX������apiurl
const exportApiUrl = (apiAction) => {
          return host + '/api/' + apiAction + '/Export';
}

const getApiUrl = (apiCode) => {
          return apisUrl[apiCode];
}