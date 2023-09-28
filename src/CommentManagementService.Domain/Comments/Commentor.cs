using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments;

public class Commentor : Entity<CommentorId>
{
    public UserName UserName { get; private set; }

    public static Result<Commentor> Create(CommentorId commentorId, UserName userName)
    {
        Contracts.Require(commentorId != null);
        Contracts.Require(userName != null);

        var commentor = new Commentor
        {
            Id = commentorId,
            UserName = userName
        };

        return commentor;
    }
}