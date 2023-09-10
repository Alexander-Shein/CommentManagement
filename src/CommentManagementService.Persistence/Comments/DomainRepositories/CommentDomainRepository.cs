using CommentManagementService.Domain.Comments;
using EmpCore.Persistence.EntityFrameworkCore;

namespace CommentManagementService.Persistence.Comments.DomainRepositories;

public class CommentDomainRepository : ICommentDomainRepository
{
    private readonly AppDbContext _appDbContext;

    public CommentDomainRepository(AppDbContext dbContext)
    {
        _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Comment?> GetByIdAsync(long commentId)
    {
        if (commentId <= 0) return null;

        return await _appDbContext.FindAsync<Comment>(commentId).ConfigureAwait(false);
    }

    public void Save(Comment comment)
    {
        _appDbContext.Add(comment ?? throw new ArgumentNullException(nameof(comment)));
        _appDbContext.Attach(comment.Commentor);
    }
}