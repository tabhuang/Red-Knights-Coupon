<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RD_Coupon.aspx.cs" Inherits="RD_Coupon" %>

<!DOCTYPE html>

<style>
    .Coupon table {
        font-size:16px;
        font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
        border-collapse: collapse;
        border-spacing: 0;
        width: 100%;
        }

    .Coupon td, th {
        border: 1px solid #ddd;
        text-align: left;
        padding: 8px;
    }

    .Coupon th:nth-child(1){width:100px;}
    .Coupon th:nth-child(2){width:200px;}
    .Coupon th:nth-child(3){width:400px;}
    .Coupon tr:nth-child(even){background-color: #f2f2f2}

    .Coupon th {
        padding-top: 11px;
        padding-bottom: 11px;
        background-color: #4CAF50;
        color: white;
    }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>天堂Red Knights序號登錄</title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <%--<tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="【活動時間】："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="【活動獎勵】："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <asp:Label ID="Label_No" runat="server" Text="【序號號碼】："></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="Input_No" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button_Submit" runat="server" Text="登錄序號" OnClick="Button_Submit_Click" />
                </td>
            </tr>
        </table> 
        <br/>
        <asp:Table ID="Table_Coupon" CssClass="Coupon" runat="server" BorderWidth="1" BorderStyle="Solid" GridLines="Both" BorderColor="Black">
        </asp:Table>
    </form>
</body>
</html>
