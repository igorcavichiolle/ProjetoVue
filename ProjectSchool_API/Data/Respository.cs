using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{
    //Implementando nosso repositorio interface
    public class Respository : IRepository
    {
        //Inicializamos um contexto e recebemos ele como parametro no construtor
        public DataContext _context { get; }
        public Respository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        //Temos uma task que gera uma thread, falamos que  ele é um método assincrono
        //Vamos esperar  (await) o _contect verificar se tem algum item pora salvar de forma assincrona
        //Se ele for maior que 0 quer dizer que houve alteração, retornarei verdadeiro ou falso
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        //ALUNOS
        public async Task<Aluno[]> GetAllAlunosAsync(bool includeProfessor = false)
        {
            //Preparando minha query
            //Dentro do meu query, terei todos os meus alunos
            IQueryable<Aluno> query = _context.Alunos;

            //Caso seja necessario retornar o professor para o aluno
            if (includeProfessor)
            {
                //Para cada Aluno que eu retornar, vou retornar seu professor no objeto Professor
                query = query
                        .Include(a => a.Professor);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(a => a.Id);

            //Implementando a chamada do banco com a query que eu criei
            return await query.ToArrayAsync();
        }

        public async Task<Aluno[]> GetAlunoAsyncByProfessorId(int ProfessorId, bool includeProfessor)
        {
            //Preparando minha query
            //Dentro do meu query, terei todos os meus alunos
            IQueryable<Aluno> query = _context.Alunos;

            //Caso seja necessario retornar o professor para o aluno
            if (includeProfessor)
            {
                //Para cada Aluno que eu retornar, vou retornar seu professor no objeto Professor
                query = query
                        .Include(a => a.Professor);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.ProfessorId == ProfessorId);

            //Implementando a chamada do banco com a query que eu criei
            return await query.ToArrayAsync();
        }

        public async Task<Aluno> GetAlunoAsyncById(int AlunoId, bool includeProfessor)
        {
            //Preparando minha query
            //Dentro do meu query, terei todos os meus alunos
            IQueryable<Aluno> query = _context.Alunos;

            //Caso seja necessario retornar o professor para o aluno
            if (includeProfessor)
            {
                //Para cada Aluno que eu retornar, vou retornar seu professor no objeto Professor
                query = query
                        .Include(a => a.Professor);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(aluno => aluno.Id == AlunoId);

            //Implementando a chamada do banco com a query que eu criei
            return await query.FirstOrDefaultAsync();
        }

        //PROFESSORES
        public async Task<Professor[]> GetAllProfessoresAsync(bool includeAluno = false)
        {
             //Preparando minha query
            //Dentro do meu query, terei todos os meus alunos
            IQueryable<Professor> query = _context.Professores;

            //Caso seja necessario retornar o professor para o aluno
            if (includeAluno)
            {
                //Para cada Aluno que eu retornar, vou retornar seu professor no objeto Professor
                query = query
                        .Include(a => a.Alunos);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(a => a.Id);

            //Implementando a chamada do banco com a query que eu criei
            return await query.ToArrayAsync();
        }

        public async Task<Professor> GetProfessorAsyncById(int ProfessorId, bool includeAluno = false)
        {
            //Preparando minha query
            //Dentro do meu query, terei todos os meus Professores
            IQueryable<Professor> query = _context.Professores;

            //Caso seja necessario retornar o professor para o Professor
            if (includeAluno)
            {
                //Para cada Professor que eu retornar, vou retornar seu aluno no objeto Professor
                query = query
                        .Include(a => a.Alunos);
            }

            query = query
                    .AsNoTracking()
                    .OrderBy(a => a.Id)
                    .Where(Professor => Professor.Id == ProfessorId);

            //Implementando a chamada do banco com a query que eu criei
            return await query.FirstOrDefaultAsync();
        }
    }
}