namespace PetSanctuary.Common
{
    public static class GlobalConstants
    {
        public const string WwwRootPath = "C:\\Users\\black\\OneDrive\\Desktop\\PetSanctuary\\Web\\PetSanctuary.Web\\wwwroot";
        public const string SystemName = "PetSanctuary";
        public const string AdministratorRoleName = "Administrator";
        public const string AdministratorEmail = "admin64836@gmail.com";

        public class Pet
        {
            public const int MaxNameLength = 30;
            public const int MinAddressLength = 4;
            public const int MaxAddressLength = 30;
            public const int MinCityLength = 4;
            public const int MaxCityLength = 20;
            public const int MinPetNameLength = 3;
            public const int MaxPetNameLength = 15;
        }

        public class Blog
        {
            public const int MinCommentContentLength = 3;
            public const int MaxCommentContentLength = 90;
            public const int MinBlogTitleLength = 3;
            public const int MaxBlogTitleLength = 20;
            public const int MinBlogDescriptionLength = 10;
            public const int MaxBlogDescriptionLength = 200;
        }

        public class Clinic
        {
            public const int MaxClinicNameLength = 30;
            public const int MinClinicNameLength = 3;
        }

        public class Vet
        {
            public const int MaxVetFirstNameLength = 30;
            public const int MinVetFirstNameLength = 4;
            public const int MaxVetSurnameLength = 30;
            public const int MinVetSurnameLength = 5;
            public const int MaxDescriptionLength = 400;
            public const int MinDecriptionLength = 5;
            public const int MaxQualificationLength = 20;
            public const int MinQualifactionLength = 4;
        }
    }
}
