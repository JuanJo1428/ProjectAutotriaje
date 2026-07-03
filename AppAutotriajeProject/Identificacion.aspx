<%@ Page Title="Identificación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Identificacion.aspx.cs" Inherits="AutoTriageWeb.Identificacion" %>

<asp:Content ID="ContentIdentificacion" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Identificación</h1>
    <p class="subtitle">Ingrese sus datos manualmente o escanee el código de barras de su documento.</p>

    <fieldset style="border:none;width:100%;">
        <legend class="sr-only">
            Identificación del Paciente
        </legend>

        <div class="split-flow-container">
            <div class="manual-entry-column">
                <div class="form-group">
                    <asp:DropDownList
                        ID="ddlTipoDocumento"
                        runat="server"
                        ClientIDMode="Static"
                        CssClass="touch-dropdown"
                        aria-label="Tipo de documento"
                        DataTextField="Descripcion"
                        DataValueField="IdTipoDocumento"
                        onchange="DocumentoUI.onTipoDocumentoChange()">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator
                        ID="rfvTipoDocumento"
                        runat="server"
                        ControlToValidate="ddlTipoDocumento"
                        InitialValue=""
                        Display="Dynamic"
                        CssClass="validation-error"
                        ErrorMessage="Debe seleccionar el tipo de documento." />
                </div>

                <div class="form-group">
                    <asp:TextBox
                        ID="txtDocumento"
                        runat="server"
                        ClientIDMode="Static"
                        CssClass="input-display"
                        placeholder="Ingrese número de documento"
                        aria-label="Número de documento">
                    </asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="rfvDocumento"
                        runat="server"
                        ControlToValidate="txtDocumento"
                        Display="Dynamic"
                        CssClass="validation-error"
                        ErrorMessage="Debe ingresar su número de documento para continuar." />

                    <asp:CustomValidator
                        ID="cvDocumento"
                        runat="server"
                        ControlToValidate="txtDocumento"
                        Display="Dynamic"
                        CssClass="validation-error"
                        ValidateEmptyText="false"
                        ClientValidationFunction="validateDocumento"
                        OnServerValidate="cvDocumento_ServerValidate"
                        ErrorMessage="El documento no cumple el formato del tipo seleccionado." />
                </div>

                <div id="numpadGrid" class="numpad-grid">
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('1')">1</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('2')">2</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('3')">3</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('4')">4</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('5')">5</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('6')">6</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('7')">7</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('8')">8</button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('9')">9</button>
                    <button type="button" class="num-button key-clear" onclick="DocumentoUI.backspace()">
                        <i class="fa-solid fa-delete-left" aria-hidden="true"></i>
                    </button>
                    <button type="button" class="num-button" onclick="DocumentoUI.pressNumber('0')">0</button>
                </div>
            </div>

            <div class="scan-option-column">
                <div class="divider-text">— O —</div>
                <asp:LinkButton
                    ID="lnkEscaneo"
                    runat="server"
                    CssClass="btn btn-kiosk-scan"
                    CausesValidation="false"
                    OnClick="lnkEscaneo_Click">
                    <i class="fa-solid fa-barcode" aria-hidden="true"></i>
                    <span>Escanear Documento</span>
                </asp:LinkButton>
            </div>
        </div>

        <div class="flow-navigation">
            <asp:Button
                ID="Button1"
                runat="server"
                Text="Volver"
                CssClass="btn btn-back"
                CausesValidation="false"
                OnClick="btnVolver_Click" />
            <asp:Button
                ID="Button2"
                runat="server"
                Text="Continuar"
                CssClass="btn btn-primary"
                OnClick="btnContinuar_Click" />
        </div>
    </fieldset>

    <script>
        const rfvDocumentoId = '<%= rfvDocumento.ClientID %>';
        const cvDocumentoId = '<%= cvDocumento.ClientID %>';
        
        // Inyección del JSON del servidor al entorno de JavaScript
        const CONFIG_DOCUMENTOS = <%= ReglasDocumentoJson %>;
    </script>
    <script src="Scripts/js/identificacion.js?v=1.1" defer></script>
</asp:Content>
