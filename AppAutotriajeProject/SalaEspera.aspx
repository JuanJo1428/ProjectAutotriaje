<%@ Page Title="Sala de Espera" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaEspera.aspx.cs" Inherits="AppAutotriajeProject.SalaEspera" %>

<asp:Content ID="ContentSalaEspera" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Sala de Espera</h1>
        <p>Pacientes actualmente en espera de atención.</p>
    </div>

    <asp:UpdatePanel ID="upSalaEspera" runat="server">
        <ContentTemplate>
            <div class="sala-espera-container">
                <asp:Repeater ID="rptPacientesEspera" runat="server" OnItemCommand="rptPacientesEspera_ItemCommand">
                    <ItemTemplate>
                        <div class='<%# GetCssClassTarjeta(Eval("NivelPrioridad") != null ? Convert.ToInt32(Eval("NivelPrioridad")) : 4, (bool)Eval("ContinuoTriage")) %>'>
                            
                            <!-- Clic en toda la tarjeta si ContinuoTriage es true -->
                            <asp:Button ID="btnVerDetallesCard" runat="server" 
                                        CommandName="VerDetalles" 
                                        CommandArgument='<%# Eval("IdRegistro") %>' 
                                        CssClass="btn-card-overlay" 
                                        Visible='<%# (bool)Eval("ContinuoTriage") %>' />

                            <div class="paciente-card-content">
                                <div class="icon-container">
                                    <i class='<%# GetClaseIconoFontAwesome(Eval("Condicion") != null ? Eval("Condicion").ToString() : "") %>'></i>
                                </div>

                                <div class="paciente-detalles">
                                    <h3 class="nombre-paciente"><%# Eval("Paciente.NombreCompleto") %></h3>
                                    <div class="info-secundaria">
                                        <%# Eval("Paciente.TipoDocumento.Nombre") %> <%# Eval("Paciente.NroDocumento") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:Label ID="lblSinPacientes" runat="server" Text="No hay pacientes en la sala de espera." 
                                   Visible='<%# rptPacientesEspera.Items.Count == 0 %>' CssClass="sin-pacientes-mensaje" />
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <!-- MODAL DE RESPUESTAS -->
            <asp:Panel ID="pnlModalDetalles" runat="server" Visible="false">
                <div class="modal-overlay">
                    <div class="modal-container">
                        <div class="modal-header">
                            <h3>Respuestas del Pre-triage</h3>
                        </div>
                        <div class="modal-body">
                            <asp:Repeater ID="rptPreguntas" runat="server">
                                <ItemTemplate>
                                    <div class="pregunta-item">
                                        <div class="pregunta-item-texto"><%# Eval("Pregunta") %></div>
                                        <div class="pregunta-item-respuesta">R// <%# Eval("Respuesta") %></div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCerrarModal" runat="server" Text="Cerrar" CssClass="btn btn-next" OnClick="btnCerrarModal_Click" />
                        </div>
                    </div>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="flow-navigation" >
        <asp:Button
            ID="btnVolver"
            runat="server"
            Text="Volver"
            CssClass="btn btn-back"
            CausesValidation="false"
            OnClick="btnVolver_Click" />
    </div>
</asp:Content>