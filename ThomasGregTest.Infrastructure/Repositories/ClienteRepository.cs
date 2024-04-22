using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ThomasGregTest.Domain;

namespace ThomasGregTest.Infrastructure;

public class ClienteRepository(ApplicationDbContext context) : IClienteRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> Salvar(Cliente entity, CancellationToken cancellationToken)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[]
               {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Nome", entity.Nome),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@Logotipo", entity.Logotipo),
                new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
               };

            await _context.Database.ExecuteSqlRawAsync("EXEC Sp_SalvarCliente @Id, @Nome, @Email, @Logotipo, @RowCount OUTPUT", parameters, cancellationToken);
            return (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao salvar cliente: {ex.Message}");
            return 0;
        }
    }
    public async Task<ICliente?> ObterPorId(int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao tentar obter cliente: {ex.Message}");
            return null;
        }
    }
    public async Task<IEnumerable<ICliente>?> ListarClientes(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Clientes.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao listar os clientes: {ex.Message}");
            return null;
        }
    }
    public async Task<bool> ExcluirPorId(int id, CancellationToken cancellationToken)
    {
        try
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@RowCount", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC Sp_DeletaClientePorId @Id, @RowCount OUTPUT", parameters, cancellationToken);
            int rowCount = (int)parameters.Single(p => p.ParameterName == "@RowCount").Value;
            return rowCount > 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao excluir cliente: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> VerificaEmailCadastrado(string email, int id, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Clientes.AnyAsync(x => x.Id != id && x.Email == email, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro ao verificar e-mail cadastrado: {ex.Message}");
            return false;
        }        
    }
}
