<%@ Page Title="Motivo de consulta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MotivoConsulta.aspx.cs" Inherits="AppAutotriajeProject.MotivoConsulta" %>

<asp:Content ID="ContentMotivoConsulta" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Motivo de consulta</h1>
        <p class="subtitle">¿Qué síntomas presenta? Puede escribir o usar el micrófono.</p>
    </div>

    <fieldset class="sintomas-form">
        <legend class="sr-only">
            Síntomas del Paciente
        </legend>

        <asp:TextBox
            ID="txtSintomas"
            runat="server"
            ClientIDMode="Static"
            TextMode="MultiLine" 
            Rows="4"
            onkeypress="return soloLetras(event)"
            placeholder="Ejemplos: Tengo dolor fuerte en el pecho desde esta mañana...">
        </asp:TextBox>

        <asp:Button
            ID="btnHablar"
            runat="server"
            Text="Hablar (Voz)"
            CssClass="btn btn-voice"
            ClientIDMode="Static"
            CausesValidation="false"
            OnClientClick="return false;" />
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

    <script type="text/javascript">
        function soloLetras(e) {
            var key = e.keyCode || e.which;
            var tecla = String.fromCharCode(key).toLowerCase();
        
            var letras = /^[a-zA-Z\s\n]*$/;

            if (key === 8 || key === 13) return true;

            if (!letras.test(tecla)) {
                e.preventDefault();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
