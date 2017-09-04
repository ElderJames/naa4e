<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fake_payment.aspx.cs" Inherits="IBuyStuff.Server.fake_payment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fake Bank : Payment Gateway</title>
    <script type="text/javascript" src="<%= ResolveUrl("~/Content/Scripts/jquery-1.10.2.min.js")%>"></script>
</head>
    <script type="text/javascript">
        $(document).ready(function() {
            window.setTimeout(doneWithPayment, 5000);
        });
        function doneWithPayment() {
            var transactionId = getFakeTransactionId();
            var baseUrl = "<%= ResolveUrl("~/") %>";
            window.location.href = baseUrl + "order/endcheckout?tid=" + transactionId;
        }
        function getFakeTransactionId() {
            var transaction = "000";
            for (var i = 0; i < 12; i++) {
                transaction += Math.floor((Math.random() * 10) + 1);
            }
            return transaction;
        }
    </script>
<body>
    <h1>Welcome to FAKE-BANK</h1>
    <hr />
    <form id="form1" runat="server">
    <div style="text-align: center;">
        <h3>We're taking <b>a bit of money</b> out of your pocket ... </h3>
        <p>
            Don't worry about your sensitive data. We're using HTTPS here not to 
            make your heart bleeding :)
        </p>
        
        <img alt="" src="<%= ResolveUrl("~/Content/Images/Main/processing.gif")%>" />
    </div>
    </form>
</body>
</html>
