<%@ Page Title="Información del Paciente" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InformacionPaciente.aspx.cs" Inherits="AutoTriageWeb.InformacionPaciente" %>

<asp:Content ID="ContentInformacion" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Información del Paciente</h1>
        <p class="subtitle">
            Verifique que sus datos personales sean correctos antes de continuar.
        </p>
    </div>

    <fieldset style="border:none;">
        <legend class="sr-only">
            Información del paciente
        </legend>

        <div class="form-grid">
            <div class="form-group">
                <label for="txtTipoDocumento">Tipo de Documento</label>

                <asp:TextBox
                    ID="txtTipoDocumento"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    Enabled="false" />
            </div>
            
            <div class="form-group">
                <label for="txtNumeroDocumento">Número de Documento</label>

                <asp:TextBox
                    ID="txtNumeroDocumento"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    Enabled="false" />
            </div>

            <div class="form-group">
                <label for="txtPrimerNombre">
                    Primer Nombre *
                </label>

                <asp:TextBox
                    ID="txtPrimerNombre"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    placeholder="Ej. John" />

                <asp:RequiredFieldValidator
                    ID="rfvPrimerNombre"
                    runat="server"
                    ControlToValidate="txtPrimerNombre"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ErrorMessage="Debe ingresar el primer nombre." />
            </div>

            <div class="form-group">
                <label for="txtSegundoNombre">
                    Segundo Nombre
                </label>

                <asp:TextBox
                    ID="txtSegundoNombre"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    placeholder="Ej. James" />
            </div>

            <div class="form-group">
                <label for="txtPrimerApellido">
                    Primer Apellido *
                </label>

                <asp:TextBox
                    ID="txtPrimerApellido"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    placeholder="Ej. Doe" />

                <asp:RequiredFieldValidator
                    ID="rfvPrimerApellido"
                    runat="server"
                    ControlToValidate="txtPrimerApellido"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ErrorMessage="Debe ingresar el primer apellido." />
            </div>

            <div class="form-group">

                <label for="txtSegundoApellido">
                    Segundo Apellido
                </label>

                <asp:TextBox
                    ID="txtSegundoApellido"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    placeholder="Ej. Williams" />
            </div>

            <div class="form-group">
                <label for="txtFechaNacimiento">
                    Fecha de Nacimiento *
                </label>

                <asp:TextBox
                    ID="txtFechaNacimiento"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    TextMode="Date" />

                <asp:RequiredFieldValidator
                    ID="rfvFechaNacimiento"
                    runat="server"
                    ControlToValidate="txtFechaNacimiento"
                    Display="Dynamic"
                    CssClass="validation-error"
                    ErrorMessage="Debe ingresar la fecha de nacimiento." />
            </div>

            <div class="form-group">
                <label for="ddlSexoBiologico">
                    Sexo Biológico *
                </label>

                <asp:DropDownList
                    ID="ddlSexoBiologico"
                    runat="server"
                    ClientIDMode="Static"
                    CssClass="form-control"
                    DataTextField="Descripcion"
                    DataValueField="IdGenero">
                </asp:DropDownList>

                <asp:RequiredFieldValidator
                    ID="rfvSexoBiologico"
                    runat="server"
                    ControlToValidate="ddlSexoBiologico"
                    InitialValue=""
                    Display="Dynamic"
                    CssClass="validation-error"
                    ErrorMessage="Debe seleccionar el sexo biológico." />
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

        <div class="action-buttons-right">
            <button
                type="button"
                id="btnEditarPaciente"
                class="btn btn-secondary"
                onclick="InformacionPacienteUI.habilitarEdicion()"
                runat="server">
                <i class="fa-solid fa-pen-to-square"
                    aria-hidden="true"></i>
                Editar
            </button>

            <asp:Button
                ID="btnContinuar"
                runat="server"
                Text="Continuar"
                CssClass="btn btn-continue"
                OnClick="btnContinuar_Click" />
        </div>
    </div>

    <script src="Scripts/js/informacionPaciente.js?v=2.0" defer></script>
</asp:Content>
