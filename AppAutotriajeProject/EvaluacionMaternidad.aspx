<%@ Page Title="Evaluación de Maternidad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EvaluacionMaternidad.aspx.cs" Inherits="AppAutotriajeProject.EvaluacionMaternidad" %>

<asp:Content ID="ContentEvaluacionMaternidad" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Evaluación de Maternidad</h1>
        <p class="subtitle">¿Se encuentra usted actualmente embarazada o ha tenido un parto reciente?</p>
    </div>

    <fieldset class="condition-form" style="border:none;">
        <legend class="sr-only">Evaluación de maternidad</legend>

        <asp:RadioButtonList
            ID="rblMaternidad"
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
            ControlToValidate="rblMaternidad"
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
