function swalTip(isTrue, title, text = null) {
    va

    if (text == null) {
        Swal.fire({
            title,
            confirmButtonColor: '#FF625C',
        })
    }
    else {
        Swal.fire({
            title,
            text,
            confirmButtonColor: '#FF625C',
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
