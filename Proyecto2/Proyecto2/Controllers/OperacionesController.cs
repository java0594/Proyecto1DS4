using Proyecto2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Proyecto2.Models; 



namespace Proyecto2.Controllers
{
    public class OperacionesController : ApiController
    {
        private OperacionesDAL dal = new OperacionesDAL();

        [HttpGet]
        [Route("api/operaciones/sumas")]
        public Reply GetSumas()
        {
            var lista = dal.GetOperacionesPorTipo(1); 
            return new Reply { result = 1, message = "Todas las sumas", data = lista };
        }

        [HttpGet]
        [Route("api/operaciones/restas")]
        public Reply GetRestas()
        {
            var lista = dal.GetOperacionesPorTipo(2); 
            return new Reply { result = 1, message = "Todas las restas", data = lista };
        }

        [HttpGet]
        [Route("api/operaciones/multiplicaciones")]
        public Reply GetMultiplicaciones()
        {
            var lista = dal.GetOperacionesPorTipo(3); 
            return new Reply { result = 1, message = "Todas las multiplicaciones", data = lista };
        }

        [HttpGet]
        [Route("api/operaciones/divisiones")]
        public Reply GetDivisiones()
        {
            var lista = dal.GetOperacionesPorTipo(4); 
            return new Reply { result = 1, message = "Todas las divisiones", data = lista };
        }

        [HttpGet]
        [Route("api/operaciones")]
        public Reply GetTodos()
        {
            var lista = dal.GetOperaciones();
            return new Reply { result = 1, message = "Todos los cálculos", data = lista };
        }
    }
}


