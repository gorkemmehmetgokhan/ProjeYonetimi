<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmProje.aspx.cs" Inherits="ProjeYonetim.frmProje" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="cphHead" runat="server">
    <link rel="stylesheet" href="../Content/css/to-do.css">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/css/bootstrap-select.min.css" />
</asp:Content>

<asp:Content ID="cntBody" ContentPlaceHolderID="cphBody" runat="server">
    <div class="container wide">

        <div runat="server" id="alertGorevSil" class="alert alert-danger d-flex align-items-center" style="display: none; z-index: 99; position: fixed; left: 40%; top: 50%;" role="alert">
            <div style="text-align: center;">
                <i class="fa fa-exclamation-triangle fa-3x"></i>
            </div>
            <div>
                <div style="margin-top: 7px;">
                    Görevi silmek istediğinizden emin misiniz?
                </div>
                <form method="post" id="frmGorevSil">
                    <input type="hidden" id="hidGorevSil" name="id_Gorev" />
                    <div style="margin-top: 10px; text-align: center;">
                        <button type="submit" name="btnGorevSil" class="btn btn-primary">Evet</button>
                        <button type="button" id="btnAlertKapat" class="btn btn-danger">Hayır</button>
                    </div>
                </form>
            </div>
        </div>

        <h3><i class="fa fa-angle-right"></i><%= myProje.ProjeAd + " - Görevler" %></h3>

        <form method="post" id="frmListeler">
            <div class="row mt mb" style="display: flex; align-items: center;">

                <% for (int i = 0; i < myProjeDetay.Listeler.Count; i++)
                    { %>
                <div id='<%= "liste" + myProjeDetay.Listeler[i].id_Liste %>' class="col-lg-3 col-md-4 col-sm-12 custom-scroll" style="overflow-y: scroll; max-height: 728px;">
                    <section class="task-panel tasks-widget">
                        <div class="panel-heading">
                            <div class="pull-left">
                                <h5><i class="fa fa-tasks"></i><%= myProjeDetay.Listeler[i].ListeAd %></h5>
                            </div>
                            <br />
                            <div class="panel-body">
                                <div class="task-content">
                                    <ul id="sortable<%= i + 1 %>" class="task-list">

                                        <% foreach (var gorev in myProjeDetay.Listeler[i].tbl_Gorev)
                                            { %>
                                        <li id='<%= "gorev" + gorev.id_Gorev %>' class="list-primary">
                                            <div>
                                                <% foreach (var etiket in gorev.tbl_Etiket)
                                                    { %>
                                                <span class="badge" <%= "style='background-color: #" + etiket.EtiketRenk + ";'" %>><%= etiket.EtiketAd %></span>
                                                <% } %>
                                                <input type="hidden" id='<%= "hidEtiket" + gorev.id_Gorev%>' value='<%= string.Join(",", gorev.tbl_Etiket.Select(e => e.EtiketAd).ToList()) %>' />
                                            </div>
                                            <br />
                                            <% if (gorev.GorevResim != null)
                                                { %>
                                            <img src="<%= gorev.GorevResim %>" style="width: 100%; height: 50%; margin-bottom: 7px;">
                                            <% } %>
                                            <div class="task-checkbox">
                                                <input type="checkbox" class="list-child" value="" />
                                            </div>
                                            <div class="task-title">
                                                <span class="task-title-sp gorev-ad" style="font-weight: 700;"><%= gorev.GorevAd %></span>
                                                <br />
                                                <span class="task-title-sp aciklama"><%= gorev.Aciklama %></span>

                                                <div style="margin-top: 7px;">
                                                    <% foreach (var gk in gorev.tbl_GorevKullanici)
                                                        { %>
                                                    <img src="<%= gk.tbl_Kullanici.Resim %>" class="img-circle" width="40" height="40">
                                                    <% } %>
                                                    <input type="hidden" id='<%= "hid" + gorev.id_Gorev%>' value='<%= string.Join(",", gorev.tbl_GorevKullanici.Select(gk => gk.id_Kullanici).ToList()) %>' />
                                                </div>

                                                <div style="margin-top: 7px;">
                                                    <% foreach (var dosya in gorev.tbl_EkDosya)
                                                        { %>
                                                    <a class="badge" style="background-color: #cc0ec2;" target="_blank" href='<%= dosya.EkDosyaURL %>'><%= dosya.EkDosyaAd %></a>
                                                    <% } %>
                                                </div>

                                                <div class="pull-left" style="margin-top: 7px;">
                                                    <span class="badge" style="background-color: darkred;"><i class="fa fa-exclamation-circle"></i><%= gorev.BitisTarihi.ToString("dd/MM/yyyy HH:mm:ss") %></span>
                                                </div>

                                                <% if (myKullanici.id_KullaniciTur == 1)
                                                    { %>
                                                <div class="pull-right hidden-phone" style="margin-top: 7px;">
                                                    <button type="button" data-toggle="modal" data-target="#modalGorevGuncelle" class="btn btn-primary btn-md fa fa-pencil"></button>
                                                    <button type="button" class="btn btn-danger btn-md fa fa-trash-o"></button>
                                                </div>
                                                <% } %>
                                            </div>
                                        </li>
                                        <hr />
                                        <% } %>
                                    </ul>
                                </div>
                                <% if (i == 0 && myKullanici.id_KullaniciTur == 1)
                                    { %>
                                <div class="add-task-row">
                                    <button type="button" data-toggle="modal" data-target="#modalGorevEkle" class="btn btn-success btn-sm pull-left">Yeni Görev Ekle</button>
                                </div>
                                <% } %>
                            </div>
                        </div>
                    </section>
                </div>

                <% if (i < 2)
                    { %>
                <form method="post">
                    <div class="col-md-1" style="text-align: center;">
                        <input type="hidden" id="hidGorevID<%= i + 1 %>" name="id_Gorev" />
                        <button type="submit" name="btnSagaTasi<%= i + 1 %>" class="btn-primary" style="width: 50px; height: 50px;"><i class="fa fa-arrow-right"></i></button>
                        <br />
                        <br />
                        <button type="submit" name="btnSolaTasi<%= i + 1 %>" class="btn-success" style="width: 50px; height: 50px;"><i class="fa fa-arrow-left"></i></button>
                    </div>
                </form>
                <% } %>

                <% } %>

                <div class="modal fade" id="modalGorevEkle" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Yeni Görev Ekle</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <form method="post" id="frmGorevEkle" enctype="multipart/form-data">
                                <div class="modal-body">
                                    <div class="form-group">
                                        <input type="hidden" name="id_Liste" />
                                        <input type="text" name="GorevAd" class="form-control" placeholder="Görev Adı" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <input type="text" name="Aciklama" class="form-control" placeholder="Açıklama" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group date" id="dtpBitisTarihi">
                                            <input type="text" name="BitisTarihi" class="form-control" placeholder="Bitiş Tarihi" required="required" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <label>Kapak Resmi</label>
                                            <input type="file" id="fuGorevResim" runat="server" />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <input type="text" name="EtiketAd" class="form-control" placeholder="Etiketler (Aralarına virgül koymak şartıyla birden fazla etiket ekleyebilirsiniz.)" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <select class="selectpicker" multiple title="Görev Ekibi" name="id_Kullanici" required="required">
                                            <% foreach (var kullanici in myProjeKullanicilar)
                                                {%>
                                            <option value='<%= kullanici.id_Kullanici %>'>
                                                <%= kullanici.tbl_Kullanici.AdSoyad %>
                                            </option>
                                            <% } %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <label>Ek Dosya</label>
                                            <input type="file" id="fuEkDosya" runat="server" />

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>Kapat</button>
                                    <button type="submit" id="btnGorevKaydet" name="btnGorevKaydet" class="btn btn-primary">Kaydet</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="modalGorevGuncelle" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Görev Güncelle</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <form method="post" id="frmGorevGuncelle" enctype="multipart/form-data">
                                <div class="modal-body">
                                    <div class="form-group">
                                        <input type="hidden" id="hidGorevGuncelle" name="id_Gorev" />
                                        <input type="text" name="GorevAd" class="form-control" placeholder="Görev Adı" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <input type="text" name="Aciklama" class="form-control" placeholder="Açıklama" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group date" id="dtpBitisTarihiGuncelle">
                                            <input type="text" name="BitisTarihi" class="form-control" placeholder="Bitiş Tarihi" required="required" />
                                            <span class="input-group-addon">
                                                <span class="glyphicon glyphicon-calendar"></span>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <label>Kapak Resmi</label>
                                            <input type="file" id="fileGorevResim" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <input type="text" name="EtiketAd" class="form-control" placeholder="Etiketler (Aralarına virgül koymak şartıyla birden fazla etiket ekleyebilirsiniz.)" required="required" />
                                    </div>
                                    <div class="form-group">
                                        <select class="selectpicker" multiple title="Görev Ekibi" name="id_Kullanici" required="required">
                                            <% foreach (var kullanici in myProjeKullanicilar)
                                                {%>
                                            <option value='<%= kullanici.id_Kullanici %>'>
                                                <%= kullanici.tbl_Kullanici.AdSoyad %>
                                            </option>
                                            <% } %>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <label>Ek Dosya</label>
                                            <input type="file" id="fileEkDosya" runat="server" />

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>Kapat</button>
                                    <button type="submit" id="btnGorevGuncelle" name="btnGorevGuncelle" class="btn btn-primary">Güncelle</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>

<asp:Content ID="cntBodyScript" ContentPlaceHolderID="cphBodyScript" runat="server">
    <!--script for this page-->
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="../Scripts/tasks.js" type="text/javascript"></script>
    <script src="../Scripts/moment-with-locales.js"></script>
    <script src="Scripts/moment-with-locales.min.js"></script>
    <script src="Scripts/moment-with-locales.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datetimepicker/4.17.47/js/bootstrap-datetimepicker.min.js"></script>

    <script src="Scripts/moment.min.js"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/js/bootstrap-select.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.18/js/i18n/defaults-tr_TR.min.js"></script>

    <script>
        $(function () {
            //console.log("Test");

            TaskList.initTaskWidget();

            $(".task-list").sortable();
            $(".task-list").disableSelection();

            //DateTimePicker'lar için TR yerelleştirilmesi yapılır. (Tarih formatı için)
            $('#dtpBitisTarihi').datetimepicker({
                locale: 'tr',           
            });

            $('#dtpBitisTarihiGuncelle').datetimepicker({
                locale: 'tr',   
            });
        });

        //Yeni Görev Ekle butonuna basıldığında görevin ekleneceği listenin ID'si daha sonra kullanılmak üzere hiddenField'a atılır.
        $(".add-task-row button").click(function () {
            var listeID = $(this).closest(".col-lg-3.col-md-4.col-sm-12").attr("id").substring(5);

            $('input[name="id_Liste"]').val(listeID);

            //console.log(listeID);
        });

        //Görev Silme butonuna tıklandığında silinecek görevin ID'si daha sonra kullanılmak üzere hiddenField'a atılır.
        $(".btn.btn-danger.btn-md.fa.fa-trash-o").click(function () {
            $("#cphBody_alertGorevSil").show(100);

            var gorevID = $(this).closest("li.list-primary").attr("id").substring(5);
            $("#hidGorevSil").val(gorevID);
        });

        //Görev Silme işlemi için çıkan uyarıyı kapatma butonuna basıldığında
        $("#btnAlertKapat").click(function () {
            $("#cphBody_alertGorevSil").hide(100);
        });

        //Seçilen görevleri Devam Eden Listesi'ne taşımak için butona basıldığında
        //İlgili görevlerin ID'leri daha sonra kullanılmak üzere hiddenField'a atılır.
        $('button[name="btnSagaTasi1"]').click(async function () {
            //console.log($("#sortable1 .list-primary.task-done"));
            var seciliGorevIDs = "";

            $("#sortable1 .list-primary.task-done").each(function () {
                seciliGorevIDs += $(this).attr("id").substring(5) + ",";
            });

            await $("#hidGorevID1").val(seciliGorevIDs.substring(0, seciliGorevIDs.length - 1));
            //console.log($("#hidGorevID1").val());
        });

        //Seçilen görevleri Yapılacaklar Listesi'ne taşımak için butona basıldığında
        //İlgili görevlerin ID'leri daha sonra kullanılmak üzere hiddenField'a atılır.
        $('button[name="btnSolaTasi1"]').click(async function () {
            //console.log($("#sortable2 .list-primary.task-done"));
            var seciliGorevIDs = "";

            $("#sortable2 .list-primary.task-done").each(function () {
                seciliGorevIDs += $(this).attr("id").substring(5) + ",";
            });

            await $("#hidGorevID1").val(seciliGorevIDs.substring(0, seciliGorevIDs.length - 1));
            //console.log($("#hidGorevID1").val());
        });

        //Seçilen görevleri Yapıldı Listesi'ne taşımak için butona basıldığında
        //İlgili görevlerin ID'leri daha sonra kullanılmak üzere hiddenField'a atılır.
        $('button[name="btnSagaTasi2"]').click(async function () {
            //console.log($("#sortable2 .list-primary.task-done"));
            var seciliGorevIDs = "";

            $("#sortable2 .list-primary.task-done").each(function () {
                seciliGorevIDs += $(this).attr("id").substring(5) + ",";
            });

            await $("#hidGorevID2").val(seciliGorevIDs.substring(0, seciliGorevIDs.length - 1));
            //console.log($("#hidGorevID2").val());
        });

        //Seçilen görevleri Devam Eden Listesi'ne taşımak için butona basıldığında
        //İlgili görevlerin ID'leri daha sonra kullanılmak üzere hiddenField'a atılır.
        $('button[name="btnSolaTasi2"]').click(async function () {
            //console.log($("#sortable3 .list-primary.task-done"));
            var seciliGorevIDs = "";

            $("#sortable3 .list-primary.task-done").each(function () {
                seciliGorevIDs += $(this).attr("id").substring(5) + ",";
            });

            await $("#hidGorevID2").val(seciliGorevIDs.substring(0, seciliGorevIDs.length - 1));
            //console.log($("#hidGorevID2").val());
        });

        //Görev Güncelleme İşlemi (Modal'daki form düzenlenir.)
        //İlgili alanlar hiddenField'lara atılır.
        $(".btn.btn-primary.btn-md.fa.fa-pencil").click(function () {
            var gorevID = $(this).closest(".list-primary").attr("id").substring(5);
            var gorevAd = $(this).closest(".list-primary").find(".gorev-ad").text();
            var gorevAciklama = $(this).closest(".list-primary").find(".aciklama").text();
            var bitisTarihi = $(this).closest(".list-primary").find(".pull-left .badge").text();
            var etiketIDs = $("#hidEtiket" + gorevID).val();
            var kullaniciIDs = $(this).closest(".task-title").find("input[type=hidden]").val();

            $("#hidGorevGuncelle").val(gorevID);
            $("#modalGorevGuncelle").find('input[name="GorevAd"]').val(gorevAd);
            $("#modalGorevGuncelle").find('input[name="Aciklama"]').val(gorevAciklama);
            $("#modalGorevGuncelle").find('input[name="BitisTarihi"]').val(bitisTarihi);
            $("#modalGorevGuncelle").find('input[name="EtiketAd"]').val(etiketIDs);

            var arrayKullanici = kullaniciIDs.split(',');
            $('.selectpicker').selectpicker('val', arrayKullanici);
            $('.selectpicker').selectpicker('refresh');

            //console.log(gorevID);
            //console.log(gorevAd);
            //console.log(gorevAciklama);
            //console.log(bitisTarihi);
            //console.log(etiketIDs);
            //console.log(kullaniciIDs);
        });
    </script>
</asp:Content>
