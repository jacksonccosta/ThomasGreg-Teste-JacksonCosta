using ThomasGregTest.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Shared/NotFound", "/NotFound");
});
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:BaseUrl").Value);
});
builder.Services.AddScoped<IClienteApiService, ClienteApiService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAutenticacaoUsuarioApiService, AutenticacaoUsuarioApiService>();
builder.Services.AddScoped<IUsuarioApiService, UsuarioApiService>();
builder.Services.AddScoped<IRequisicaoService, RequisicaoService>();
builder.Services.AddScoped<VerificarAutenticacaoAttribute>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();