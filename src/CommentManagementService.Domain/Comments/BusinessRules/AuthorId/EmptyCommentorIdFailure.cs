using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessRules.AuthorId;

public class EmptyCommentorIdFailure : Failure
{
    private const string ErrorCode = "empty_commentor_id";
    private static readonly string ErrorMessage = "Commentor Id must not be empty.";

    public static readonly EmptyCommentorIdFailure Instance = new();

    private EmptyCommentorIdFailure() : base(ErrorCode, ErrorMessage) { }
}