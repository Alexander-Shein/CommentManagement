using CommentManagementService.Domain.Comments.BusinessFailures.Commentor;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.ValueObjects;

public class UserName : SingleValueObject<string>
{
    public const int MinLength = 2;
    public const int MaxLenght = 100;
    private const string NotAllowedCharacters = "\r\n";

    private UserName(string value) : base(value) { }

    public static Result<UserName> Create(string useName)
    {
        if (string.IsNullOrWhiteSpace(useName)) return EmptyUserNameFailure.Instance;
        useName = useName.Trim();

        if (useName.Length > MaxLenght) return new UserNameMaxLengthExceededFailure(useName.Length);
        if (useName.Length < MinLength) return new UserNameTooShortFailure(useName.Length);
        if (useName.Any(NotAllowedCharacters.Contains)) return NotAllowedUserNameCharactersFailure.Instance;

        return new UserName(useName);
    }
}