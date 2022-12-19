async function FillDataTable(parameters) {

          var { tableId, data, columns } = parameters

          $('#' + tableId).DataTable().destroy();
          $('#' + tableId).DataTable({
                    "data": data,
                    "fixedColumns": true,
                    //"searching": false,
                    columnDefs: [
                              {
                                        "defaultContent": "-",
                                        "targets": "_all"
                              },
                              //{ orderable: false, targets: 0 },
                    ],
                    "language": {
                              "lengthMenu": "每頁共 _MENU_ 筆",
                              "zeroRecords": "找不到相關資料",
                              "info": "頁數 _PAGE_ of _PAGES_ (共_TOTAL_筆)",
                              "infoEmpty": "目前無資料",
                              "search": "過濾:",
                              "paginate": {
                                        "first": "第一頁",
                                        "last": "最後一頁",
                                        "next": "下一頁",
                                        "previous": "上一頁"
                              },
                              "infoFiltered": "(filtered from _MAX_ total records)",
                    },
                    "order": [],//example:[[5, "asc"]]
                    "destory": true,
                    "pagingType": 'full_numbers',
                    "columns": columns
                    //example:
                    //[
                    //    { "data": "UniqueId", "width": "10%" },
                    //    { "data": "NickName", "width": "20%" },
                    //    { "data": "BetAmount", "width": "10%" },
                    //    { "data": "WinAmount", "width": "10%" },
                    //]
          }).columns.adjust().draw();
}