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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarSelects();
                ObterClientes();
            }

            PnlSucesso.Visible = false;
            PnlErro.Visible = false;
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

            var listaUf = ObterListaUf();

            PreencherListaUf(DdlUfExpedicao, listaUf);
            PreencherListaUf(DdlUf, listaUf);

        }

        private IEnumerable<Uf> ObterListaUf()
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

            return listaUf;
        }

        private void PreencherListaUf(DropDownList dropDownList, IEnumerable<Uf> listaUf)
        {
            dropDownList.DataSource = listaUf.OrderBy(uf => uf.Sigla);
            dropDownList.DataValueField = "Id";
            dropDownList.DataTextField = "Sigla";
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, string.Empty);
        }

        protected void BtnCadastrar_Click(object sender, EventArgs e)
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
                    client.AdicionarCliente(cliente);
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

            ObterClientes();
        }

        private void ObterClientes()
        {
            IEnumerable<Cliente> clientes = new List<Cliente>();

            try
            {
                ServiceHelper.Using<ClienteWcfService.ClienteWcfServiceClient>(client =>
                {
                    clientes = client.ObterClientes();
                });
            }
            catch (Exception ex)
            {
                Page.AdicionarErroValidacao(ex);
            }

            var dataSource = clientes.Select(c => new
            {
                c.Id,
                c.Cpf,
                c.Nome,
                c.Rg,
                DataNascimento = c.DataNascimento.ToShortDateString(),
                c.Sexo,
                c.EstadoCivil,
                Endereco = $"{c.Endereco.Logradouro}, {c.Endereco.Numero} - {c.Endereco.Cidade} - {c.Endereco.Uf.Sigla}"
            });

            GrdClientes.DataSource = dataSource;
            GrdClientes.DataBind();
        }

        protected void GrdClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

        protected void GrdClientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var linha = (GridViewRow)((WebControl)e.CommandSource).NamingContainer;
            var id = int.Parse(linha.Cells[1].Text);

            switch (e.CommandName)
            {
                case "Visualizar":
                    VisualizarCliente(id);
                    break;
                case "Editar":
                    EditarCliente(id);
                    break;
                case "Excluir":
                    ExcluirCliente(id);
                    break;
            }
        }

        private void VisualizarCliente(int id)
        {
            Response.Redirect($"Visualizar.aspx?id={id}");
        }

        private void EditarCliente(int id)
        {
            Response.Redirect($"Editar.aspx?id={id}");
        }

        private void ExcluirCliente(int id)
        {
            try
            {
                ServiceHelper.Using<ClienteWcfService.ClienteWcfServiceClient>(client =>
                {
                    client.ExcluirCliente(id);
                });
            }
            catch (Exception ex)
            {
                Page.AdicionarErroValidacao(ex);
            }

            ObterClientes();
        }
    }
}