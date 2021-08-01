namespace PetSanctuary.Web.ViewModels.User
{
    public abstract class BaseQueryModel
    {
        public int TotalPosts { get; set; }

        public int ElementsPerPage => 3;

        public int CurrentPage { get; set; } = 1;
    }
}
