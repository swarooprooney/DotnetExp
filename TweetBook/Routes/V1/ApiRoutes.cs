namespace TweetBook.Routes.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string GetPosts = Base +"/"+"posts";
        }
    }
}
