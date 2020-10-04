using Application.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ClientsController : BaseController
    {
        /// <summary>
        /// Retrieve All Clients
        /// </summary>
        /// <returns>Action Result of Clients</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Clients
        /// </remarks>
        /// <response code="200">Returns list of all Clients</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientListDTO>>> GetClients()
        {
            var clientsFromRepo = await Repository.Client.GetAllClients();
            var clientsToReturn = Mapper.Map<ClientListDTO[]>(clientsFromRepo);
            return Ok(clientsToReturn);
        }

        /// <summary>
        /// Retrieve A Single Client
        /// </summary>
        /// <param name="code"></param>
        /// <returns>An Action Result of a Client</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Clients/MT409
        /// </remarks>
        /// <response code="200">Returns the found Client with Associated Matters</response>
        /// <response code="404">If no matching Client was found</response>
        [HttpGet("{code}", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientDetailDTO>> GetClient(string code)
        {
            var clientFromRepo = await Repository.Client.GetClientWithMatters(code);

            if (clientFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {code} does not exist"
                });
            }

            var clientToReturn = Mapper.Map<ClientDetailDTO>(clientFromRepo);
            return Ok(clientToReturn);
        }

        /// <summary>
        /// Create New Client
        /// </summary>
        /// <param name="clientDTO"></param>
        /// <returns>Action Result of a Client</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Clients
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew",
        ///       "code": "MT408"        
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created Client</response>
        /// <response code="400">If the Client Code has been registered</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientListDTO>> CreateClient([FromBody] CreateClientDTO clientDTO)
        {
            bool codeTaken = await Repository.Client.CodeExist(clientDTO.Code);

            if (codeTaken)
            {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"Client code {clientDTO.Code} is already in use"
                });
            }

            var clientToCreate = Mapper.Map<Client>(clientDTO);
            clientToCreate.RegistrationDate = DateTime.Now;

            Repository.Client.CreateClient(clientToCreate);
            await Repository.Commit();

            var clientToReturn = Mapper.Map<ClientListDTO>(clientToCreate);

            return CreatedAtRoute("GetClient", new { code = clientToReturn.Code }, clientToReturn);
        }

        /// <summary>
        /// Update A Client
        /// </summary>
        /// <param name="code"></param>
        /// <param name="clientDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Clients/MT409
        ///     {        
        ///       "firstName": "Mike",
        ///       "lastName": "Andrew"       
        ///     }
        /// </remarks>
        /// <response code="204">Returns nothing except a success status code</response>
        /// <response code="404">If no matching Client was found</response>
        [HttpPut("{code}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateClient(string code, [FromBody] UpdateClientDTO clientDTO)
        {
            var clientFromRepo = await Repository.Client.GetClient(code);

            if (clientFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {code} does not exist"
                });
            }

            Mapper.Map(clientDTO, clientFromRepo);
            Repository.Client.UpdateClient(clientFromRepo);
            await Repository.Commit();

            return NoContent();
        }

        /// <summary>
        /// Delete A Client
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Clients/MT409
        /// </remarks>
        /// <response code="204">Returns nothing except a success status code</response>
        /// <response code="404">If no matching Client was found</response>
        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(string code)
        {
            var clientFromRepo = await Repository.Client.GetClient(code);

            if (clientFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {code} does not exist"
                });
            }

            Repository.Client.DeleteClient(clientFromRepo);
            await Repository.Commit();

            return NoContent();
        }
    }
}
