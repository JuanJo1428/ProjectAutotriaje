<%@ Page Title="Motivo de consulta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MotivoConsulta.aspx.cs" Inherits="AppAutotriajeProject.MotivoConsulta" %>

<asp:Content ID="ContentMotivoConsulta" ContentPlaceHolderID="MainContent" runat="server">

    <div class="description-container">
        <h1>Motivo de consulta</h1>

        <p class="subtitle">
            Describa con el mayor detalle posible los síntomas que presenta.
            Puede escribirlos o utilizar el dictado por voz.
        </p>
    </div>

    <fieldset class="sintomas-form">

        <legend class="sr-only">
            Síntomas del paciente
        </legend>

        <asp:TextBox
            ID="txtSintomas"
            runat="server"
            ClientIDMode="Static"
            TextMode="MultiLine"
            Rows="5"
            onkeypress="return soloLetras(event)"
            placeholder="Ejemplo: Tengo dolor fuerte en el pecho desde esta mañana, acompañado de dificultad para respirar.">
        </asp:TextBox>

        <div class="voice-container">

            <asp:Button
                ID="btnHablar"
                runat="server"
                ClientIDMode="Static"
                Text="🎤 Iniciar dictado"
                CssClass="btn btn-voice"
                CausesValidation="false"
                UseSubmitBehavior="false"
                OnClientClick="return false;" />

            <div id="estadoDictado"
                 class="voice-status"
                 style="display:none;">

                🟢 Escuchando...

                <br />

                <small>
                    Hable normalmente.
                    Cuando termine, presione nuevamente el botón o simplemente pulse
                    <strong>Continuar</strong>.
                </small>

            </div>

        </div>

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
            ClientIDMode="Static"
            Text="Continuar"
            CssClass="btn btn-next"
            OnClick="btnContinuar_Click" />

    </div>

    <script type="text/javascript">
    tion soloLetras(e) {
      var key = e.keyCode || e.which;
    var tecla = String.fromCharCode(key);

    if (key === 8 || key === 13)
                return true;
            letras = /^[a-zA-ZáéíóúüñÁÉÍÓÚÜÑ\s\n.,;:()¿?¡!]*$/;
             (!letras.test(tecla)) {
          e.preventDefault();
            return false;

            }

            return true;

        }

    </script>

    <script src="Scripts/js/MotivoConsulta.js?v=2.0" defer></script>

</asp:Content>