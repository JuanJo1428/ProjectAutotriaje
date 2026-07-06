<%@ Page Title="Evaluación de Salud Mental" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluacionSaludMental.aspx.cs" Inherits="AppAutotriajeProject.EvaluacionSaludMental" %>

<asp:Content ID="ContentEvaluacionSaludMental" ContentPlaceHolderID="MainContent" runat="server">

    <div class="description-container">
        <h1>Evaluación de Salud Mental</h1>
        <p class="subtitle">¿Actualmente presenta o ha presentado recientemente una condición de salud mental que requiera atención médica?</p>
    </div>

    <fieldset class="condition-form" style="border:none;">
        <legend class="sr-only">Evaluación de salud mental</legend>

        <asp:RadioButtonList
            ID="rblSaludMental"
            runat="server"
            ClientIDMode="Static"
            RepeatLayout="Flow"
            RepeatDirection="Horizontal"
            CssClass="condition-options">
            <asp:ListItem Text="No" Value="false"></asp:ListItem>
            <asp:ListItem Text="Sí" Value="true"></asp:ListItem>
        </asp:RadioButtonList>

        <asp:RequiredFieldValidator
            ID="rfvRespuesta"
            runat="server"
            ControlToValidate="rblSaludMental"
            Display="Dynamic"
            CssClass="validation-error"
            ErrorMessage="Debe seleccionar una opción antes de continuar." />
    </fieldset>

    <div class="flow-navigation">
        <asp:Button
            ID="btnVolver"
            runat="server"
            Text="Volver"
            CssClass="btn btn-back"
            CausesValidation="false"
            OnClick="btnVolver_Click" />

        <asp:Button
            ID="btnContinuar"
            runat="server"
            Text="Continuar"
            CssClass="btn btn-next"
            OnClick="btnContinuar_Click" />
    </div>
</asp:Content>
