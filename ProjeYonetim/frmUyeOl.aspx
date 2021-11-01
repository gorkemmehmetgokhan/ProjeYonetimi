<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUyeOl.aspx.cs" Inherits="ProjeYonetim.frmUyeOl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Proje Yönetim Sistemi - Giriş</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome/css/font-awesome.css" rel="stylesheet" />

    <link href="Content/css/style.css" rel="stylesheet" />
    <link href="Content/css/style-responsive.css" rel="stylesheet" />

</head>
<body>
    <div id="login-page">
        <div class="container">

            <form runat="server" class="form-login" enctype="multipart/form-data">
                <h2 class="form-login-heading">Üye Ol</h2>
                <div class="login-wrap">
                    <div class="d-flex flex-row justify-content-around">
                        <div>
                            <input type="radio" id="radioYonetici" name="radioKullaniciTur" value="1" />
                            <label for="radioBireysel">Proje Yöneticisi</label>
                        </div>
                        <div>
                            <input type="radio" id="radioEleman" name="radioKullaniciTur" value="2" checked />
                            <label for="radioKurumsal">Proje Elemanı</label>
                        </div>
                    </div>
                    <input type="text" name="Ad" class="form-control" placeholder="Ad" required="required" autofocus />
                    <br />
                    <input type="text" name="Soyad" class="form-control" placeholder="Soyad" required="required" />
                    <br />
                    <input type="email" name="Eposta" class="form-control" placeholder="E-Posta" required="required" />
                    <br />
                    <input type="password" name="Sifre" class="form-control" placeholder="Şifre" required="required" />
                    <br />
                    <div class="d-flex flex-row justify-content-between align-items-center">
                        <label>Foto</label>
                        <input type="file" id="fileProfilResmi" name="fileProfilResmi" accept="image/jpeg, image/jpg, image/png, image/gif" />
                    </div>

                    <asp:Label runat="server" ID="lblUyeOlUyari" Style="color: darkred;" Visible="false"></asp:Label>
                    <asp:Button runat="server" ID="btnUyeOl" OnClick="btnUyeOl_Click" class="btn btn-theme btn-block mt-2" Text="Üye Ol"></asp:Button>
                </div>
            </form>
        </div>
    </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.min.js"></script>

    <!-- You can use an image of whatever size. This script will stretch to fit in any screen size.-->
    <script type="text/javascript" src="Scripts/jquery.backstretch.min.js"></script>
    <script>
        $.backstretch("Images/img/5137801.jpg", { speed: 500 });
    </script>
    <%--</form>--%>
</body>
</html>
