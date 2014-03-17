<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NtierDemo.aspx.cs" Inherits="nTier_Chapman.NtierDemo" %>

<!DOCTYPE html> 

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:nTierChapmanConnectionString %>" SelectCommand="get_JobList" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        <br />
        <asp:DropDownList ID="ddlJobList" runat="server" style="z-index: 1; left: 46px; top: 91px; position: absolute">
        </asp:DropDownList>
        <asp:GridView ID="gvJobList" runat="server" style="z-index: 1; left: 542px; top: 94px; position: absolute; height: 133px; width: 187px">
        </asp:GridView>
        <br />
        <br />
        <asp:ListBox ID="lbJobList" runat="server" style="z-index: 1; left: 321px; top: 90px; position: absolute; height: 64px"></asp:ListBox>
        <br />
        
    
    </div>
    </form>
</body>
</html>
