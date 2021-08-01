namespace PetSanctuary.Services.Data.Counts
{
    public interface ICountService
    {
        public int GetUserPostsCount(string id, bool isAdmin);

        public int GetUserBlogsCount(string id, bool isAdmin);

        public int GetBlogCommentsCount(string id);

        public int GetVetCommentsCount(string id);
    }
}
