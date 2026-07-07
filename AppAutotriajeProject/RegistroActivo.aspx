<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finalizacion.aspx.cs" Inherits="AppAutotriajeProject.RegistroActivo" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registro activo</title>
    <link rel="stylesheet" href="~/Content/css/finales.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
</head>
<body>
    <form id="formf" runat="server" class="pantalla-completa">
        <div class="container">
            <div class="circulo-icono">
                <i class="fa-solid fa-check"></i>
            </div>

            <h1>Usted ya tiene un registro activo</h1>
            <p>
                <span>Su información ha sido enviada al personal médico (GHIPS).</span><br /><br />
                <b>Por favor, tome asiento en la sala de espera.<br />
                Será llamado en breve.</b>
            </p>
            
            <asp:LinkButton ID="btnTerminar" runat="server" CssClass="btn" OnClick="btnTerminar_Click">
                Terminar
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
