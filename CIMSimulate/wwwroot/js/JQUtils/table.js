function CreateTableContent(searchContents) {
    let searchParamatersHtml = ``;
    searchContents.forEach(item => {
        searchParamatersHtml += item;
    });
    let html = `
        <div class="table_query_box">
            <div class="table_query_div">
                  <div class="query_data_div">
                        <form>
                            ${searchParamatersHtml}
                            <!--查詢按鈕-->
                            <div class="search_conditions_button search_img">
                                <button type="button" id="query_button">
                                    <div>
                                        <span>查詢</span>
                                        <span>Search</span>
                                    </div>
                                </button>
                            </div>
                        </form>
                  </div>
                  <div class="queryResult_div" id="pagination">
                        <div class="queryResult_content_div">
                            <!--no data-->
                            <div class="noData_message">
                                <span id="noDataMessage"></span>
                            </div>
                            <div class="queryResult_table_div">
                                <table class="queryResult_table" id="queryResult">
                                      <thead></thead>
                                      <!--查詢內容-->
                                      <tbody></tbody>
                                </table>
                            </div>
                        </div>
                  </div>
            </div>
        </div>
    `;
    return html;
}

function CreateSearchInput(options) {
    let { mdsize, id, chLabel, enLabel } = options;
    if (mdsize == undefined) {
        mdsize = 4
    }
    var html = `<div class="col-md-${mdsize}">
                            <input type="text" id="${id}" name="${id}" required="" />
                            <div class="title">
                                <label>${chLabel}</label>
                                <label>${enLabel}</label>
                            </div>
                        </div>`;
    return html;
}

function CreateSearchSelect(options) {
    let { mdsize, selectOptions, id, chLabel, enLabel } = options;
    if (mdsize == undefined) {
        mdsize = 4
    }
    let optionContent = "";
    Object.keys(selectOptions).forEach(key => {
        optionContent += `<option value="${selectOptions[key]}">${key}</option>`;
    });
    //selected="selected" 預設選取第一個
    var html = `<div class="col-md-4">
                        <select id="${id}" name="${id}" required="">
                            ${optionContent}
                        </select>
                        <div class="title">
                            <label>${chLabel}</label>
                            <label>${enLabel}</label>
                        </div>
                    </div>`;
    return html;
}

function CreateSearchTimeInput(options) {
    let { mdsize, startTimeId, endTimeId, chLabel, enLabel } = options;
    if (mdsize == undefined) {
        mdsize = 4
    }
    var html = `<div class="col-md-${mdsize}">
                        <div class="time_style">
                            <input type="text" id="${startTimeId}" name="${startTimeId}" autocomplete="off" />
                            <div class="date_range"></div>
                            <input type="text" id="${endTimeId}" name="${endTimeId}" autocomplete="off" />
                        </div>
                        <div class="title time_top">
                            <label>${chLabel}</label>
                            <label>${enLabel}</label>
                        </div>
                   </div>`;
    return html;
}