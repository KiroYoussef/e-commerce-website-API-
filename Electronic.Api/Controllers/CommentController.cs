using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Reporsitories;
using Electronic.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electronic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // private readonly context contexttDB;
        private readonly ICommentService comments;
        private readonly IUserService Users;
        private readonly ContextDB context;

        public CommentController(ICommentService _comments, ContextDB _contextDB, IUserService _User)
        {
            this.comments = _comments;
            this.context = _contextDB;
            this.Users = _User;
        }

        [HttpGet]
        [Route("/NewComment/{UserId}/{ProductId}/{comment}")]
        public IActionResult AddNew([FromRoute] CommentDTO NewComment)
        {
            comments.AddNew(NewComment);

            return Ok( "data saved");
        }

        [HttpGet]
        [Route("/GetAllCommentsProduct/{ProId}")]

        public IActionResult GetAllCommentsProduct(int ProId)
        {
            var AllComment = comments.GetAllCommentsProduct(ProId);

            List<UserCommentDTO> UserComments = comments.getCommentsWithUserData(AllComment);
            return Ok(UserComments);
        }

        [HttpGet]
        [Route("/GetAllComments")]

        public IActionResult getALL()
        {
            IEnumerable<Comment> AllComment = comments.getALL();
            List<UserCommentDTO> UserComments = comments.getCommentsWithUserData(AllComment);
            return Ok(UserComments);
        }

        [HttpDelete]
        [Route("/DeleteComment/{commentID}")]
        public IActionResult Delete(int commentID)
        {
            comments.delete(commentID);
            return Ok("deleted");
        }
       

    }
}
