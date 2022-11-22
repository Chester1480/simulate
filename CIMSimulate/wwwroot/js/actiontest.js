/**
 * 將物件搬入
 * */
async function SetRackMoveIn(parameters) {
    try {
        const greentran_agv_api = "http://172.20.39.237:3004";

        const { origin, group, rack, task, station } = parameters;

        let postData = {
            origin,//車輛代碼(必填)
            group,// 群組代碼(必填)
            rack,//貨架代碼(選填)
            task,//工單編號
            station,//來源站點
        };
        //getempty
        response = await jqHttpPost(`${greentran_agv_api}/rack/getEmpty`, postData);

        if (response.result != "true") {
            return "getEmpty issue: " + response;
        }

        postData = {
            origin,//車輛代碼(必填)
            cell: response.rack,//儲位代碼(必填) RK101C02"
        };

        //getinfo
        response = await jqHttpPost(`${greentran_agv_api}/rack/getInfo`, postData);

        if (result !== "true") {
            return "getInfo issue: " + response;
        } else {
            return response;
        }

        //arm move in
        ////api move in 使用API參數強制 move in
        //let { } = response;
        //parameters = {

        //};
        //response = await jqHttpPost(`${greentran_agv_api}/rack/moveIn`, parameters);
        //return {
        //    message: "完成流程設定",
        //    ...response
        //};
    } catch (e) {
        return {
            message: "設定格式有誤 請檢查 以下為錯誤訊息: " + e,
        };
    }
}

/**
 * 將物件搬出
 * */
async function SetRackMoveOut(parameters) {

    //const inputs = document.querySelectorAll('#agvDispatch input');
    //for (var i = 0; i < inputs.length; i++) {
    //    object[inputs[i].id] = inputs[i].value;
    //}
    try {
        const greentran_agv_api = "http://172.20.39.237:3004";
        
        let postData = {};

        const { origin, group, rack, tag, type } = parameters;


        //origin 車輛代碼(必填)
        //MR101
        //group 群組代碼(必填)
        //TEST
        //rack 貨架代碼(選填, 可指定)
        //RK101
        //tag 物料編號(RFID)(必填)
        //FO000198T
        //type 物料型態(選填, 可指定)
        postData = {
            origin, group, rack, tag, type 
        };
       
        let response = await jqHttpPost(`${greentran_agv_api}/rack/findProduct`, postData);
        //false return
        if (response.result != "true") {
            return "findProduct issue: "+ response;
        }

        //const {
        //    //result,
        //    //msg,
        //    group,
        //    rack,
        //    //cell,
        //    tag,
        //    type,
        //    //zone
        //} = response;

        postData = {
            origin:response.origin ,//車輛代碼(必填)MR101
            group:response.group, //群組代碼(必填)
            rack:response.rack, //貨架代碼(選填)RK101
            task:response.task, //工單編號
            station: response.station, //來源站點
        };
        //getempty
        response = await jqHttpPost(`${greentran_agv_api}/rack/getinfo`, postData);

        //false return
        if (response.result != "true") {
            return "getinfo issue: " + response;
        }

        //ResponseFormatter("txtRackGetEmpty", response);
        //let {
        //    //result,// true,
        //    //msg,//
        //    group,
        //    //zone,
        //    rack,//RK101"
        //    cell , //RK101C02 
        //} = response;

        postData = {
          origin:response.origin,//車輛代碼(必填)MR101
          cell:response.cell //儲位代碼(必填)RK101C02
        };
        //getinfo
        response = await jqHttpPost(`${greentran_agv_api}/rack/setinfo`, postData);
        //arm move in

        //let {
        //   result,//": true,
        //   msg,//": "",
        //   group,//": "",
        //   rack,//": "RK101",
        //   cell,//": "RK101C02",
        //   exist,//": false,
        //   tag,//": "",
        //   lot,//": "",
        //   task,//": "",
        //   station,//": "",
        //   status,//": "test",
        //   zone,//": "TESTC02"
        //} = response;

        if (result !== "true") {
            return "setinfo issue: " + response;
        } else {
            return response;
        }


        //api move in 使用API參數強制 move in
        //let {


        //} = response;
        //postData = {
         
        //};
        //response = await jqHttpPost(`${greentran_agv_api}/rack/moveIn`, postData);
        //return {
        //    message: "完成流程設定",
        //    ...response
        //};

        //getinfo

        //setinfo

        //arm move out

        //api move out
    } catch (e) {

    }
}


$("#btnMoveInGetEmpty").click(async function (e) {
    const inputs = document.querySelectorAll('#MoveInGetEmpty input');
    let object = {};
    for (var i = 0; i < inputs.length; i++) {
        object[inputs[i].id] = inputs[i].value;
    }
    var response = await SetRackMoveIn(object);
    ResponseFormatter("txtMoveInGetEmpty", response);

    document.getElementById("moveInOrigin").value = response.origin;
    document.getElementById("moveInCell").value = response.cell;
    document.getElementById("moveInTag").value = response.tag;
    document.getElementById("moveInLot").value = response.lot;
    document.getElementById("moveInTask").value = response.task;
    document.getElementById("moveInStation").value = response.station;
    //document.getElementById("moveInOrigin")
    //moveInOrigin
    //moveInCell
    //moveInTag
    //moveInLot
    //moveInTask
    //moveInStation

});

$("#btnKeyInMoveIn").click(async function (e) {
    const inputs = document.querySelectorAll('#keyInMoveIn input');
    let object = {};
    for (var i = 0; i < inputs.length; i++) {
        object[inputs[i].id] = inputs[i].value;
    }

    let parameters = {
        url: `http://172.20.39.237:3003/rack/moveIn`,
        data: object,
    }

    var response = await jqHttpPost(parameters.url, parameters.data);
    ResponseFormatter("txtKeyInMoveIn", response);
});

$("#btnMoveOutFIndProduct").click(async function (e) {
    const inputs = document.querySelectorAll('#moveOutFindProduct input');
    let object = {};
    for (var i = 0; i < inputs.length; i++) {
        object[inputs[i].id] = inputs[i].value;
    }

    let parameters = {
        url: `http://172.20.39.237:3003/rack/findProduct`,
        data: object,
    }
    var response = await SetRackMoveOut(object);
    //var response = await jqHttpPost(parameters.url, parameters.data);
    ResponseFormatter("txtMoveOutFIndProduct", response);

    document.getElementById("MoveOutOrigin").value = response.origin;
    document.getElementById("MoveOutCell").value = response.cell;
    document.getElementById("MoveOutTag").value = response.tag;
    document.getElementById("MoveOutForce").value = response.force;
    //MoveOutOrigin
    //MoveOutCell
    //MoveOutTag
    //MoveOutForce
});

$("#btnMoveOut").click(async function (e) {
    const inputs = document.querySelectorAll('#MoveOut input');
    let object = {};
    for (var i = 0; i < inputs.length; i++) {
        object[inputs[i].id] = inputs[i].value;
    }

    let parameters = {
        url: `http://172.20.39.237:3003/rack/moveIn`,
        data: object,
    }

    var response = await jqHttpPost(parameters.url, parameters.data);
    ResponseFormatter("txtKeyInMoveIn", response);
});
