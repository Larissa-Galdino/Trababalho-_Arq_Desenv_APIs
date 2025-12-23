using TrabalhoApi.Data;
using TrabalhoApi.Models;

namespace TrabalhoApi.Services
{
    public class EmpresaService
    {
        private readonly AppDbContext _context;

        public EmpresaService(AppDbContext context)
        {
            _context = context;
        }

        public List<Empresa> ListarTodas() => _context.Empresas.ToList();

        public Empresa? BuscarPorId(int id) => _context.Empresas.Find(id);

        public void Criar(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();
        }

        public void CriarEmLote(List<Empresa> empresas)
        {
            _context.Empresas.AddRange(empresas);
            _context.SaveChanges();
        }

        public void Atualizar(Empresa empresa)
        {
            _context.Empresas.Update(empresa);
            _context.SaveChanges();
        }

        public void Deletar(Empresa empresa)
        {
            _context.Empresas.Remove(empresa);
            _context.SaveChanges();
        }
    }
}
