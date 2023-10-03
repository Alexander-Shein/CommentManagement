using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.BusinessRules.Commentor;

public class UserNameTooShortFailure : Failure
{
    private const string ErrorCode = "user_name_too_short";

    public int MinLength { get; }
    public int ActualLength { get; }

    public UserNameTooShortFailure(int actualLength) : base(
        ErrorCode,
        $"The length of user name must be {UserName.MinLength} characters or more. You entered {actualLength} characters.")
    {
        MinLength = UserName.MinLength;
        ActualLength = actualLength;
    }
}
