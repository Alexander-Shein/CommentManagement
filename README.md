# CommentManagement

- Deployed here: https://comment-management.polandcentral.cloudapp.azure.com/swagger/index.html using 

- Architecture details: https://github.com/Alexander-Shein/EmpCore

# It's subscribed to 2 integration events from BlogPostManagementService
- `BlogPostPublishedEvent` once this event is received `CommentManagementService` enables leaving comments on a published blog post.
- `BlogPostDeletedEvent` once this event is received `CommentManagementService` deletes all the comments for a deleted blog post and disables leaving comments.

# API endpoints
- `GET /v{version}/comments` - searches for comments. Returns paged list. Can be filtered by BlogPostId
- `POST /v{version}/comments` - it creates a new comment on published blog post. You can leave comments only when `BlogPostPublishedEvent` is received. Domain Model with implementation:
```csharp
public class PublishedBlogPost : AggregateRoot<Guid>
{
    public static Result<PublishedBlogPost> Create(Guid id)
    {
        if (id == Guid.Empty) return EmptyBlogPostIdFailure.Instance;
        return new PublishedBlogPost{Id = id};
    }
    
    public Result<Comment> Comment(Commentor commentor, Message message)
    {
        var comment = new Comment(this,
            commentor ?? throw new ArgumentNullException(nameof(commentor)),
            message ?? throw new ArgumentNullException(nameof(message)));

        return Result.Ok(comment);
    }
}
```
As you can see here `PublishedBlogPost` has method `Comment`. It means that it's possible to create a new comment only if you have `PublishedBlogPost` object. 
- `PUT /v{version}/comments/{commentId}/replies` - creates a replay on a comment. Domain Model with implementation:
```csharp
public class Comment : AggregateRoot<long>
{
    private Comment() { }

    public PublishedBlogPost PublishedBlogPost { get; private set; }
    public Commentor Commentor { get; private set; }
    public Comment? ParentComment { get; private set; }
    public Message Message { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    internal Comment(
        PublishedBlogPost publishedBlogPost,
        Commentor commentor,
        Message message,
        Comment? parentComment = null)
    {
        var now = DateTime.UtcNow;

        PublishedBlogPost = publishedBlogPost ?? throw new ArgumentNullException(nameof(publishedBlogPost));
        Commentor = commentor ?? throw new ArgumentNullException(nameof(commentor));
        Message = message ?? throw new ArgumentNullException(nameof(message));
        ParentComment = parentComment;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public Result<Comment> Reply(Commentor commentor, Message message)
    {
        var comment = new Comment(PublishedBlogPost,
            commentor ?? throw new ArgumentNullException(nameof(commentor)),
            message ?? throw new ArgumentNullException(nameof(message)),
            this);

        return comment;
    }
}
```
As you can see it's possible to create a reply only if you have a comment object. No other way because everything is encapsulated and private/internal constructors won't allow you to create an instance in invalid state.

DB schema
```SQL
CREATE TABLE [dbo].[PublishedBlogPost]
(
  [Id]				UNIQUEIDENTIFIER NOT NULL

  CONSTRAINT [PK_PublishedBlogPost_Id] PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[Commentor]
(
  [Id]				 VARCHAR(128) NOT NULL,
  [UserName]   NVARCHAR(256) NOT NULL CONSTRAINT [DF_Commentor_UserName] DEFAULT '',

  CONSTRAINT [PK_Commentor_Id] PRIMARY KEY (Id)
);

CREATE TABLE [dbo].[Comment]
(
  [Id]					        BIGINT IDENTITY(1,1) NOT NULL,
  [PublishedBlogPostId]	UNIQUEIDENTIFIER NOT NULL,
  [CommentorId]			    VARCHAR(128) NOT NULL,
  [ParentCommentId]		  BIGINT NULL,
  [Message]				      NVARCHAR(1024) NOT NULL		CONSTRAINT [DF_Comment_Message] DEFAULT '',
  [CreatedAt]				    DATETIME2 NOT NULL			CONSTRAINT [DF_Comment_CreatedAt] DEFAULT GETDATE(),
  [UpdatedAt]				    DATETIME2 NOT NULL			CONSTRAINT [DF_Comment_UpdatedAt] DEFAULT GETDATE(),

  CONSTRAINT [PK_Comment_Id] PRIMARY KEY (Id),
  CONSTRAINT [FK_Comment_PublishedBlogPostId_PublishedBlogPost_Id] FOREIGN KEY ([PublishedBlogPostId]) REFERENCES [dbo].[PublishedBlogPost]([Id]),
  CONSTRAINT [FK_Comment_CommentorId_Commentor_Id] FOREIGN KEY ([CommentorId]) REFERENCES [dbo].[Commentor]([Id]),
  CONSTRAINT [FK_Comment_ParentCommentId_Comment_Id] FOREIGN KEY ([ParentCommentId]) REFERENCES [dbo].[Comment]([Id])
);



```
