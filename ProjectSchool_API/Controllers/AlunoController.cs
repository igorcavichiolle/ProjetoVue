using Microsoft.AspNetCore.Mvc;

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
        public AlunoController()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        //Essa rota recebe um parametro
        [HttpGet("{AlunoId}")]
        public IActionResult Get(int AlunoId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult post()
        {
            return Ok();
        }

        [HttpPut("{AlunoId}")]
        public IActionResult put(int AlunoId)
        {
            return Ok();
        }

        [HttpDelete("{AlunoId}")]
        public IActionResult delete(int AlunoId)
        {
            return Ok();
        }
    }
}