<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualizar.aspx.cs" Inherits="WebForm.Visualizar" %>

<!DOCTYPE html>

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
                        <label for="LblCpf" class="form-label fw-bold">CPF: </label>
                        <asp:Label ID="LblCpf" runat="server" class="form-control form-label"></asp:Label>
                    </div>
                </div>
                <div class="col-9">
                    <div class="form-group">
                        <label for="LblNome" class="form-label fw-bold">Nome: </label>
                        <asp:Label ID="LblNome" runat="server" class="form-control form-label"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblRg" class="form-label fw-bold">RG: </label>
                        <asp:Label ID="LblRg" runat="server" class="form-control form-label"></asp:Label>
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblDataExpedicao" class="form-label fw-bold">Data Expedição: </label>
                        <asp:Label ID="LblDataExpedicao" runat="server" class="form-control form-label"></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="LblOrgaoExpedicao" class="form-label fw-bold">Órgão Expedição: </label>
                        <asp:Label ID="LblOrgaoExpedicao" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="LblUfExpedicao" class="form-label fw-bold">UF Expedição: </label>
                        <asp:Label ID="LblUfExpedicao" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblDataNascimento" class="form-label fw-bold">Data de Nascimento: </label>
                        <asp:Label ID="LblDataNascimento" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblSexo" class="form-label fw-bold">Sexo: </label>
                        <asp:Label ID="LblSexo" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblEstadoCivil" class="form-label fw-bold">Estado Civil: </label>
                        <asp:Label ID="LblEstadoCivil" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col">
                    <h4>Endereço</h4>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-2">
                    <div class="form-group">
                        <label for="LblCep" class="form-label fw-bold">CEP: </label>
                        <asp:Label ID="LblCep" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-3">
                    <div class="form-group">
                        <label for="LblLogradouro" class="form-label fw-bold">Logradouro: </label>
                        <asp:Label ID="LblLogradouro" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-2 col-xl-1">
                    <div class="form-group">
                        <label for="LblNumero" class="form-label fw-bold">Número: </label>
                        <asp:Label ID="LblNumero" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="LblComplemento" class="form-label fw-bold">Complemento: </label>
                        <asp:Label ID="LblComplemento" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-2 col-xl-3">
                    <div class="form-group">
                        <label for="LblBairro" class="form-label fw-bold">Bairro: </label>
                        <asp:Label ID="LblBairro" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-2">
                    <div class="form-group">
                        <label for="LblCidade" class="form-label fw-bold">Cidade: </label>
                        <asp:Label ID="LblCidade" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
                <div class="col-1">
                    <div class="form-group">
                        <label for="LblUf" class="form-label fw-bold">UF: </label>
                        <asp:Label ID="LblUf" runat="server" class="form-control form-label"></asp:Label>                        
                    </div>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col text-center">
                    <asp:LinkButton ID="BtnVoltar" runat="server" Text="Voltar" class="btn btn-secondary btn-lg" OnClick="BtnVoltar_Click" />
                </div>
            </div>
        </form>
    </div>
    <script src="Scripts\bootstrap.min.js"></script>
</body>
</html>
