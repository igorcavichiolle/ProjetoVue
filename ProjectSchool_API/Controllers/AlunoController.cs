using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Controllers
{
    //Setando a rota do meu controller, quando colocamos [controller] ele ja entende que a rota será o nome do Controller
    //api/aluno/
    [Route("api/[controller]")]
    [ApiController]
    //Criando meu controller Aluno, herdando as caracretisticas de Controller 
    public class AlunoController : Controller
    {
        //Construtor do meu controller Aluno
        public IRepository _repo { get; }
        //Construtor do meu controller Aluno
        public AlunoController(IRepository repo)
        {
            // agora temos o _repo como uma interface
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //retorna todos os alunos com seus professores
                var result = await _repo.GetAllAlunosAsync(true);

                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }

        }

        //Essa rota recebe um parametro
        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> GetByAlunoId(int AlunoId)
        {
            try
            {
                //retorna aluno pelo seu ID junto com seu professor
                var result = await _repo.GetAlunoAsyncById(AlunoId, true);

                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }
        }

        //Essa rota recebe um parametro
        [HttpGet("ByProfessor/{ProfessorId}")]
        public async Task<IActionResult> GetByProfessorId(int ProfessorId)
        {
            try
            {
                //retorna apenas os alunso que seja de um determinado professor
                var result = await _repo.GetAlunoAsyncByProfessorId(ProfessorId, true);

                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }
        }

        //Trabalhamos de forma assincrona pois podemos ter varias pessoas ou aplicações consumindo minha API
        [HttpPost]
        public async Task<IActionResult> post(Aluno model)
        {
            //Nosso método recebe um Objeto Aluno no corpo da requisição
            try
            {
                //Neste momento com o Objeto que veio no corpo da requisição, fazemos o Add
                _repo.Add(model);

                //Caso meu model esteja sem nenhum problema, adicionamos ele 
                if(await _repo.SaveChangesAsync())
                {
                    //Caso seja criado meu aluno, ele nos retornara um Created com uma chamada para a rota que retorna os dados do meu novo aluno criado
                    //O Created alem de retornar que deu tudo certo, retorna o objeto que foi criado junto
                    return Created($"/api/aluno/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }

            return BadRequest();
        }

        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> put(int AlunoId, Aluno model)
        {
            try
            {
                // Procura se o aluno que veio na requisição realmente existe no banco 
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);

                //Caso nao encontrou o aluno
                if (aluno == null) return NotFound();

                //Neste momento com o Objeto que veio no corpo da requisição, fazemos o Update
                _repo.Update(model);

                //Caso meu model esteja sem nenhum problema, adicionamos ele 
                if(await _repo.SaveChangesAsync())
                {
                    //Atualzamos nosso aluno que retornara para a a tela 
                    //Com seus dados atualizados após o Update
                    //E com o seu professor
                    aluno = await _repo.GetAlunoAsyncById(AlunoId, true);

                    //Caso seja criado meu aluno, ele nos retornara um Created com uma chamada para a rota que retorna os dados do meu novo aluno criado
                    //O Created alem de retornar que deu tudo certo, retorna o objeto que foi criado junto
                    return Created($"/api/aluno/{model.Id}", aluno);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }

            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> delete(int AlunoId)
        {
            try
            {
                // Procura se o aluno que veio na requisição realmente existe no banco 
                var aluno = await _repo.GetAlunoAsyncById(AlunoId, false);

                //Caso nao encontrou o aluno
                if (aluno == null) return NotFound();

                //Neste momento com o aluno encontrado, realizamos a Exclusão no banco de dados
                _repo.Delete(aluno);

                //Caso meu model esteja sem nenhum problema, adicionamos ele 
                if(await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }
            
            return BadRequest();
        }
    }
}