<%@ Page Title="Bienvenido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppAutotriajeProject.Inicio" %>

<asp:Content ID="ContentWelcome" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="top-actions">
        <asp:Button
            ID="btnWaiting"
            runat="server"
            Text="Sala de Espera"
            CssClass="btn-waiting"
            OnClick="btnWaitingRoom_Click" />
    </div>

    <section class="welcome-container">
        <h1>Bienvenido a Urgencias</h1>
        
        <p class="welcome-instructions">
            Toque el botón inferior para iniciar su proceso de admisión de manera rápida y segura.
        </p>
        
        <asp:Button ID="btnStart" 
                    runat="server" 
                    Text="Iniciar Triage" 
                    CssClass="btn-start" 
                    OnClick="btnStart_Click" />
    </section>

</asp:Content>
