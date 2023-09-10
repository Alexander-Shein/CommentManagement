﻿using CommentManagementService.Application.Comments.Queries.SearchComments.DTOs;
using EmpCore.QueryStack;

namespace CommentManagementService.Application.Comments.Queries.SearchComments;

public class SearchCommentsQuery : Query<PagedList<CommentListItemDto>>
{
    public Guid? BlogPostId { get; }
    public int PageSize { get; }
    public int PageNumber { get; }
    public string SortField { get; }
    public SortDir SortDir { get; }

    public SearchCommentsQuery(Guid? blogPostId, int pageSize, int pageNumber, string sortField, SortDir sortDir)
    {
        BlogPostId = blogPostId;
        PageSize = pageSize;
        PageNumber = pageNumber;
        SortField = sortField;
        SortDir = sortDir;
    }
}