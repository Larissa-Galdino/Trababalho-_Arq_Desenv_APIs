using TrabalhoApi.Data;
using TrabalhoApi.Models;

namespace TrabalhoApi.Services
{
    public class FuncionarioService
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public List<Funcionario> ListarTodos() => _context.Funcionarios.ToList();

        public Funcionario? BuscarPorId(int id) => _context.Funcionarios.Find(id);

        public void Criar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
        }

        public void CriarEmLote(List<Funcionario> funcionarios)
        {
            _context.Funcionarios.AddRange(funcionarios);
            _context.SaveChanges();
        }

        public void Atualizar(Funcionario funcionario)
        {
            _context.Funcionarios.Update(funcionario);
            _context.SaveChanges();
        }

        public void Deletar(Funcionario funcionario)
        {
            _context.Funcionarios.Remove(funcionario);
            _context.SaveChanges();
        }
    }
}
