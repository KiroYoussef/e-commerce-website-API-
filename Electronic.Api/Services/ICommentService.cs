using Electronic.Api.DTO;
using Electronic.Api.Model;

namespace Electronic.Api.Reporsitories
{
    public interface ICommentService
    {
        List<Comment> GetAllCommentsProduct(int ProId);
        void AddNew(CommentDTO NewComment);
        IEnumerable<Comment> getALL( );
        void delete( int commentID);

        public List<UserCommentDTO> getCommentsWithUserData(IEnumerable<Comment> AllComment);
        
    }
}
