using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Attributes;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using DirectoryService.Api.Core.Domain.Concrete.ResponseDTO;
using DirectoryService.Api.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DirectoryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommunicationController : ControllerBase
    {
        private IUserCommunicationRepository userCommunicationRepository;
        private IUserRepository userRepository;
        public UserCommunicationController(IUserCommunicationRepository userCommunicationRepository, IUserRepository userRepository)
        {
            this.userCommunicationRepository = userCommunicationRepository;
            this.userRepository = userRepository;
        }

        [Route("AddUserCommunication")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AddUserCommunicationResponseDTO), (int)HttpStatusCode.OK)]
        [ValidateEnum(typeof(CommunicationTypeEnum))]
        [HttpPost]
        public async Task<ActionResult> AddUserCommunication([FromBody] AddUserCommunicationRequestDTO request)
        {
            try
            {
                if(!userRepository.CheckUser(request.UserInfoId))
                    return NotFound("User Not Found.");

                var userCommunication = await userCommunicationRepository.AddUserCommunicationAsync(request);

                var response = new AddUserCommunicationResponseDTO()
                {
                    Id = userCommunication.Id,
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteUserCommunication(Guid id)
        {
            if (!userCommunicationRepository.CheckUserCommunication(id))
                return NotFound("User Not Found.");

            await userCommunicationRepository.DeleteUserCommunicationAsync(id);
            return Ok();
        }

    }
}
