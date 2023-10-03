using CommentManagementService.Domain.Comments.BusinessRules.Message;
using EmpCore.Domain;

namespace CommentManagementService.Domain.Comments.ValueObjects;

public class Message : SingleValueObject<string>
{
    public const int MinLength = 2;
    public const int MaxLenght = 1000;
    private static readonly IEnumerable<string> BlacklistedWords = new List<string> { "Word1", "Word2", "Word3" };

    private Message(string value) : base(value) { }

    public static Result<Message> Create(string message)
    {
        message = message?.Trim();
        
        if (string.IsNullOrWhiteSpace(message)) return EmptyMessageFailure.Instance;
        if (message.Length > MaxLenght) return new MessageMaxLengthExceededFailure(message.Length);
        if (message.Length < MinLength) return new MessageTooShortFailure(message.Length);

        message = HideBlacklistedWords(message);
        
        return new Message(message);
    }

    private static string HideBlacklistedWords(string message)
    {
        foreach (var blackListedWord in BlacklistedWords)
        {
            message = message.Replace(blackListedWord, "***", StringComparison.OrdinalIgnoreCase);
        }

        return message;
    }
}