using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactLibrary;
using System.Web.Cors;
using System.Web.Http.Cors;


namespace WebApplication1.Controllers
{
    [EnableCors("*","*","*")]
    public class JamesController : ApiController
    {
        //private Roladex roladex;
        private RolData crud = new RolData();

        //public JamesController()
        //{
        //    roladex = new Roladex();
        //    crud = new RolData(roladex);

        //}

        //read
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            crud.Populate();
            var persons = crud.GetPeople();
            return persons;
        }

        //ADD Person
        [HttpPost]
        public IHttpActionResult Post([FromBody]Person p)
        {
            if (p != null)
            {
                // Make a call to CRUD Method to insert in to DB

                Roladex roladex = new Roladex();
                crud = new RolData(roladex);
                try
                {
                    crud.Populate();
                    roladex.Add(p);
                    crud.PersistDB();
                }
                catch (Exception)
                    { return BadRequest(); }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        // to do  Put

        // to do Delete

    }
}
