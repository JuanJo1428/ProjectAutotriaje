<%@ Page Title="Sala de Espera" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaEspera.aspx.cs" Inherits="AppAutotriajeProject.SalaEspera" %>

<asp:Content ID="ContentSalaEspera" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Sala de Espera</h1>
        <p class="subtitle">Pacientes actualmente en espera de atención.</p>
    </div>

    <div class="sala-espera-container">
        <asp:Repeater ID="rptPacientesEspera" runat="server">
            <HeaderTemplate>
                <div class="pacientes-lista-header">
                    <span class="col-info">Documento</span>
                    <span class="col-info">Paciente</span>
                </div>
            </HeaderTemplate>
            
            <ItemTemplate>
                <div class="paciente-card">
                    <div class="col-info">
                        <span class="tipo-doc"><%# Eval("TipoDocumento") %></span> 
                        <span class="nro-doc"><%# Eval("NroDocumento") %></span>
                    </div>
                    <div class="col-info">
                        <span class="nombre-paciente"><%# Eval("NombreCompleto") %></span>
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                <asp:Label ID="lblSinPacientes" runat="server" Text="No hay pacientes en la sala de espera." 
                           Visible='<%# rptPacientesEspera.Items.Count == 0 %>' CssClass="sin-pacientes-mensaje" />
            </FooterTemplate>
        </asp:Repeater>

    </div>

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
