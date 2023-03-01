﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using list_api.Models.DTOs;
using list_api.Repository.Interface;
using list_api.Security;
namespace list_api.Controllers {
	[ApiController]
	[AllowAnonymous]
	[Route("api[controller]")]
	public class AuthController : ControllerBase { // Constructing.
		private readonly IAuthRepository auth_repository;
		public AuthController(IAuthRepository auth_repository) {
			this.auth_repository = auth_repository;
		}
		[HttpPost("/register")]
		public IActionResult Register([FromBody] UserAuthDTO user_auth_dto) { // Responding with OK status after registering a user.
			if (ModelState.IsValid) {
				auth_repository.Register(user_auth_dto);
				return Ok();
			} else return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpPost("/login")]
		public IActionResult LogIn([FromBody] UserAuthDTO user_auth_dto) { // Responding with a token after logging into system as a user.
			if (ModelState.IsValid) return Ok(auth_repository.LogIn(user_auth_dto));
			return BadRequest(ModelState.Values.SelectMany(mse => mse.Errors).Select(me => me.ErrorMessage));
		}
		[HttpGet("{refresh_token}")]
		public IActionResult Refresh(string refresh_token) { // Refreshing a token.
			SecurityValidator refresh_token_validator = new SecurityValidator(refresh_token);
			if (refresh_token_validator.RefreshTokenValidator()) return Ok(auth_repository.Refresh(refresh_token));
			else return BadRequest(refresh_token_validator.ListMessage);
		}
	}
}