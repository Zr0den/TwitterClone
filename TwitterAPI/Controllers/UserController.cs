﻿using Database.Entities;
using Helpers;
using MessageClient;
using Microsoft.AspNetCore.Mvc;
using UserProfileService;


namespace TwitterAPI.Controllers
{
    internal static class MessageWaiter
    {
        public static async Task<T?>? WaitForMessage<T>(MessageClient<T> messageClient, string clientId, int timeout = 5000)
        {
            var tcs = new TaskCompletionSource<T?>();
            var cancellationTokenSource = new CancellationTokenSource(timeout);
            cancellationTokenSource.Token.Register(() => tcs.TrySetResult(default));

            using (
                var connection = messageClient.ListenUsingTopic<T>(message =>
                {
                    tcs.TrySetResult(message);
                }, "User" + clientId, clientId)
            )
            {
            }

            return await tcs.Task;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly MessageClient<UserCreateDto> _createUserMessage;
        private readonly MessageClient<UserProfileDto> _getUserMessage;

        public UserController(UserService userService, MessageClient<UserCreateDto> createUserMessage, MessageClient<UserProfileDto> getUserMessage)
        {
            _userService = userService;
            _createUserMessage = createUserMessage;
            _getUserMessage = getUserMessage;
        }
        [HttpPost]
        public async Task<ActionResult<string>> CreateUser(UserCreateDto userDto)
        {
            _createUserMessage.SendUsingTopic(new UserCreateDto
            {
                Name = userDto.Name,
                Email = userDto.Email, 
                Password = userDto.Password,
                UserTag = userDto.UserTag
            }, "newUser");

            
            var response = await MessageWaiter.WaitForMessage(_getUserMessage, userDto.UserTag)!;

            return response != null ? response.Email : "Order timed out.";
            //var user = new User
            //{
            //    Name = userDto.Name,
            //    UserTag = userDto.Username,
            //    Email = userDto.Email,
            //    Password = userDto.Password,
            //    // Password handling should ideally involve hashing
            //};

            //await _userService.AddUserAsync(user);

            //// Mapping the User entity to UserProfileDto
            //var result = new UserProfileDto
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    Username = user.UserTag,
            //    Email = user.Email
            //};

            //return CreatedAtAction(nameof(_userService.GetUserByIdAsync), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            user.Name = userDto.Name;
            user.UserTag = userDto.UserTag;
            user.Email = userDto.Email;

            await _userService.UpdateUserAsync(user);

            return NoContent();
        }
        // Get a user profile by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                UserTag = user.UserTag,
                Email = user.Email
            };

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> SearchUsers(string query)
        {
            var users = await _userService.SearchAsync(query);

            var result = users.Select(user => new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                UserTag = user.UserTag,
                Email = user.Email
            }).ToList();

            return Ok(result);
        }

    }
}
