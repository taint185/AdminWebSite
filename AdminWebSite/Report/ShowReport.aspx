<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowReport.aspx.cs" Inherits="AdminWebSite.Report.ShowReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server">
                <LocalReport ReportPath="Report\Report.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource Name="MyData" DataSourceId="OrderDataSet" />
                    </DataSources>
                </LocalReport>
             </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
