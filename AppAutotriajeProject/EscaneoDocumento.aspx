<%@ Page Title="Escaneo de Documento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EscaneoDocumento.aspx.cs" Inherits="AutoTriageWeb.EscaneoDocumento" %>

<asp:Content ID="ContentEscaneo" ContentPlaceHolderID="MainContent" runat="server">
    <h1 id="scanTitle">Escaneo de documento</h1>
    <p id="scanInstructions" class="subtitle">Ubique el código de barras detrás de su documento en el escáner.</p>

    <div id="scanTargetZone" class="scan-target-box">
        <i id="scanIcon" class="fa-solid fa-camera scan-main-icon" aria-hidden="true"></i>
    </div>
    <div class="flow-navigation text-left">
        <asp:Button 
            ID="btnVolver" 
            runat="server" 
            Text="Volver" 
            CssClass="btn btn-back" 
            OnClick="btnVolver_Click" />
    </div>

    <script src="Scripts/js/escaneoDocumento.js?v=1.0" defer></script>
</asp:Content>
