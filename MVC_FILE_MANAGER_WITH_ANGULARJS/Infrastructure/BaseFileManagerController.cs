namespace Infrastructure
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public partial class BaseFileManagerController : System.Web.Mvc.Controller
	{
		public BaseFileManagerController()
			: base()
		{
		}

		protected int SleepMillisecondsTimeout
		{
			get
			{
				return (1000);
			}
		}

		protected virtual string PreDefinedRootRelativePath
		{
			get
			{
				return ("~");
			}
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult UploadFile(string path)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.UploadFile(PreDefinedRootRelativePath, path);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult DeleteDirectoriesAndFiles
			(MvcFileManager.ViewModels.PathAndDirectoriesAndFilesViewModel viewModel)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.DeleteDirectoriesAndFiles(PreDefinedRootRelativePath, viewModel);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpGet]
		public virtual System.Web.Mvc.ActionResult Download(string path, string fileName)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.Utility.Download(PreDefinedRootRelativePath, path, fileName);

			return (null);
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult GetDirectoriesAndFiles(string path = "/")
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.GetDirectoriesAndFiles(PreDefinedRootRelativePath, path);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult Compress
			(MvcFileManager.ViewModels.PathAndDirectoriesAndFilesAndFileNameViewModel viewModel)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.Compress(PreDefinedRootRelativePath, viewModel);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult Decompress
			(MvcFileManager.ViewModels.PathAndDirectoriesAndFilesViewModel viewModel)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.Decompress(PreDefinedRootRelativePath, viewModel);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult CreateDirectory(string path, string directoryName)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.CreateDirectory(PreDefinedRootRelativePath, path, directoryName);

			return (oJsonData.GetJsonResult());
		}

		[System.Web.Mvc.HttpPost]
		public virtual System.Web.Mvc.JsonResult Rename(string path, string oldName, string newName)
		{
			System.Threading.Thread.Sleep(SleepMillisecondsTimeout);

			MvcFileManager.JsonData oJsonData =
				MvcFileManager.Utility.Rename(PreDefinedRootRelativePath, path, oldName, newName);

			return (oJsonData.GetJsonResult());
		}
	}
}
