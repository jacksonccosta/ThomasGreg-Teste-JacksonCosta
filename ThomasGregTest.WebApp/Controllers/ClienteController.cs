using Microsoft.AspNetCore.Mvc;
using ThomasGregTest.Application;
using ThomasGregTest.Domain;

namespace ThomasGregTest.WebApp;

[ServiceFilter(typeof(VerificarAutenticacaoAttribute))]
public class ClienteController(ILogger<ClienteController> logger,
   IClienteApiService clienteApiService) : BaseController()
{
    private readonly ILogger<ClienteController> _logger = logger;
    private readonly IClienteApiService _clienteApiService = clienteApiService;

    public async Task<IActionResult> Index()
    {
        var response = await _clienteApiService.ObterClientes();

        if (response != null && response.Success)
            return View(response.Data);

        _logger.LogError($"Erro ao obter clientes da API: {response.Data}");
        return View("Error");
    }
    
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return base.Erro404();

        var response = await _clienteApiService.ObterClientePorId(id.Value);
        if (response == null)
            return base.Erro404();

        if (response == null || !response.Success)
        {
            string mensagem = $"Erro ao obter clientes da API: {response?.Data}";
            _logger.LogError(message: mensagem);

            return View("Error");
        }

        return View(response.Data);
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Logotipo")] AdicionarClienteFormModel formModel)
    {
        var query = new AdicionarClienteQuery("", "", null);
        if (ModelState.IsValid)
        {
            var arquivo = await ConvertFileService.ConvertFileToBase64(formModel.Logotipo);
            query = new AdicionarClienteQuery(formModel.Nome, formModel.Email, new ArquivoLogotipo { NomeArquivo = formModel.Logotipo.FileName, Base64 = arquivo });

            var response = await _clienteApiService.AdicionarCliente(query);

            if (response.Success)
                return RedirectToAction(nameof(Index));

            ViewBag.Erro = response;
        }
        return View(query);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return base.Erro404();

        var response = await _clienteApiService.ObterClientePorId(id.Value);

        if (response == null)
            return base.Erro404();

        if (response == null || !response.Success)
        {
            _logger.LogError($"Erro ao obter clientes: {response?.Data}");

            return View("Error");
        }
        return View(response.Data);
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Logotipo")] AtualizarClienteFormModel formModel)
    {
        if (id != formModel.Id)
            return base.Erro404();

        if (ModelState.IsValid)
        {
            var arquivo = formModel.Logotipo != null ? await ConvertFileService.ConvertFileToBase64(formModel.Logotipo) : String.Empty;

            var logotipo = formModel.Logotipo != null ? new ArquivoLogotipo { NomeArquivo = formModel.Logotipo.FileName, Base64 = arquivo }
            : new ArquivoLogotipo { NomeArquivo = String.Empty, Base64 = String.Empty };

            var query = new AtualizarClienteQuery(formModel.Id, formModel.Nome, formModel.Email, logotipo);

            var atualizarResponse = await _clienteApiService.AtualizarCliente(query);
            if (atualizarResponse == null)
                return base.Erro404();

            if (atualizarResponse.Success)
                return RedirectToAction(nameof(Index));
            ViewBag.Erro = atualizarResponse;

        }

        var clientResponse = await _clienteApiService.ObterClientePorId(id);
        var response = new ClienteResponse { Nome = formModel.Nome, Email = formModel.Email, Logotipo = clientResponse.Data.Logotipo };
        
        return View(response);
    }
}