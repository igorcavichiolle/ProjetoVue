using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        public IRepository _repo { get; }
        public ProfessorController(IRepository repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //retorna todos os professores com seus alunos
                var result = await _repo.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }
        }

        //Essa rota recebe um parametro
        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> Get(int ProfessorId)
        {
            try
            {
                //retorna professor pelo seu ID junto com seus alunos
                var result = await _repo.GetProfessorAsyncById(ProfessorId, true);

                return Ok(result);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Professor model)
        {
            //Nosso método recebe um Objeto Aluno no corpo da requisição
            try
            {
                //Neste momento com o Objeto que veio no corpo da requisição, fazemos o Add
                _repo.Add(model);

                //Caso meu model esteja sem nenhum problema, adicionamos ele 
                if (await _repo.SaveChangesAsync())
                {
                    //Caso seja criado o professor, ele nos retornara um Created com uma chamada para a rota que retorna os dados do meu novo aluno professor
                    //O Created alem de retornar que deu tudo certo, retorna o objeto que foi criado junto
                    return Created($"/api/Professor/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }

            return BadRequest();
        }

        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> put(int ProfessorId, Professor model)
        {
            try
            {
                // Procura se o professor que veio na requisição realmente existe no banco 
                var professor = await _repo.GetProfessorAsyncById(ProfessorId, false);

                //Caso nao encontrou o professor
                if (professor == null) return NotFound();

                //Neste momento com o Objeto que veio no corpo da requisição, fazemos o Update
                _repo.Update(model);

                //Caso meu model esteja sem nenhum problema, adicionamos ele 
                if(await _repo.SaveChangesAsync())
                {
                    //Atualzamos nosso professor que retornara para a a tela 
                    //Com seus dados atualizados após o Update
                    //E com o seus alunos
                    professor = await _repo.GetProfessorAsyncById(ProfessorId, true);

                    //Caso seja criado o professor, ele nos retornara um Created com uma chamada para a rota que retorna os dados do meu novo professor criado
                    //O Created alem de retornar que deu tudo certo, retorna o objeto que foi criado junto
                    return Created($"/api/professor/{model.Id}", professor);
                }
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados FALHOU");
            }

            return BadRequest();
        }

        [HttpDelete("{ProfessorId}")]
        public async Task<IActionResult> delete(int ProfessorId)
        {
            try
            {
                // Procura se o professor que veio na requisição realmente existe no banco 
                var professor = await _repo.GetProfessorAsyncById(ProfessorId, false);

                //Caso nao encontrou o professor
                if (professor == null) return NotFound();

                //Neste momento com o professor encontrado, realizamos a Exclusão no banco de dados
                _repo.Delete(professor);

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