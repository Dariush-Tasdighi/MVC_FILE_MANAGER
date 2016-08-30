namespace MvcFileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class CreatedFileAndDirectoriesAndFilesViewModel : DirectoriesAndFilesViewModel
	{
		public CreatedFileAndDirectoriesAndFilesViewModel()
			: base()
		{
		}

		public Models.File CreatedFile { get; set; }
	}
}
