using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using EmpCore.QueryStack;
using EmpCore.QueryStack.Middleware.Caching;

namespace CommentManagementService.Application.Comments.Queries.SearchComments;

public class CachePolicy : CachePolicy<SearchCommentsQuery, PagedList<CommentListItemDto>>
{
    public override TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromSeconds(15);
}