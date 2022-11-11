function swalTip(parameters) {
    var { isTrue, title, text} = parameters;
    let icon= 'success';
    if(!isTrue)
        icon = 'error';

    if (text == null) {
        Swal.fire({
            title,
            icon,
            confirmButtonColor: '#333',
        })
    }
    else {
        Swal.fire({
            title,
            icon,
            text,
            confirmButtonColor: '#333',
        })
    }
  
}

function swalHtml(options) {
    var { icon, title, html } = options;
    Swal.fire({
        icon: icon,
        title: title,
        html: html,
        confirmButtonColor: '#FF625C',
    })
}

function swalConfirm(options, succesMethod) {
    const { title, text } = options
    Swal.fire({
        title,
        text,
        showCancelButton: true,
        confirmButtonColor: '#FF625C',
        cancelButtonColor: '#d33',
        confirmButtonText: '確認',
        cancelButtonText: '取消'
    }).then((result) => {
        if (result) {
            succesMethod();
        }
    })
}
