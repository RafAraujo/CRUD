using Domain.Models;
using Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WebForm.Validation;
using WebForm.WCF;

namespace WebForm
{
    public partial class Editar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TxtId.Text = Request.QueryString["id"];
                ConfigurarSelects();
                CarregarCliente();
            }

            PnlSucesso.Visible = false;
            PnlErro.Visible = false;
        }

        private void CarregarCliente()
        {
            var id = int.Parse(TxtId.Text);

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

            TxtCpf.Text = cliente.Cpf;
            TxtNome.Text = cliente.Nome;
            TxtRg.Text = cliente.Rg;
            TxtDataExpedicao.Text = cliente.DataExpedicao.HasValue ? cliente.DataExpedicao.Value.ToString("yyyy-MM-dd") : null;
            TxtOrgaoExpedicao.Text = cliente.OrgaoExpedicao;
            DdlUfExpedicao.SelectedValue = cliente.UfExpedicaoId.HasValue ? cliente.UfExpedicaoId.Value.ToString() : null;
            TxtDataNascimento.Text = cliente.DataNascimento.ToString("yyyy-MM-dd");
            DdlSexo.SelectedValue = cliente.Sexo;
            DdlEstadoCivil.SelectedValue = cliente.EstadoCivil;

            TxtCep.Text = cliente.Endereco.Cep;
            TxtLogradouro.Text = cliente.Endereco.Logradouro;
            TxtNumero.Text = cliente.Endereco.Numero;
            TxtComplemento.Text = cliente.Endereco.Complemento;
            TxtBairro.Text = cliente.Endereco.Bairro;
            TxtCidade.Text = cliente.Endereco.Cidade;
            DdlUf.SelectedValue = cliente.Endereco.UfId.ToString();
        }

        private void ConfigurarSelects()
        {
            DdlSexo.Items.Add(new ListItem());
            DdlSexo.Items.Add(new ListItem("Feminino", "F"));
            DdlSexo.Items.Add(new ListItem("Masculino", "M"));

            DdlEstadoCivil.Items.Add(string.Empty);
            DdlEstadoCivil.Items.Add("Casado(a)");
            DdlEstadoCivil.Items.Add("Divorciado(a)");
            DdlEstadoCivil.Items.Add("Solteiro(a)");
            DdlEstadoCivil.Items.Add("Viúvo(a)");

            PreencherListaUf(DdlUfExpedicao);
            PreencherListaUf(DdlUf);
        }

        private void PreencherListaUf(DropDownList dropDownList)
        {
            IEnumerable<Uf> listaUf = new List<Uf>();

            try
            {
                ServiceHelper.Using<UfWcfService.UfWcfServiceClient>(client =>
                {
                    listaUf = client.ObterListaUf();
                });
            }
            catch (Exception ex)
            {
                Page.AdicionarErroValidacao(ex);
            }

            dropDownList.DataSource = listaUf.OrderBy(uf => uf.Sigla);
            dropDownList.DataValueField = "Id";
            dropDownList.DataTextField = "Sigla";
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, string.Empty);
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            DateTime? dataExpedicao = null;
            if (DateTime.TryParse(TxtDataExpedicao.Text, out DateTime value))
            {
                dataExpedicao = value;
            }

            int? ufExpedicaoId = null;
            if (!string.IsNullOrEmpty(DdlUfExpedicao.SelectedValue))
            {
                ufExpedicaoId = int.Parse(DdlUfExpedicao.SelectedValue);
            }

            var cliente = new Cliente
            {
                Id = int.Parse(Request.QueryString["id"]),
                Cpf = TxtCpf.Text,
                Nome = TxtNome.Text,
                Rg = TxtRg.Text,
                DataExpedicao = dataExpedicao,
                OrgaoExpedicao = string.IsNullOrEmpty(TxtOrgaoExpedicao.Text) ? null : TxtOrgaoExpedicao.Text,
                UfExpedicaoId = ufExpedicaoId,
                DataNascimento = DateTime.Parse(TxtDataNascimento.Text),
                Sexo = DdlSexo.Text,
                EstadoCivil = DdlEstadoCivil.SelectedValue,
                Endereco = new Endereco
                {
                    Cep = TxtCep.Text,
                    Logradouro = TxtLogradouro.Text,
                    Numero = TxtNumero.Text,
                    Complemento = string.IsNullOrEmpty(TxtComplemento.Text) ? null : TxtComplemento.Text,
                    Bairro = TxtBairro.Text,
                    Cidade = TxtCidade.Text,
                    UfId = int.Parse(DdlUf.SelectedValue)
                }
            };

            if (!Page.Validar(new ClienteValidator(), cliente))
            {
                return;
            }

            try
            {
                ServiceHelper.Using<ClienteWcfService.ClienteWcfServiceClient>(client =>
                {
                    client.EditarCliente(cliente);
                });

                PnlSucesso.Visible = true;
                PnlErro.Visible = false;
            }
            catch (Exception ex)
            {
                PnlSucesso.Visible = false;
                PnlErro.Visible = true;

                Page.AdicionarErroValidacao(ex);
            }
        }
    }
}