<%@ Page Title="Evaluación de Condición Oncológica" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluacionOncologica.aspx.cs" Inherits="AppAutotriajeProject.EvaluacionOncologica" %>

<asp:Content ID="ContentEvaluacionOncologica" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Evaluación de Condición Oncológica</h1>
        <p class="subtitle">¿Actualmente se encuentra en tratamiento o seguimiento por una enfermedad oncológica?</p>
    </div>

    <fieldset class="condition-form" style="border:none;">
        <legend class="sr-only">Evaluación de condición oncológica</legend>

        <asp:RadioButtonList
            ID="rblOncologica"
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
            ControlToValidate="rblOncologica"
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
