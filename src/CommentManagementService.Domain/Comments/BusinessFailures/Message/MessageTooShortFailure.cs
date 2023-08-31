using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessFailures.Message;

public class MessageTooShortFailure : Failure
{
    private const string ErrorCode = "message_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public MessageTooShortFailure(int actualLength) : base(
        ErrorCode,
        $"The length of message must be {ValueObjects.Message.MinLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = ValueObjects.Message.MinLength;
        ActualLength = actualLength;
    }
}
