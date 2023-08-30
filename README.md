# CommentManagement

Deployed here: http://comment-management.polandcentral.cloudapp.azure.com/swagger/index.html

- It's subscribed to 2 events from BlogPostManagementService: BlogPostPublishedEvent, BlogPostDeletedEvent


```
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
