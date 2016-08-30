namespace MvcFileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class DirectoriesAndFilesViewModel : System.Object
	{
		public DirectoriesAndFilesViewModel()
			: base()
		{
		}

		private System.Collections.Generic.IList<Models.File> _files;

		public System.Collections.Generic.IList<Models.File> Files
		{
			get
			{
				if (_files == null)
				{
					_files =
						new System.Collections.Generic.List<Models.File>();
				}

				return (_files);
			}
		}

		private System.Collections.Generic.IList<Models.Directory> _directories;

		public System.Collections.Generic.IList<Models.Directory> Directories
		{
			get
			{
				if (_directories == null)
				{
					_directories =
						new System.Collections.Generic.List<Models.Directory>();
				}

				return (_directories);
			}
		}
	}
}
