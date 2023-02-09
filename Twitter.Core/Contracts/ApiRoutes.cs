namespace Twitter.Core.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Categories
        {
            public const string GetAll = Base + "/categories";
            public const string GetCategory = Base + "/categories/{id}";
            public const string Create = Base + "/categories";
            public const string Delete = Base + "/categories/{id}";
            public const string DeleteByName = Base + "/categories/name/{Name}";
        }

        public static class Post
        {
            public const string GetAll = Base + "/posts";
            public const string ReadAll = Base + "/readposts";
            public const string NumberOfAvailablePosts = Base + "/numberofavailableposts";
            public const string GetPost = Base + "/posts/{id}";
            public const string Create = Base + "/posts";
            public const string Update = Base + "/posts";
            public const string Delete = Base + "/posts/{id}";
        }

        public static class User
        {
            public const string CreateRole = Base + "/user/roles";
            public const string Register = Base + "/user/register";
            public const string Login = Base + "/user/login";
        }

        public static class FastPost
        {
            public const string Create = Base + "/fastposts";
            public const string GetFastPost = Base + "/fastposts/{id}";
            public const string GetAll = Base + "/fastposts";
            public const string Delete = Base + "/fastposts/{id}";
        }
    }
}
