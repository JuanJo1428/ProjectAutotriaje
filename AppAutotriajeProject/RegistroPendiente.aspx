<%@ Page Title="Registro pendiente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistroPendiente.aspx.cs" Inherits="AppAutotriajeProject.RegistroPendiente" %>

<asp:Content ID="ContentRegistroPendiente" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Registro Pendiente</h1>
        <p class="subtitle">Usted tiene un registro pendiente ¿Desea continuarlo?</p>
    </div>

    <fieldset class="condition-form" style="border:none;">
        <legend class="sr-only">Continuar registro pendiente</legend>
        
        <asp:LinkButton ID="btnSiRP" runat="server" CssClass="btn btn-next" OnClick="ProcesarRespuesta" CommandArgument="true">
            <i class="fas fa-asterisk"></i> Sí, continuar registro
        </asp:LinkButton>

        <asp:LinkButton ID="btnNoRP" runat="server" CssClass="btn btn-no" OnClick="ProcesarRespuesta" CommandArgument="false">
            <i class="fas fa-asterisk"></i> No, volver a sala de espera
        </asp:LinkButton>
    </fieldset>

    <div class="flow-navigation">
    <asp:Button
        ID="btnVolver"
        runat="server"
        Text="Volver"
        CssClass="btn btn-back"
        CausesValidation="false"
        OnClick="btnVolver_Click" />
    </div>
</asp:Content>
