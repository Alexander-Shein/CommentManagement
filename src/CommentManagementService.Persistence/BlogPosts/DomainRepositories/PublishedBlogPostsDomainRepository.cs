using CommentManagementService.Domain.BlogPosts;
using EmpCore.Persistence.EntityFrameworkCore;

namespace CommentManagementService.Persistence.BlogPosts.DomainRepositories;

public class PublishedBlogPostsDomainRepository : IPublishedBlogPostDomainRepository
{
    private readonly AppDbContext _appDbContext;

    public PublishedBlogPostsDomainRepository(AppDbContext dbContext)
    {
        _appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<PublishedBlogPost?> GetByIdAsync(Guid blogPostId)
    {
        if (blogPostId == Guid.Empty) return null;
        
        return await _appDbContext.FindAsync<PublishedBlogPost>(blogPostId).ConfigureAwait(false);
    }

    public void Save(PublishedBlogPost blogPost)
    {
        _appDbContext.Add(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }

    public void Delete(PublishedBlogPost blogPost)
    {
        _appDbContext.Remove(blogPost ?? throw new ArgumentNullException(nameof(blogPost)));
    }
}