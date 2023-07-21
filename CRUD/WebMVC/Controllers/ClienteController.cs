using AutoMapper;
using Domain.Models;
using Domain.Validators;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebMVC.AutoMapperUtils;
using WebMVC.Controllers.Base;
using WebMVC.ViewModels;
using WebMVC.WebApi;

namespace WebMVC.Controllers
{
    public class ClienteController : ValidatableController
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Cliente> _validator;

        private const string WebApiErrorMessage = "Falha na comunicação. Verifique se o projeto WebAPI está em execução.";

        public ClienteController()
        {
            _mapper = MapperConfig.GetMapperConfiguration().CreateMapper();
            _validator = new ClienteValidator();
        }

        public async Task<ActionResult> Index()
        {
            await Task.WhenAll(CarregarViewBags(), CarregarListaClientes());
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            Validate(cliente, _validator);

            if (!ModelState.IsValid)
            {
                return View(clienteViewModel);
            }

            var apiResponse = await WebApiCommunicator.Post<Cliente, Cliente>("Cliente", cliente);
            ViewBag.Success = apiResponse.Succcess;

            await Task.WhenAll(CarregarViewBags(), CarregarListaClientes());

            return View(clienteViewModel);
        }

        private async Task CarregarViewBags()
        {
            var apiResponse = await WebApiCommunicator.Get<IEnumerable<Uf>>("Uf");
            var listaUf = apiResponse.Result;

            if (!apiResponse.Succcess)
            {
                ViewBag.WebApiErrorMessage = WebApiErrorMessage;
                listaUf = new List<Uf>();
            }

            var listaSexo = new List<SelectListItem>
            {
                new SelectListItem() { Value = "F", Text = "Feminino" },
                new SelectListItem() { Value = "M", Text = "Masculino" }
            };

            var listaEstadoCivil = new List<SelectListItem>
            {
                new SelectListItem() { Value = "Casado(a)" },
                new SelectListItem() { Value = "Divorciado(a)" },
                new SelectListItem() { Value = "Solteiro(a)" },
                new SelectListItem() { Value = "Viúvo(a)" }
            };

            ViewBag.ListaSexo = new SelectList(listaSexo, "Value", "Text");
            ViewBag.ListaEstadoCivil = new SelectList(listaEstadoCivil, "Value", "Value");
            ViewBag.ListaUf = new SelectList(listaUf, "Id", "Sigla");
        }

        private async Task CarregarListaClientes()
        {
            var apiResponse = await WebApiCommunicator.Get<IEnumerable<Cliente>>("Cliente");
            var listaClientes = apiResponse.Result;

            if (!apiResponse.Succcess)
            {
                ViewBag.WebApiErrorMessage = WebApiErrorMessage;
                listaClientes = new List<Cliente>();
            }

            ViewBag.ListaClientes = _mapper.Map<IEnumerable<ClienteViewModel>>(listaClientes);
        }

        public async Task<ActionResult> Visualizar(int id)
        {
            var clienteViewModel = await CarregarCliente(id);
            return View(clienteViewModel);
        }

        private async Task<ClienteViewModel> CarregarCliente(int id)
        {
            var apiResponse = await WebApiCommunicator.Get<Cliente>($"Cliente/{id}");
            var cliente = apiResponse.Result;

            if (!apiResponse.Succcess)
            {
                ViewBag.WebApiErrorMessage = WebApiErrorMessage;
                cliente = new Cliente();
            }

            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);
            return clienteViewModel;
        }

        public async Task<ActionResult> Editar(int id)
        {
            await CarregarViewBags();
            var clienteViewModel = await CarregarCliente(id);
            return View(clienteViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Editar(ClienteViewModel clienteViewModel)
        {
            var cliente = _mapper.Map<Cliente>(clienteViewModel);

            Validate(cliente, _validator);

            if (!ModelState.IsValid)
            {
                return View(clienteViewModel);
            }

            var apiResponse = await WebApiCommunicator.Put<Cliente, Cliente>("Cliente", cliente);
            ViewBag.Success = apiResponse.Succcess;

            await Task.WhenAll(CarregarViewBags(), CarregarListaClientes());

            return View(clienteViewModel);
        }

        public async Task<ActionResult> Excluir(int id)
        {
            var apiResponse = await WebApiCommunicator.Delete($"Cliente/{id}");
            ViewBag.Success = apiResponse.Succcess;
            return RedirectToAction("Index");
        }
    }
}