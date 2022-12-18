using HiringManagerAPI.Models;
using HiringManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HiringManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        public PersonController()
        {

        }


        /// <summary>
        /// Retrieves all persons
        /// </summary>
        /// <response code="200">Persons found. Return all persons</response>
        /// <returns>All persons</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        [HttpGet]
        public ActionResult<List<Person>> GetAll()
        {
            return PersonService.GetAll();
        }

        /// <summary>
        /// Retrieves a specific person by unique id
        /// </summary>
        /// <param name="id" example="0">The person id</param>
        /// <response code="200">Person found</response>
        /// <response code="404">Person not found</response>
        /// <returns>A specific person by unique id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Person> Get(int id)
        {
            Person person = PersonService.Get(id);

            if(person == null)
            {
                return NotFound();
            }
            else
            {
                return person;
            }
        }

        /// <summary>
        /// Creates a Person.
        /// </summary>
        /// <param name="person"></param>
        /// <returns>A new created Person</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Person
        ///     {
        ///         "id": 0,
        ///         "name": "Jane",
        ///         "lastName": "Doe",
        ///         "email": "janedoe@gmail.com",
        ///         "phone": "123456789",
        ///         "address": "City Lorem ipsum",
        ///         "dateOfBirth": "2091-09-14T01:16:59.732Z",
        ///         "dateOfInterview": "2022-12-17T01:16:59.732Z",
        ///         "offerJob": true,
        ///         "remarks": "Lorem ipsum dolor sit amet consectetur."
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created person</response>
        /// <response code="400">If the person is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] Person person)
        {
            if (person == null)
            {
                return BadRequest();
            }
            else
            {
                PersonService.Add(person);
                return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
            }            
        }

        /// <summary>
        /// Update a person.
        /// </summary>
        /// <param name="id" example="0">The person id</param>
        /// <param name="person"></param>
        /// <returns>Updated person</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Person/0
        ///     {
        ///         "id": 0,
        ///         "name": "John",
        ///         "lastName": "Doe",
        ///         "email": "johnedoe@gmail.com",
        ///         "phone": "123456789",
        ///         "address": "City Lorem ipsum",
        ///         "dateOfBirth": "2091-09-14T01:16:59.732Z",
        ///         "dateOfInterview": "2022-12-17T01:16:59.732Z",
        ///         "offerJob": false,
        ///         "remarks": "Lorem ipsum dolor sit amet consectetur."
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Person updated. Returns nothing</response>
        /// <response code="204">Person updated. Returns nothing</response>
        /// <response code="404">Person not found</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, Person person)
        {
            if(person.Id < 0 || id < 0)
            {
                return BadRequest();
            }
            else
            {
                if(PersonService.Get(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    PersonService.Update(person);
                    return NoContent();
                }
            }
            
        }

        /// <summary>
        /// Delete a specific Person.
        /// </summary>
        /// <param name="id" example="0">The person id</param>
        /// <response code="204">Person deleted</response>
        /// <response code="404">Person not found</response>
        /// <response code="400">The person id is less than 0</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            else
            {
                if (PersonService.Get(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    PersonService.Delete(id);
                    return NoContent();
                }
            }

        }
    }
}
