namespace TweetBook.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetPosts = Base + "/" + "posts";
            public const string CreatePost = Base + "/" + "posts";
            public const string Get = Base + "/" + "posts/{postId}";
            public const string Update = Base + "/" + "posts/{postId}";
            public const string Delete = Base + "/" + "posts/{postId}";
        }

        public static class Identity
        {
            public const string Register = Base + "/identity/register";
            public const string Login = Base + "/identity/login";
            public const string Refresh = Base + "/identity/refresh";
        }

        public static class Tags
        {
            public const string CreateTag = Base + "/Tags";
            public const string GetTags = Base + "/Tags";
            public const string DeleteTag = Base + "/Tags/{tagId}";
        }
    }
}
