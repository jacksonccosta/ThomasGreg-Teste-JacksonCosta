using Microsoft.AspNetCore.Mvc;

namespace ThomasGregTest.WebApp;
public class BaseController : Controller
{
    public ViewResult Erro404()
    {
        return View("NotFound");
    }
}
