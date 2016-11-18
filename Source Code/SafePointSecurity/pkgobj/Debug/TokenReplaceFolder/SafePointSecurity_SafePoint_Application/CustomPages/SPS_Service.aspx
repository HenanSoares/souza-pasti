<%@ Assembly Name="SafePointSecurity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2664ee7e78e28e42" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="SPS_Service.aspx.cs"
    Inherits="SafePointSecurity.SPS_Service, SafePointSecurity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=2664ee7e78e28e42"
    MasterPageFile="~masterurl/default.master"
    meta:progid="SharePoint.WebPartPage.Document" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../../Style%20Library/SafePointSecurity/js/jquery.min.js"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                url: 'SPS_Service.aspx/TesteConnection',
                cache: false,
                type: 'post',
                contentType: "application/json;",
                success: function (data) {
                    alert(data.d);
                }
            });
        });
    </script>
</asp:Content>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <%--<asp:Button runat="server" ID="button1" OnClick="Button_Click" />
    <asp:Label runat="server" ID="label1" />
    <div></div>
    <div></div>
    For more information, visit 
    <a href="http://msdn.microsoft.com/en-us/library/bb964680(v=office.12).aspx">Chapter 3: Pages and Design (Part 1 of 2)</a>--%>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    SafePointSecurity
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    SafePointSecurity: Página de Serviço e Manutenção
</asp:Content>
