namespace GirlsHandmadeShop.Web.Areas.Administration.Controllers
{
    using GirlsHandmadeShop.Common;
    using GirlsHandmadeShop.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
