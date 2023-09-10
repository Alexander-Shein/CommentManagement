using CommentManagementService.Domain.Comments;
using CommentManagementService.Domain.Comments.ValueObjects;
using CommentManagementService.Persistence.BlogPosts.DomainRepositories;
using CommentManagementService.Persistence.Comments.DomainRepositories;
using EmpCore.Application.Failures;
using EmpCore.Domain;
using MediatR;

namespace CommentManagementService.Application.Comments.Commands.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Result<long>>
{
    private readonly IPublishedBlogPostDomainRepository _publishedBlogPostDomainRepository;
    private readonly ICommentDomainRepository _commentDomainRepository;

    public AddCommentCommandHandler(
        IPublishedBlogPostDomainRepository publishedBlogPostDomainRepository,
        ICommentDomainRepository commentDomainRepository)
    {
        _commentDomainRepository = commentDomainRepository ?? throw new ArgumentNullException(nameof(commentDomainRepository));
        _publishedBlogPostDomainRepository = publishedBlogPostDomainRepository ?? throw new ArgumentNullException(nameof(publishedBlogPostDomainRepository));
    }

    public async Task<Result<long>> Handle(AddCommentCommand command, CancellationToken ct)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var commentor = BuildCommentor(command.CommentorId, command.CommentorUserName);
        var message = Message.Create(command.Message);

        var result = Result.Combine(commentor, message);
        if (result.IsFailure) return Result.Fail<long>(result.Failures);

        var blogPost = await _publishedBlogPostDomainRepository.GetByIdAsync(command.PublishedBlogPostId).ConfigureAwait(false);
        if (blogPost == null) return ResourceNotFoundFailure.Instance;

        var comment = blogPost.Comment(commentor, message);
        if (comment.IsFailure) return Result.Fail<long>(comment.Failures);

        _commentDomainRepository.Save(comment);
        
        return Result.Ok(comment.Value.Id);
    }

    private static Result<Commentor> BuildCommentor(string commentorId, string userName)
    {
        var userNameResult = UserName.Create(userName);
        if (userNameResult.IsFailure) return Result.Fail<Commentor>(userNameResult.Failures);
        
        var commentor = Commentor.Create(commentorId, userNameResult);
        return commentor;
    }
}