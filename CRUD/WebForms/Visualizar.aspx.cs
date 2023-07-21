using Domain.Models;
using System;
using WebForm.Validation;
using WebForm.WCF;

namespace WebForm
{
    public partial class Visualizar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCliente();
            }
        }

        private void CarregarCliente()
        {
            var id = int.Parse(Request.QueryString["id"]);

            var cliente = new Cliente();

            try
            {
                ServiceHelper.Using<ClienteWcfService.ClienteWcfServiceClient>(client =>
                {
                    cliente = client.ObterCliente(id);
                });
            }
            catch (Exception ex)
            {
                Page.AdicionarErroValidacao(ex);
            }

            LblCpf.Text = cliente.Cpf;
            LblNome.Text = cliente.Nome;
            LblRg.Text = cliente.Rg;
            LblDataExpedicao.Text = cliente.DataExpedicao.HasValue ? cliente.DataExpedicao.Value.ToShortDateString() : "-";
            LblOrgaoExpedicao.Text = cliente.OrgaoExpedicao ?? "-";
            LblUfExpedicao.Text = cliente.UfExpedicaoId.HasValue ? cliente.UfExpedicao.Sigla : "-";
            LblDataNascimento.Text = cliente.DataNascimento.ToShortDateString();
            LblSexo.Text = cliente.Sexo == "F" ? "Feminino" : "Masculino";
            LblEstadoCivil.Text = cliente.EstadoCivil;

            LblCep.Text = cliente.Endereco.Cep;
            LblLogradouro.Text = cliente.Endereco.Logradouro;
            LblNumero.Text = cliente.Endereco.Numero;
            LblComplemento.Text = cliente.Endereco.Complemento ?? "-";
            LblBairro.Text = cliente.Endereco.Bairro;
            LblCidade.Text = cliente.Endereco.Cidade;
            LblUf.Text = cliente.Endereco.Uf.Sigla;
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}