using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThomasGregTest.WebApp;

public class VerificarAutenticacaoAttribute(IAutenticacaoUsuarioApiService autenticacaoUsuarioApiService) : ActionFilterAttribute
{
    private readonly IAutenticacaoUsuarioApiService _autenticacaoUsuarioApiService = autenticacaoUsuarioApiService;

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var usuarioLogado = await _autenticacaoUsuarioApiService.ObterUsuarioLogado();

        if (usuarioLogado == null)
            context.Result = new RedirectToActionResult("Login", "Home", null);
        else
            await next();
    }
}
