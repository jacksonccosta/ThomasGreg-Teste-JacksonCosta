using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class LogradouroRepository(ApplicationDbContext context) : ILogradouroRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> Salvar(Logradouro entity, CancellationToken cancellationToken)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[]
           {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@ClienteId", entity.ClienteId),
                new SqlParameter("@NomeRua", entity.Endereco),
                new SqlParameter("@Numero", entity.Numero),
                new SqlParameter("@Bairro", entity.Bairro),
                new SqlParameter("@Cidade", entity.Cidade),
                new SqlParameter("@Estado", entity.Estado),
                new SqlParameter("@Cep", entity.Cep),
                new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
           };

            await _context.Database.ExecuteSqlRawAsync("EXEC Sp_SalvarLogradouro @Id, @ClienteId, @Endereco, @Numero, @Bairro, @Cidade, @Estado, @Cep, @RowCount OUTPUT", parameters, cancellationToken);
            return (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao salvar logradouro: {ex.Message}");
            return 0;
        }
    }
    public async Task<ILogradouro?> ObterPorId(int id, int clienteId, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Logradouros.FirstOrDefaultAsync(x => x.Id == id && x.ClienteId == clienteId, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao tentar obter o logradouro: {ex.Message}");
            return null;
        }        
    }
    public async Task<bool> ExcluirPorId(int id, int clienteId, CancellationToken cancellationToken)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@ClienteId", clienteId),
                    new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC Sp_DeletaLogradouro @Id, @ClienteId, @RowCount OUTPUT", parameters, cancellationToken);
            int rowCount = (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
            return rowCount > 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao excluir logradouro: {ex.Message}");
            return false;
        }
    }
    public async Task<IEnumerable<ILogradouro>?> ListarLogradouros(int clienteId, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Logradouros.Where(x => x.ClienteId == clienteId).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao tentar listar o logradouro: {ex.Message}");
            return null;
        }
    }
}
