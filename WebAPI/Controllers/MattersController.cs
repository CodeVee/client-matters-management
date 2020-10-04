using Application.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/Clients/{clientCode}/[controller]")]
    public class MattersController : BaseController
    {
        /// <summary>
        /// Retrieve All Associated Matters For A Client
        /// </summary>
        /// <param name="clientCode"></param>
        /// <returns>Action Result of Matters</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Clients/MT409/Matters
        /// </remarks>
        /// <response code="200">Returns the List of Matters for a Client</response>
        /// <response code="404">If no matching Client was found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MatterListDTO>>> GetMattersForClient(string clientCode)
        {
            bool isValidClient = await Repository.Client.CodeExist(clientCode);

            if (!isValidClient)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {clientCode} does not exist"
                });
            }

            var mattersFromRepo = await Repository.Matter.GetAllMattersForClient(clientCode);
            var mattersToReturn = Mapper.Map<MatterListDTO[]>(mattersFromRepo);

            return Ok(mattersToReturn);
        }

        /// <summary>
        /// Retrieve A Single Matter For A Client
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="code"></param>
        /// <returns>Action Result of Matter</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET api/Clients/MT409/Matters/BZ209
        /// </remarks>
        /// <response code="200">Returns the found Matter for a Client</response>
        /// <response code="404">If no matching Client/Matter was found</response>
        [HttpGet("{code}", Name = "GetMatter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatterListDTO>> GetMatterForClient(string clientCode, string code)
        {
            bool isValidClient = await Repository.Client.CodeExist(clientCode);

            if (!isValidClient)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {clientCode} does not exist"
                });
            }

            var matterFromRepo = await Repository.Matter.GetMatterForClient(clientCode, code);

            if (matterFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Matter with code {code} does not exist"
                });
            }

            var matterToReturn = Mapper.Map<MatterListDTO>(matterFromRepo);
            return Ok(matterToReturn);
        }

        /// <summary>
        /// Create Matter For A Client
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="matterDTO"></param>
        /// <returns>An Action Result of Matter</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/Clients/MT409/Matters
        ///     {        
        ///       "title": "Dawn of a new Horizon",
        ///       "amount": 50,
        ///       "code": "BZ209"        
        ///     }
        /// </remarks>
        /// <response code="201">Returns the newly created Matter for a Client</response>
        /// <response code="400">If the Matter Code has been registered</response>
        /// <response code="404">If no matching Client was found</response>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatterListDTO>> CreateMatterForClient(string clientCode, CreateMatterDTO matterDTO)
        {
            bool isValidClient = await Repository.Client.CodeExist(clientCode);

            if (!isValidClient)
            {
                return NotFound();
            }

            bool codeTaken = await Repository.Matter.CodeExist(matterDTO.Code);

            if (codeTaken)
            {
                return BadRequest(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = $"Matter code {matterDTO.Code} is already in use"
                });
            }

            var matterToCreate = Mapper.Map<Matter>(matterDTO);
            matterToCreate.ClientCode = clientCode;

            Repository.Matter.CreateMatterForClient(matterToCreate);
            await Repository.Commit();

            var matterToReturn = Mapper.Map<MatterListDTO>(matterToCreate);
            return CreatedAtRoute("GetMatter", new { clientCode, code = matterToCreate.MatterCode }, matterToReturn);
        }

        /// <summary>
        /// Update Exisiting Matter For A Client
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="code"></param>
        /// <param name="matterDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/Clients/MT409/Matters/BZ209
        ///     {        
        ///       "title": "Dawn of the planet of Apes",
        ///       "amount": 20,        
        ///     }
        /// </remarks>
        /// <response code="204">Returns nothing except a success status code</response>
        /// <response code="404">If no matching Client/Matter was found</response>
        [HttpPut("{code}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMatterForClient(string clientCode, string code, UpdateMatterDTO matterDTO)
        {
            bool isValidClient = await Repository.Client.CodeExist(clientCode);

            if (!isValidClient)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {clientCode} does not exist"
                });
            }

            var matterFromRepo = await Repository.Matter.GetMatterForClient(clientCode, code);
            if (matterFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"The Client with code {clientCode} does not have a Matter with code {code}"
                });
            }

            Mapper.Map(matterDTO, matterFromRepo);
            Repository.Matter.UpdateMatterForClient(matterFromRepo);
            await Repository.Commit();

            return NoContent();
        }

        /// <summary>
        /// Delete Matter For A Client
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE api/Clients/MT409/Matters/BZ209
        /// </remarks>
        /// <response code="204">Returns nothing except a success status code</response>
        /// <response code="404">If no matching Client/Matter was found</response>
        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMatterForClient(string clientCode, string code)
        {
            bool isValidClient = await Repository.Client.CodeExist(clientCode);

            if (!isValidClient)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"Client with code {clientCode} does not exist"
                });
            }

            var matterFromRepo = await Repository.Matter.GetMatterForClient(clientCode, code);
            if (matterFromRepo is null)
            {
                return NotFound(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = $"The Client with code {clientCode} does not have a Matter with code {code}"
                });
            }

            Repository.Matter.DeleteMatterForClient(matterFromRepo);
            await Repository.Commit();

            return NoContent();
        }
    }
}
