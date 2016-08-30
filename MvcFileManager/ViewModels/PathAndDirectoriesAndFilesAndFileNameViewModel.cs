namespace MvcFileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PathAndDirectoriesAndFilesAndFileNameViewModel : DirectoriesAndFilesViewModel
	{
		public PathAndDirectoriesAndFilesAndFileNameViewModel()
			: base()
		{
		}

		public string Path { get; set; }

		public string FileName { get; set; }
	}
}
