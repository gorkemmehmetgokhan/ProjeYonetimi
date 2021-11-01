<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAnasayfa.aspx.cs" Inherits="ProjeYonetim.frmAnasayfa" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="cphHead" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/css/bootstrap-select.min.css" />
</asp:Content>

<asp:Content ID="cntBody" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container wide">
        <div class="row">
            <button data-toggle="modal" data-target="#modalProjeEkle" class="btn-primary mt-3 <%= myKullanici.id_KullaniciTur == 2 ? "d-none" : "" %>" style="width: 15%; height: 40px; font-weight: 700; font-size: 14px;"><i class="fa fa-plus"></i>Proje Ekle</button>
        </div>
        <hr />
        <div class="row">

            <% foreach (var proje in myProjeler)
                { %>
            <div class="col-lg-3 col-md-6 col-sm-12 mb">
                <div id='<%= "proje" + proje.id_Proje %>' class="content-panel pn">
                    <div id="profile-01" <%= "style='background: url(" + proje.ProjeResim + ") no-repeat center top; background-size: cover;'" %>>
                        <a href="/proje/<%= proje.id_Proje %>-<%= proje.ProjeAd %>" style="text-decoration: none;">
                            <h3><%= proje.ProjeAd %></h3>
                        </a>
                    </div>
                    <div class="profile-01 centered">
                        <p data-toggle="modal" data-target="#modalProjeGuncelle"><i class="fa fa-edit"></i>Düzenle</p>
                    </div>
                    <div>
                        <h6><%=proje.Aciklama %></h6>
                    </div>
                    <input type="hidden" id='<%= "hid" + proje.id_Proje%>' value='<%= string.Join(",", proje.tbl_ProjeKullanici.Select(pk => pk.id_Kullanici).ToList()) %>' />
                </div>
            </div>
            <%  } %>
        </div>
    </div>

    <div class="modal fade" id="modalProjeEkle" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Yeni Proje Ekle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form method="post" id="frmProjeEkle" enctype="multipart/form-data">
                    <div class="modal-body">
                        <div class="form-group">
                            <input type="text" name="ProjeAd" class="form-control" placeholder="Proje Adı" required="required" />
                        </div>
                        <div class="form-group">
                            <input type="text" name="Aciklama" class="form-control" placeholder="Açıklama" />
                        </div>
                        <div class="form-group">
                            <div class="d-flex flex-row justify-content-around align-items-center">
                                <label>Kapak Resmi</label>
                                <input type="file" id="fuProjeResim" runat="server" />

                            </div>
                        </div>
                        <div class="form-group">
                            <select class="selectpicker" multiple title="Proje Ekibi" name="id_Kullanici">
                                <% foreach (var kullanici in myKullanicilar)
                                    {%>
                                <option value='<%= kullanici.id_Kullanici %>'>
                                    <%= kullanici.AdSoyad %>
                                </option>
                                <% } %>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>Kapat</button>
                        <button type="submit" id="btnProjeKaydet" name="btnProjeKaydet" class="btn btn-primary">Kaydet</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalProjeGuncelle" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Proje Güncelle</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form method="post" id="frmProjeGuncelle" enctype="multipart/form-data">
                    <div class="modal-body">
                        <div class="form-group">
                            <input type="hidden" name="id_Proje" />
                            <input type="text" name="ProjeAd" class="form-control" placeholder="Proje Adı" required="required" />
                        </div>
                        <div class="form-group">
                            <input type="text" name="Aciklama" class="form-control" placeholder="Açıklama" />
                        </div>
                        <div class="form-group">
                            <div class="d-flex flex-row justify-content-around align-items-center">
                                <label>Kapak Resmi</label>
                                <input type="file" id="fileProjeResim" runat="server" />

                            </div>
                        </div>
                        <div class="form-group">
                            <select class="selectpicker" multiple title="Proje Ekibi" name="id_Kullanici">
                                <% foreach (var kullanici in myKullanicilar)
                                    {%>
                                <option value='<%= kullanici.id_Kullanici %>'>
                                    <%= kullanici.AdSoyad %>
                                </option>
                                <% } %>
                            </select>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>Kapat</button>
                        <button type="submit" id="btnProjeGuncelle" name="btnProjeGuncelle" class="btn btn-primary">Güncelle</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="cntBodyScript" ContentPlaceHolderID="cphBodyScript" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="Scripts/umd/popper.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/js/bootstrap-select.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/js/i18n/defaults-tr_TR.min.js"></script>

    <script>
        $(".profile-01.centered p").click(function () {
            var projeID = $(this).closest(".content-panel.pn").attr("id").substring(5);
            var projeAd = $(this).closest(".content-panel.pn").find("h3").text();
            var projeAciklama = $(this).closest(".content-panel.pn").find("h6").text();
            var kullaniciIDs = $(this).closest(".content-panel.pn").find("input[type=hidden]").val();

            $('input[name="id_Proje"]').val(projeID);
            $('input[name="ProjeAd"]').val(projeAd);
            $('input[name="Aciklama"]').val(projeAciklama);
            var arrayKullanici = kullaniciIDs.split(',');
            $('.selectpicker').selectpicker('val', arrayKullanici);
            $('.selectpicker').selectpicker('refresh');

            //console.log(projeID);
            //console.log(projeAd);
            //console.log(projeAciklama);
            //console.log(kullaniciIDs);
        });
    </script>
</asp:Content>
