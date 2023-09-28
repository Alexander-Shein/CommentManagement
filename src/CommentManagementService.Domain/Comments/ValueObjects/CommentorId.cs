using CommentManagementService.Domain.Comments.BusinessFailures.AuthorId;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.ValueObjects;

public class CommentorId : SingleValueObject<string>
{
    public const int MaxLength = 128;
    
    private CommentorId(string value) : base(value) { }
    
    public static Result<CommentorId> Create(string commentorId)
    {
        commentorId = commentorId?.Trim();
        
        if (string.IsNullOrWhiteSpace(commentorId)) return EmptyCommentorIdFailure.Instance;
        if (commentorId.Length > MaxLength) return new CommentorIdMaxLengthExceededFailure(commentorId.Length);

        return new CommentorId(commentorId);
    }
}