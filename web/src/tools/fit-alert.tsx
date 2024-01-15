import swal from 'sweetalert';

const fitAlert = (title: string, text: string, mode: string) => {
    swal({
        title: title,
        text: text,
        icon: mode,
    })
}

const fitAlertShort = (title: string, mode: string) => {
    swal({
        title: title,
        icon: mode
    })
}

export { fitAlert, fitAlertShort }