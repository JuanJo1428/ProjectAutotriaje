<%@ Page Title="Preguntas de Seguimiento" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreguntasSeguimiento.aspx.cs" Inherits="AppAutotriajeProject.PreguntasSeguimiento" %>

<asp:Content ID="ContentPreguntasSeguimiento" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-triaje">
        <h1>Preguntas de seguimiento</h1>

        <asp:UpdatePanel ID="upPreguntas" runat="server">
            <ContentTemplate>
                
                <!-- Tarjeta contenedora de la pregunta activa -->
                <div class="card-pregunta" id="divCardPregunta" runat="server">
                    
                    <!-- Texto dinámico de la pregunta -->
                    <h3 class="pregunta-titulo">
                        <asp:Label ID="lblTextoPregunta" runat="server" Text="Cargando..." />
                    </h3>
                    
                    <!-- ESQUELETO 1: Pregunta Tipo Sí / No -->
                    <asp:Panel ID="pnlTipoSiNo" runat="server" Visible="false" CssClass="opciones-sino">
                        <asp:Button ID="btnSi" runat="server" Text="" CssClass="btn-opcion-sino btn-opcion-si" OnClick="btnOpcionSiNo_Click" UseSubmitBehavior="false" />
                        <asp:Button ID="btnNo" runat="server" Text="" CssClass="btn-opcion-sino btn-opcion-no" OnClick="btnOpcionSiNo_Click" UseSubmitBehavior="false" />
                    </asp:Panel>

                    <!-- ESQUELETO 2: Pregunta Tipo Lista -->
                    <asp:Panel
                        ID="pnlTipoLista"
                        runat="server"
                        Visible="false"
                        CssClass="opciones-lista">

                        <asp:Repeater
                            ID="rptOpciones"
                            runat="server"
                            OnItemCommand="rptOpciones_ItemCommand">

                            <ItemTemplate>

                                <asp:Button
                                    ID="btnOpcion"
                                    runat="server"
                                    Text='<%# Eval("Texto") %>'
                                    CssClass="opcion-card"
                                    CommandName="Seleccionar"
                                    CommandArgument='<%# Eval("IdOpcion") %>'
                                    UseSubmitBehavior="false" />

                            </ItemTemplate>

                        </asp:Repeater>

                    </asp:Panel>

                    <div id="loadingPregunta" class="loading-pregunta" style="display:none;">
                        <i class="fa-solid fa-spinner fa-spin"></i>
                        <span>Cargando siguiente pregunta...</span>
                    </div>

                </div>

                <!-- Mensaje para cuando terminen las preguntas -->
                <asp:Panel ID="pnlFinPreguntas" runat="server" Visible="false" CssClass="card-pregunta">
                    <p class="mensaje-final">¡Has respondido todas las preguntas de seguimiento!</p>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script src="/Scripts/js/PreguntasSeguimiento.js?v=4.0" defer></script>

</asp:Content>