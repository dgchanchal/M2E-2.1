function showToastMessage(type, message) {
    // reconfiguring the toasts as sticky
    //alert("toast message func");
    $().toastmessage({ sticky: false });

    // saving the newly created toast into a variable    
    var myToast;
    myToast = $().toastmessage('show' + type + 'Toast', message);

    // removing the toast
    //$().toastmessage('removeToast', myToast);
    //$().toastmessage('showNoticeToast', 'some message here');
    //$().toastmessage('showSuccessToast', "some message here");
    //$().toastmessage('showWarningToast', "some message here");
    //$().toastmessage('showErrorToast', "some message here");
}

function showStickyToastMessage(type, message) {
    // reconfiguring the toasts as sticky
    //alert("toast message func");
    $().toastmessage({ sticky: true });

    // saving the newly created toast into a variable    
    var myToast;
    myToast = $().toastmessage('show' + type + 'Toast', message);

    // removing the toast
    //$().toastmessage('removeToast', myToast);
    //$().toastmessage('showNoticeToast', 'some message here');
    //$().toastmessage('showSuccessToast', "some message here");
    //$().toastmessage('showWarningToast', "some message here");
    //$().toastmessage('showErrorToast', "some message here");
}