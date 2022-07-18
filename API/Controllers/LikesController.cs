using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userReposiory;
        private readonly ILikesRepository _likesRepository;

        public LikesController(IUserRepository userReposiory, ILikesRepository likesRepository)
        {
            _userReposiory = userReposiory;
            _likesRepository = likesRepository;
        }

        // Request for a user to like another user
        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _userReposiory.GetUserByUsernameAsync(username);
            var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

            if (likedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

            var appUserLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if (appUserLike != null) return BadRequest("You already liked this user");

            appUserLike = new AppUserLike
            {
                SourceUserId = sourceUserId,
                LikedUserId = likedUser.Id
            };

            sourceUser.LikedUsers.Add(appUserLike);

            if (await _userReposiory.SaveAllAsync()) return Ok();

            return BadRequest("Falied to like this user");
        }

        // Request to get a users likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
        {
            likesParams.UserId = User.GetUserId();
            var users = await _likesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
            return Ok(users);
        }
        
    }
}