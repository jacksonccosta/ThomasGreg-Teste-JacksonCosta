using Microsoft.EntityFrameworkCore;

namespace ThomasGregTest.Infrastructure;

public static class InitializeDB
{
    public static void Initialize(ApplicationDbContext contexto)
    {
        if (!contexto.Database.CanConnect())
        {
            contexto.Database.Migrate();

            var path = "./DB/";

            string scriptSql = File.ReadAllText($"{path}Script_Inicial.sql");
            contexto.Database.ExecuteSqlRaw(scriptSql);

            DirectoryInfo di = new($"{path}PROCEDURES");

            foreach (var item in di.GetFiles())
            {
                scriptSql = File.ReadAllText(item.FullName);
                if (!String.IsNullOrWhiteSpace(scriptSql))
                    contexto.Database.ExecuteSqlRaw(scriptSql);
            }
        }
    }
}
