using DirectoryService.Api.Core.Application.Repository;
using DirectoryService.Api.Core.Domain.Concrete;
using DirectoryService.Api.Core.Domain.Concrete.DomainObjects;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using DirectoryService.Api.Core.Domain.Concrete.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DirectoryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IUserCommunicationRepository userCommunicationRepository;

        public UserController(IUserRepository userRepository, IUserCommunicationRepository userCommunicationRepository)
        {
            this.userRepository = userRepository;
            this.userCommunicationRepository = userCommunicationRepository;
        }

        [HttpPost]
        [Route("AddUser")]
        [ProducesResponseType(typeof(AddUserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddUser([FromBody] AddUserRequestDTO request)
        {
            var addedUser = new UserInfo()
            {
                Name = request.Name,
                Surname = request.Surname,
                CompanyName = request.CompanyName,
            };

            addedUser = await userRepository.AddUserAsync(addedUser);

            var response = new AddUserResponseDTO()
            {
                Id = addedUser.Id,
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if(!userRepository.CheckUser(id))
                return NotFound("User Not Found.");

            await userRepository.DeleteUserAsync(id);
            var deletedUserCommunicationIds = userCommunicationRepository.GetUserCommunications(x => x.UserInfoId == id).Select(x => x.Id).ToList();
            foreach(var deletedUserCommunicationId in deletedUserCommunicationIds)
            {
                await userCommunicationRepository.DeleteUserCommunicationAsync(deletedUserCommunicationId);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserInfo>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsers()
        {
            var users = userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetUserDetail/{id}")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailObject>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUserDetail(Guid id)
        {
            var response = new UserDetailResponseDTO();
            if(!userRepository.CheckUser(id))
                return NotFound("User Not Found.");

            response.UserDetail = userRepository.GetUserDetail(id);

            return Ok(response);
        }
    }
}
