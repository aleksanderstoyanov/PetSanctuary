namespace PetSanctuary.Test.Controllers.Admin
{
    using PetSanctuary.Web.Areas.Administration.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class AdministrationControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeAndAreaAttributes()
            => MyMvc
            .Controller<AdministrationController>()
            .ShouldHave()
            .Attributes(attributes => attributes
                .RestrictingForAuthorizedRequests()
            .SpecifyingArea("Administration"));
    }
}
