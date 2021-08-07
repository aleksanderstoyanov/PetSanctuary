namespace PetSanctuary.Test.Routing
{
    using PetSanctuary.Web.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class VetsControllerTest
    {
        [Fact]
        public void IndexShouldBeMappedCorrectly()
          => MyRouting
            .Configuration()
            .ShouldMap("/Vets/Index/1")
            .To<VetsController>(c=>c.Index(1));

        [Fact]
        public void LikeShouldBeMappedCorrectly()
         => MyRouting
            .Configuration()
            .ShouldMap("/Vets/Like/TestLike")
            .To<VetsController>(c => c.Like("TestLike"));

        [Fact]
        public void DislikeShouldBeMappedCorrectly()
         => MyRouting
            .Configuration()
            .ShouldMap("/Vets/Like/TestDislike")
            .To<VetsController>(c => c.Like("TestDislike"));

        [Fact]
        public void DescriptionShouldBeMappedCorrectly()
            => MyRouting
            .Configuration()
            .ShouldMap("/Vets/Description/TestDescription")
            .To<VetsController>(c => c.Description("TestDescription"));
    }
}
