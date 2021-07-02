namespace PetSanctuary.Web.Areas.Administration.Controllers
{
    using PetSanctuary.Common;
    using PetSanctuary.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
