namespace MvcFileManager.ViewModels
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class PathAndDirectoriesAndFilesAndPathCollectionViewModel : DirectoriesAndFilesViewModel
	{
		public PathAndDirectoriesAndFilesAndPathCollectionViewModel()
			: base()
		{
		}

		public string Path { get; set; }

		private System.Collections.Generic.IList<Models.PathItem> _pathCollection;

		public System.Collections.Generic.IList<Models.PathItem> PathCollection
		{
			get
			{
				if (_pathCollection == null)
				{
					_pathCollection =
						new System.Collections.Generic.List<Models.PathItem>();
				}

				return (_pathCollection);
			}
		}
	}
}
