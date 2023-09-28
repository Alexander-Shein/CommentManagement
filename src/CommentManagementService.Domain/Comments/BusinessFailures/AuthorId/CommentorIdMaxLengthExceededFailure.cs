using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.AuthorId;

public class CommentorIdMaxLengthExceededFailure : Failure
{
    private const string ErrorCode = "commentor_id_max_length_exceeded";

    public int MaxLength { get; }
    public int ActualLength { get; }

    public CommentorIdMaxLengthExceededFailure(int actualLength) : base(
        ErrorCode,
        $"The length of commentor id must be {ValueObjects.CommentorId.MaxLength} characters or fewer. You entered {actualLength} characters.")
    {
        MaxLength = ValueObjects.CommentorId.MaxLength;
        ActualLength = actualLength;
    }
}