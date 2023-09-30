﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.Common.Attributes;
using Defender.Common.Models;
using Defender.Common.DTOs;
using Defender.UserManagementService.Application.Modules.Users.Commands;
using Defender.UserManagementService.Application.Modules.Users.Queries;

namespace Defender.UserManagementService.WebApi.Controllers.V1;

public class UserController : BaseApiController
{
    public UserController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [Auth(Roles.Admin)]
    [HttpGet("get-by-id")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> GetByIdAsync([FromBody] GetUserByIdQuery query)
    {
        return await ProcessApiCallAsync<GetUserByIdQuery, UserDto>(query);
    }

    [Auth(Roles.Admin)]
    [HttpGet("get-by-login")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> GetByLoginAsync([FromBody] GetUserByLoginQuery query)
    {
        return await ProcessApiCallAsync<GetUserByLoginQuery, UserDto>(query);
    }

    [Auth(Roles.Admin)]
    [HttpPost("create")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> CreateWithCredentialsAsync([FromBody] CreateUserCommand createCommand)
    {
        return await ProcessApiCallAsync<CreateUserCommand, UserDto>(createCommand);
    }

}
