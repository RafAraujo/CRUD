<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="WebForm.Editar" %>

<!DOCTYPE html>

<style>
    .required.form-label:after {
        content: " *";
        color: darkred;
    }
</style>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRUD</title>
    <link rel="stylesheet" href="Content/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="Content/bootstrap-icons/bootstrap-icons.min.css" type="text/css" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="row mt-3 mb-3">
                <asp:panel ID="PnlErros" runat="server" CssClass="ErrorSummary">
                    <asp:ValidationSummary id="ValidationSummary" runat="server" HeaderText="Erros:"></asp:ValidationSummary>
                </asp:panel>
            </div>
            <div class="row mt-3 mb-3">
                <div class="col">
                    <h4>Cliente</h4>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <asp:TextBox ID="TxtId" runat="server" class="form-control" Visible="false" />
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="TxtCpf" class="form-label required">CPF</label>
                        <asp:TextBox ID="TxtCpf" runat="server" class="form-control" required="required" MaxLength="14" />
                    </div>
                </div>
                <div class="col-9">
                    <div class="form-group">
                        <label for="TxtNome" class="form-label required">Nome</label>
                        <asp:TextBox ID="TxtNome" runat="server" class="form-control" required="required" MaxLength="100" />
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="TxtRg" class="form-label">RG</label>
                        <asp:TextBox ID="TxtRg" runat="server" class="form-control" MaxLength="12" />
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="TxtDataExpedicao" class="form-label">Data Expedição</label>
                        <asp:TextBox runat="server" ID="TxtDataExpedicao" class="form-control" />
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="TxtOrgaoExpedicao" class="form-label">Órgão Expedição</label>
                        <asp:TextBox ID="TxtOrgaoExpedicao" runat="server" class="form-control" MaxLength="100" />
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="TxtUfExpedicao" class="form-label">UF Expedição</label>
                        <asp:DropDownList ID="DdlUfExpedicao" runat="server" class="form-select"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="TxtDataNascimento" class="form-label required">Data de Nascimento</label>
                        <asp:TextBox ID="TxtDataNascimento" runat="server" type="date" class="form-control" required="required" />
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="DdlSexo" class="form-label required">Sexo</label>
                        <asp:DropDownList ID="DdlSexo" runat="server" class="form-select" required="required"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="DdlEstadoCivil" class="form-label required">Estado Civil</label>
                        <asp:DropDownList ID="DdlEstadoCivil" runat="server" class="form-select" required="required"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <h4>Endereço</h4>
                </div>
            </div>
            <asp:HiddenField ID="HidEnderecoId" runat="server" />
            <div class="row mb-3">
                <div class="col-2">
                    <div class="form-group">
                        <label for="TxtCep" class="form-label required">CEP</label>
                        <div class="input-group">
                            <asp:TextBox ID="TxtCep" runat="server" class="form-control" required="required" MaxLength="9" />
                            <button type="button" class="btn btn-secondary" title="Pesquisar"><span class="bi-search"></span></button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="TxtLogradouro" class="form-label required">Logradouro</label>
                        <asp:TextBox ID="TxtLogradouro" runat="server" class="form-control" required="required" MaxLength="100" />

                    </div>
                </div>
                <div class="col-2 col-xl-1">
                    <div class="form-group">
                        <label for="TxtNumero" class="form-label required">Número</label>
                        <asp:TextBox ID="TxtNumero" runat="server" class="form-control" required="required" MaxLength="10" />
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="TxtComplemento" class="form-label">Complemento</label>
                        <asp:TextBox ID="TxtComplemento" runat="server" class="form-control" MaxLength="100" />
                    </div>
                </div>
                <div class="col-2 col-xl-3">
                    <div class="form-group">
                        <label for="TxtBairro" class="form-label required">Bairro</label>
                        <asp:TextBox ID="TxtBairro" runat="server" class="form-control" required="required" MaxLength="100" />
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="TxtCidade" class="form-label required">Cidade</label>
                        <asp:TextBox ID="TxtCidade" runat="server" class="form-control" required="required" MaxLength="100" />
                    </div>
                </div>
                <div class="col-1">
                    <div class="form-group">
                        <label for="DdlUf" class="form-label required">UF</label>
                        <asp:DropDownList ID="DdlUf" runat="server" class="form-select" required="required"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col text-center">
                    <asp:LinkButton ID="BtnVoltar" runat="server" Text="Voltar" class="btn btn-secondary btn-lg" OnClick="BtnVoltar_Click" />
                    <asp:Button ID="BtnEditar" runat="server" Text="Salvar" class="btn btn-primary btn-lg" OnClick="BtnEditar_Click" />
                </div>
            </div>
            <div class="row mt-3 mb-3">
                <div class="col">
                    <asp:Panel ID="PnlSucesso" runat="server" Visible="False">
                        <div class="alert alert-success alert-dismissible fade show" role="alert">
                            <span>Registro salvo com sucesso</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Fechar"></button>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PnlErro" runat="server" Visible="False">
                        <div class="alert alert-danger alert-dismissible fade show" role="alert">
                            <span>Erro ao processar a requisição</span>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Fechar"></button>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </form>
    </div>
    <script src="Scripts\bootstrap.min.js"></script>
    <script src="Scripts\imask\imask.min.js"></script>
</body>
</html>

<script>
    window.addEventListener("DOMContentLoaded", (event) => {
        configElements();
    });

    function configElements() {
        IMask(document.getElementById("TxtCpf"), { mask: "000.000.000-00" });
        IMask(document.getElementById("TxtRg"), { mask: "00.000.000-*", prepareChar: str => str.toUpperCase() });
        configDate(document.getElementById("TxtDataExpedicao"));
        configDate(document.getElementById("TxtDataNascimento"));
        IMask(document.getElementById("TxtCep"), { mask: "00000-000" });
    }

    function configDate(element) {
        let minDate = new Date();
        let maxDate = new Date();

        minDate.setFullYear(minDate.getFullYear() - 150);

        element.setAttribute("type", "date");
        element.setAttribute("min", minDate.toISOString().split("T")[0]);
        element.setAttribute("max", maxDate.toISOString().split("T")[0]);
    }
</script>
