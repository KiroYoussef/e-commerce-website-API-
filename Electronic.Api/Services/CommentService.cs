using Electronic.Api.DTO;
using Electronic.Api.Model;
using Electronic.Api.Repository;
using Electronic.Api.Services;

namespace Electronic.Api.Reporsitories
{
    public class CommentService : ICommentService
    {
        private readonly ContextDB context;
        private readonly IUserService Users;

        private readonly IBaseRepository<Comment> _CommentRepositroy;

        public CommentService(IBaseRepository<Comment> repo, ContextDB _contextDB, IUserService _User)
        {
            context = _contextDB;
            _CommentRepositroy = repo;
            this.Users = _User;

        }



        public void AddNew(CommentDTO NewComment)
        {
            var commentRepo = new Comment()
            {
                UserID = NewComment.UserId,
                ProductID = NewComment.ProductId,
                comment = NewComment.comment

            };
            _CommentRepositroy.Insert(commentRepo);

        }

        public List<Comment> GetAllCommentsProduct(int ProId)
        {
            List<Comment> productComments = context.Comments.Where(m => m.ProductID == ProId).Take(10).OrderByDescending(p => p.CreateDate).ToList();
            return productComments;
        }

        public IEnumerable<Comment> getALL()
        {
            var allComment = _CommentRepositroy.GetAll();
            return allComment;
        }
        public void delete(int commentID)
        {
            var ENTITY = _CommentRepositroy.GetById(commentID);
            _CommentRepositroy.Delete(ENTITY);
        }
        public List<UserCommentDTO> getCommentsWithUserData(IEnumerable<Comment> AllComment)
        {

            List<UserCommentDTO> UserComments = new List<UserCommentDTO>();

            foreach (var comment in AllComment)
            {
                var userId = comment.UserID;
                var userName = Users.GetUserNameByID(userId);
                var Userimg = Users.GetUserPhotoByID(userId);
                UserComments.Add
                 (new UserCommentDTO
                 {
                     UserId = userId,
                     ProductId = comment.ProductID,
                     comment = comment.comment,
                     UserName = userName,
                     UserImg = "https://localhost:7096/img/Users/" + Userimg,
                     date = comment.CreateDate
                 }
                 );

            }
            return UserComments;
        }


    }
}
