<%@ Page Title="Sala de Espera" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaEspera.aspx.cs" Inherits="AppAutotriajeProject.SalaEspera" %>

<%@ Import Namespace="ProjectDto.Dtos.RegistroAtencionDtos" %>

<asp:Content ID="ContentSalaEspera" ContentPlaceHolderID="MainContent" runat="server">
    <div class="description-container">
        <h1>Sala de Espera</h1>
        <p>Seleccione un paciente para consultar el registro de su atención.</p>
    </div>

    <asp:UpdatePanel ID="upSalaEspera" runat="server">
        <ContentTemplate>
            
            <asp:Timer
                ID="tmActualizarSala"
                runat="server"
                Interval="15000"
                OnTick="tmActualizarSala_Tick" />

            <div class="sala-espera-container">
                <asp:Repeater ID="rptPacientesEspera" runat="server" OnItemCommand="rptPacientesEspera_ItemCommand">
                    <ItemTemplate>
                        <div class='<%# GetCssClassTarjeta(((SalaEsperaDto)Container.DataItem).NivelPrioridad, ((SalaEsperaDto)Container.DataItem).AutotriajeIniciado)%>'>
                            
                            <!-- Clic en toda la tarjeta si AutotriajeIniciado es true -->
                            <asp:Button ID="btnVerDetallesCard" runat="server" 
                                        CommandName="VerDetalles" 
                                        CommandArgument='<%# ((SalaEsperaDto)Container.DataItem).IdAtencion %>' 
                                        CssClass="btn-card-overlay" 
                                        Visible='<%# ((SalaEsperaDto)Container.DataItem).AutotriajeIniciado %>' />

                            <div class="paciente-card-content">

                                <!-- Ícono -->
                                <div class="icon-container">
                                    <i class='<%# GetClaseIconoFontAwesome((SalaEsperaDto)Container.DataItem) %>'></i>
                                </div>

                                <!-- Información -->
                                <div class="paciente-detalles">

                                    <div class="nombre-paciente">
                                        <%# ((SalaEsperaDto)Container.DataItem).NombreCompleto %>
                                    </div>

                                    <div class="info-secundaria">
                                        <%# ((SalaEsperaDto)Container.DataItem).TipoDocumento %>
                                        <%# ((SalaEsperaDto)Container.DataItem).NumeroDocumento %>
                                        •
                                        <%# ((SalaEsperaDto)Container.DataItem).EdadPaciente %> años
                                    </div>

                                </div>

                                <!-- Badge -->
                                <asp:Panel
                                    runat="server"
                                    CssClass="badge-prioridad"
                                    Visible='<%# MostrarPoblacionPriorizada(Container.DataItem as SalaEsperaDto) %>'>

                                    Población priorizada

                                </asp:Panel>

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
                            <h3>Detalle del Pre-triage</h3>
                        </div>
                        <div class="modal-body">

                            <div class="modal-resumen">

                                <div class="modal-campo">
                                    <span class="modal-label">
                                        <i class="fa-solid fa-notes-medical"></i>
                                        Motivo de consulta
                                    </span>
                                    <asp:Label ID="lblMotivoConsulta" runat="server" CssClass="modal-value" />
                                </div>

                                <div class="modal-fila">

                                    <div class="modal-campo">
                                        <span class="modal-label">
                                            <i class="fa-solid fa-stethoscope"></i>
                                            Síntoma predominante
                                        </span>
                                        <asp:Label ID="lblSintomaPredominante" runat="server" CssClass="modal-value" />
                                    </div>

                                    <div class="modal-campo">
                                        <span class="modal-label">
                                            <i class="fa-solid fa-clock"></i>
                                            Tiempo en sala
                                        </span>
                                        <asp:Label ID="lblTiempoEspera" runat="server" CssClass="modal-value" />
                                    </div>

                                </div>

                            </div>

                            <hr class="modal-divider" />

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

        <Triggers>
            <asp:AsyncPostBackTrigger
                ControlID="tmActualizarSala"
                EventName="Tick" />
        </Triggers>

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