<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finalizacion.aspx.cs" Inherits="AppAutotriajeProject.Finalizacion" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Registro Completado</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <style>
        :root {
            --color-primary: #093c71;
            --color-accent: #00bab3;
            --color-background: #f4f7f6;
            --color-surface: #ffffff;
            --color-text-muted: #707272;
        }

        * {
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }
        
        html {font-size: 18px;}
        body {font-family: system-ui, -apple-system, "Segoe UI", Roboto, Arial, sans-serif;}
        h1 {font-size: 3rem;}
        p {font-size: 1.5rem; line-height: 1.5;}
        
        .pantalla-completa {
            min-height: 100vh;
            background-color: var(--color-primary);
            display: flex;
            align-items: center;
            justify-content: center;
        }
        
        .container {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            gap: 2rem;
            width: 90%;
            max-width: 600px;
            margin: 0 auto;
            color: var(--color-surface);
            text-align: center;
        }

        .circulo-icono {
            display: flex;
            align-items: center;
            justify-content: center;
            width: 10rem;
            height: 10rem;
            border-radius: 50%;
            background-color: var(--color-accent);
            color: var(--color-surface);
            font-size: 6rem;
        }

        .btn {
            width: 100%;
            max-width: 400px;
            padding: 1.5rem 0;
            border-radius: 0.7rem;
            background-color: white;
            color: var(--color-primary);
            text-decoration: none;
            font-size: 2.2rem;
            font-weight: 600;
            transition: background-color 0.2s, transform 0.1s;
        }
    </style>
</head>
<body>
    <form id="formf" runat="server" class="pantalla-completa">
        <div class="container">
            <div class="circulo-icono">
                <i class="fa-solid fa-check"></i>
            </div>

            <h1>Registro Completado</h1>
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
