using System.Threading.Tasks;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Data
{
    //.NET core trabalha com injeção de dependencias
    //IRepository necessita de uma classe que faça a sua implementação
    public interface IRepository
    {
         //MÉTODOS GERAIS
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveChangesAsync();

         //ALUNO

         //Podemos retornar um aluno e seus dados somente
         //Ou podemos retornar um aluno e seu professor de referencia
         Task<Aluno[]> GetAllAlunosAsync (bool includeProfessor);
         //Vamos retornar um aluno passando o professor ID
         Task<Aluno[]> GetAlunoAsyncByProfessorId(int ProfessorId, bool includeProfessor);

         //Vamos retornar um aluno pelo seu ID pode ou nao retornar o professor que esta relacionado
         Task<Aluno> GetAlunoAsyncById(int AlunoId, bool includeProfessor);

         //PROFESSOR
         // bool includeAluno, pode ou nao retornar os alunos do profwessor em questão
         Task<Professor[]> GetAllProfessoresAsync (bool includeAluno);
         Task<Professor> GetProfessorAsyncById(int ProfessorId, bool includeAluno);
    }
}