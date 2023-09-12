using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using EmpCore.QueryStack;

namespace CommentManagementService.Application.Comments.Queries.SearchComments;

public class SearchCommentsQuery : Query<PagedList<CommentListItemDto>>
{
    public Guid? BlogPostId { get; }
    public int PageSize { get; } = 100;
    public int PageNumber { get; } = 1;
    public string SortField { get; } = nameof(CommentListItemDto.CreatedAt);
    public SortDir SortDir { get; } = SortDir.Desc;

    public SearchCommentsQuery(Guid? blogPostId, int? pageSize, int? pageNumber, string? sortField, SortDir? sortDir)
    {
        BlogPostId = blogPostId;
        if (pageSize != null) PageSize = pageSize.Value;
        if (pageNumber != null) PageNumber = pageNumber.Value;
        if (sortField != null) SortField = sortField;
        if (sortDir != null) SortDir = sortDir.Value;
    }
}