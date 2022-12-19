function swalTip(parameters) {
          var { isTrue, title, text } = parameters;
          let icon = 'success';
          let color = '#5cb85c';
          if (!isTrue)
          {
                    icon = 'error';
                    color = '#d9534f';
          }
                  

          if (text == null) {
                    Swal.fire({
                              title,
                              icon,
                              confirmButtonColor: color,
                    })
          }
          else {
                    Swal.fire({
                              title,
                              icon,
                              text,
                              confirmButtonColor: color,
                    })
          }

}

function swalHtml(options) {
          var { icon, title, html } = options;
          Swal.fire({
                    icon: icon,
                    title: title,
                    html: html,
                    confirmButtonColor: '#5cb85c',
          })
}

function swalConfirm(options, succesMethod) {
          const { title, text } = options
          Swal.fire({
                    title,
                    text,
                    showCancelButton: true,
                    confirmButtonColor: '#5cb85c',
                    cancelButtonColor: '#d9534f',
                    confirmButtonText: '確認',
                    cancelButtonText: '取消'
          }).then((result) => {
                    if (result) {
                              succesMethod();
                    }
          })
}
 