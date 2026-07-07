<%@ Page Title="Adelantar triage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdelantarTriage.aspx.cs" Inherits="AppAutotriajeProject.AdelantarTriage" %>

<asp:Content ID="ContentAdelantarTriage" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>¿Desea adelantar su Triage?</h1>
        <p class="subtitle">Puede contarnos sus síntomas ahora para agilizar su atención, o simplemente registrar su llegada y esperar al llamado.</p>
    </div>

    <fieldset class="condition-form" style="border:none;">
        <legend class="sr-only">Adelantar proceso de triage</legend>
        
        <asp:LinkButton ID="btnSiAT" runat="server" CssClass="btn btn-next" OnClick="ProcesarRespuesta" CommandArgument="true">
            <i class="fa-solid fa-stethoscope"></i> Sí, quiero adelantar mi triage
        </asp:LinkButton>

        <asp:LinkButton ID="btnNoAT" runat="server" CssClass="btn btn-no" OnClick="ProcesarRespuesta" CommandArgument="false">
            <i class="fa-solid fa-person-walking-arrow-right"></i> No, solo registrar mi llegada
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
