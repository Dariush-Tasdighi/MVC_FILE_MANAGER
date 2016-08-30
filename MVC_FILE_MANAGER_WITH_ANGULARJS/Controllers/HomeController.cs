using System.Linq;

namespace MVC_FILE_MANAGER_WITH_ANGULARJS.Controllers
{
	public partial class HomeController : System.Web.Mvc.Controller
	{
		public HomeController()
		{
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			return (View());
		}
	}
}
