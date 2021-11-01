<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmGiris.aspx.cs" Inherits="ProjeYonetim.frmGiris" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Proje Yönetim Sistemi - Giriş</title>

    <link href="Content/css/bootstrap.css" rel="stylesheet" />
    <link href="Content/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="Content/css/style.css" rel="stylesheet" />
    <link href="Content/css/style-responsive.css" rel="stylesheet" />

</head>
<body>
    <div id="login-page">
        <div class="container">

            <form runat="server" class="form-login">
                <h2 class="form-login-heading">Giriş Yap</h2>
                <div class="login-wrap">
                    <input type="text" name="Eposta" class="form-control" placeholder="E-Posta" required="required" autofocus />
                    <br />
                    <input type="password" name="Sifre" class="form-control" required="required" placeholder="Şifre" />                 
                    <br />
                    <label runat="server" id="lblGirisUyari" visible="false">
                        <span style="color: darkred;">E-posta veya şifre hatalı!</span>
                    </label>
                    <asp:Button runat="server" ID="btnGiris" OnClick="btnGiris_Click" class="btn btn-theme btn-block" Text="Giriş"></asp:Button>
                    <hr />

                    <div class="registration">
                        Hesabınız yok mu?<br />
                        <a class="" href="/uyeol">Üye Olun
                        </a>
                    </div>

                </div>

            </form>
        </div>
    </div>

    <script src="Scripts/jquery.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>

    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="Scripts/jquery.backstretch.min.js"></script>
    <script>
        $.backstretch("Images/img/1930875.jpg", { speed: 500 });
    </script>
    <%--</form>--%>
</body>
</html>
