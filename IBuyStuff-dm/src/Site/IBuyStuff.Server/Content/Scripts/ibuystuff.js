function iBuyStuff_checkOrderId() {
    var id = $("#input-order-id").val();
    var re = new RegExp('^[0-9]+$');
    var isValid = re.test(id);

    // Refresh UI
    if (!isValid) {
        iBuyStuff_setError("Invalid order ID: up to 10 digits.");
    }
    return isValid;
};

function iBuyStuff_clearErrors() {
    iBuyStuff_setError("");
};

function iBuyStuff_setError(text) {
    $("#order-id-error-message").html(text);
}

function iBuyStuff_validateCheckout() {
    var address = $("#address").val();
    var city = $("#city").val();
    var country = $("#country").val();
    var month = $("select[name='month'] option:selected").index()+1;
    var year = $("#year").val();
    var cardType = $("#cardType").val();
    var cardNumber = $("#cardNumber").val();
    
    var isValid = address != "" && city != "" && country != "" && month < 13 && cardType > 0 && cardNumber != "";

    var today = new Date();
    var currentMonth = today.getMonth();
    var currentYear = today.getFullYear();
    if (currentYear < year)
        isValid = false;
 
    if (currentYear == year && currentMonth > month)
        isValid = false;
 
    // Refresh UI
    if (!isValid) {
        iBuyStuff_checkoutSetError("Oops! Check your input. It appears to be invalid.");
    }
    return isValid;
};

function iBuyStuff_checkoutClearErrors() {
    iBuyStuff_checkoutSetError("&nbsp;");
};

function iBuyStuff_checkoutSetError(text) {
    $("#checkout-error-message").html(text);
    window.setTimeout(iBuyStuff_checkoutClearErrors, 3000);
}