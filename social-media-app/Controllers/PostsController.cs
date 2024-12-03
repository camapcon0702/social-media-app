using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using social_media_app.Data;
using social_media_app.Interfaces;
using social_media_app.Models;
using social_media_app.Response;
using System.Net;
using System.Runtime.CompilerServices;

namespace social_media_app.Controllers
{
    [Route("")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostsController(SocialMediaContext context, IPostRepository postRepository, IUserRepository userRepository)
        {
            _context = context;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpPost("api/posts/users")]
        [Authorize]
        public async Task<ActionResult<Post>> CreatNewPost([FromBody] Post post, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                jwt = jwt.Replace("Bearer ", string.Empty);
                var reqUser = await _userRepository.FindUserByJwt(jwt);
                var newPost = await _postRepository.CreatePost(post, reqUser.Id);
                return StatusCode(StatusCodes.Status202Accepted, newPost);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("api/posts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            return await _postRepository.GetAllPosts();
        }

        [HttpDelete("api/posts/{postId}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> DeletePost(int postId, [FromHeader(Name = "Authorization")] string jwt)
        {
            try
            {
                jwt = jwt.Replace("Bearer ", string.Empty);
                var reqUser = await _userRepository.FindUserByJwt(jwt);

                var message = await _postRepository.DeletePost(postId, reqUser.Id);

                ApiResponse apiResponse = new ApiResponse 
                {
                    message = message,
                    status = true
                };
                return Ok(apiResponse);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, null);
            }
        }

        [HttpGet("api/posts/{postId}")]
        [Authorize]
        public async Task<ActionResult<Post>> FindPostByIdHandler(int postId)
        {
            var post = await _postRepository.FindPostById(postId);
            return Accepted(post);
        }

        [HttpGet("api/posts/user/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Post>>> FindUsersPost(int userId)
        {
            var posts = await _postRepository.FindPostsByUserId(userId);
            return posts;
        }

    }
}
