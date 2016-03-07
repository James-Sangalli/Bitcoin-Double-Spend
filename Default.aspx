<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        .auto-style2 {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>Double spend a transaction. </td>
            </tr>
            <tr>
                <td class="auto-style1">Enter your private key (do not use this address again after disclosing the private key) </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="privateKeyTextBox" runat="server" Width="644px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Enter the funding transaction output (select one output that has enough bitcoin to double spend your payment)</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txTextBox" runat="server" Width="642px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Enter the destination address that is to be double spent</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="destinationTextBox" runat="server" Width="644px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Enter change address (double spend and other change will be sent to this address):</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="changeTextBox" runat="server" Width="644px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Enter the amount you wish to double spend (in whole bitcoin, e.g. 0.01 bitcoin)</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="amountTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    Enter the amount you wish to be returned to your change address (remainder of tx output)</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="remainderTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="sendButton" runat="server" OnClick="sendButton_Click" Text="Send" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
