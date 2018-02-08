using Fatec.RD.Bussiness;
using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.Swagger.Annotations;

using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Fatec.RD.WebApi.Controllers
{
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        UsuarioNegocio _appUsuario;

        /// <summary>
        /// 
        /// </summary>
        public UsuarioController()
        {
            _appUsuario = new UsuarioNegocio();
        }
        /// <summary>
        /// Método que obtem um usuario....
        /// </summary>
        /// <param name="id">Id do Usuario</param>
        /// <returns></returns>
        /// <remarks>obtem um Usuario</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Usuario))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_appUsuario.SelecionarPorId(id));
        }


        /// <summary>
        /// Método que obtem uma lista de usuario....
        /// </summary>
        /// <returns></returns>
        /// <remarks>Obtem lista de Usuarios</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Usuario))]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_appUsuario.SelecionarTodos());
        }

        /// <summary>
        /// Método que obtem uma lista de usuario....
        /// </summary>
        /// <returns></returns>
        /// <remarks>Obtem lista de Usuarios</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Usuario))]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] UsuarioInput input)
        {
            var obj = _appUsuario.Atualizar(id,input);
            return Content(HttpStatusCode.Accepted, obj);
        }

        /// <summary>
        /// Método que insere um novo usuario
        /// </summary>
        /// <param name="input">Input de usuario</param>
        /// <remarks>Insere um novo usuário</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(TipoDespesa))]
        [HttpPost]
        public IHttpActionResult Post([FromBody] UsuarioInput input)
        {
            var obj = _appUsuario.Adicionar(input);
            return Created($"{Request?.RequestUri}/{obj.Id}", obj);
        }
        /// <summary>
        /// Método que exclui um usuario e suas despesas....
        /// </summary>
        /// <param name="id">Id do Usuario</param>
        /// <returns></returns>
        /// <remarks>Deleta um usuario</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Usuario))]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _appUsuario.Deletar(id);
            return Ok();
        }


 







    }
}