namespace MVC_FILE_MANAGER_WITH_ANGULARJS.Controllers
{
	public partial class SampleController : Infrastructure.BaseFileManagerController
	{
		public SampleController()
			: base()
		{
		}

		protected override string PreDefinedRootRelativePath
		{
			get
			{
				return ("~/App_Data/Test");
			}
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ViewResult Index()
		{
			return (View());
		}
	}
}
