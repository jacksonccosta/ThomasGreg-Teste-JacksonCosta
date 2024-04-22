using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ThomasGregTest.Application;

namespace ThomasGregTest.WebApp;

public class HomeController(ILogger<ClienteController> logger,
                            IAutenticacaoUsuarioApiService autenticacaoUsuarioApiService,
                            ITokenService tokenService,
                            IUsuarioApiService usuarioApiService) : BaseController()
{
    private readonly ILogger<ClienteController> _logger = logger;
    private readonly IAutenticacaoUsuarioApiService _autenticacaoUsuarioApiService = autenticacaoUsuarioApiService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUsuarioApiService _usuarioApiService = usuarioApiService;

    [ServiceFilter(typeof(VerificarAutenticacaoAttribute))]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [Route("login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var query = new AutenticacaoUsuarioQuery(username, password);
        var success = await _autenticacaoUsuarioApiService.AutenticacaoUsuario(query);
        if (success)
            return RedirectToAction("Index", "Home");
        ViewBag.ErrorMessage = "E-mail ou senha inválidos.";
        return View();
    }

    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        _tokenService.ExcluirCookies();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("obterToken")]
    public async Task<string> ObterToken()
    {
        return await _tokenService.ObterToken();
    }

    [HttpGet]
    [Route("Cadastro")]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    [Route("Cadastro")]
    public async Task<IActionResult> Cadastro(UsuarioCadastroModel model)
    {
        if (ModelState.IsValid)
        {
            var novo = new AdicionarUsuarioQuery(model.Nome, model.Email, model.Senha, true);
            var result = await _usuarioApiService.AdicionarUsuario(novo);
            if (result != null && result.Success)
            {
                var query = new AutenticacaoUsuarioQuery(model.Email, model.Senha);
                var success = await _autenticacaoUsuarioApiService.AutenticacaoUsuario(query);

                if (success)
                    return RedirectToAction("Index", "Home");
            }
            ViewBag.Erro = result;
        }
        return View(model);
    }
}
