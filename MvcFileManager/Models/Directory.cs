namespace MvcFileManager.Models
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public class Directory : BaseEntity
	{
		public Directory()
			: base()
		{
		}

		public Directory(System.IO.DirectoryInfo directoryInfo)
		{
			Name = directoryInfo.Name;

			CreationTime = directoryInfo.CreationTime;
			LastWriteTime = directoryInfo.LastWriteTime;
			LastAccessTime = directoryInfo.LastAccessTime;
		}
	}
}
