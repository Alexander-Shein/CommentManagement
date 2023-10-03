using CommentManagementService.Domain.BlogPosts.BusinessRules;
using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using EmpCore.Domain;

namespace CommentManagementService.Domain.BlogPosts;

public class PublishedBlogPost : AggregateRoot<Guid>
{
    public static Result<PublishedBlogPost> Create(Guid id)
    {
        if (id == Guid.Empty) return EmptyBlogPostIdFailure.Instance;
        return new PublishedBlogPost{Id = id};
    }
    
    public Result<Comment> Comment(Commentor commentor, Message message)
    {
        Contracts.Require(commentor != null);
        Contracts.Require(message != null);
        
        var comment = new Comment(this, commentor, message);
        return Result.Ok(comment);
    }
}