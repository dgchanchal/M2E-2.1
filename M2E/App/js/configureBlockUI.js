function startBlockUI(mssg, size) {
    $.blockUI({
        message: '<h' + size + '><img src="../../App/img/loading/loading123.gif" />' + mssg + '</h' + size + '>',
        baseZ: 2000,
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: .5,
            color: '#fff'
        }
    });
}

function stopBlockUI() {
    $.unblockUI();
}