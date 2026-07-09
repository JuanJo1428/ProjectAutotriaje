<%@ Page Title="Escaneo de Documento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EscaneoDocumento.aspx.cs" Inherits="AppAutotriajeProject.EscaneoDocumento" %>

<asp:Content ID="ContentEscaneo" ContentPlaceHolderID="MainContent" runat="server">

    <h1 id="scanTitle">Escaneo de documento</h1>

    <p id="scanInstructions" class="subtitle">
        Ubique el código de barras o la cadena de caracteres detrás de su documento en el escáner.
    </p>

    <div id="scanTargetZone" class="scan-target-box">
        <i id="scanIcon" class="fa-solid fa-camera scan-main-icon"></i>
    </div>

    <div class="flow-navigation text-left">
        <asp:Button
            ID="btnVolver"
            runat="server"
            Text="Volver"
            CssClass="btn btn-back"
            OnClick="btnVolver_Click" />
    </div>

    <!-- Aquí se guardará la lectura completa -->
    <asp:HiddenField
        ID="hdLectura"
        runat="server"
        ClientIDMode="Static" />

    <!-- Botón oculto -->
    <asp:Button
        ID="btnEscaneoExitoso"
        runat="server"
        ClientIDMode="Static"
        Style="display:none;"
        OnClick="btnEscaneoExitoso_Click" />

    <script src="Scripts/js/escaneoDocumento.js?v=3.0" defer></script>

</asp:Content>