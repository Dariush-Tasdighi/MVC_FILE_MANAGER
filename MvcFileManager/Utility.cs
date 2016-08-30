using System.Linq;

namespace MvcFileManager
{
	/// <summary>
	/// Version: 1.0.0
	/// Update Date: 1394/11/06
	/// Changed in New Version: 1.1.0
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Utility
	{
		public const string VERSION = "1.0.0";

		static Utility()
		{
		}

		public static string FixPath(string path)
		{
			if (path == null)
			{
				path = "/";
			}

			path = path.Trim();

			if (path == string.Empty)
			{
				path = "/";
			}

			while (path.Contains("//"))
			{
				path =
					path.Replace("//", "/");
			}

			if (path != "/")
			{
				if (path.EndsWith("/") == false)
				{
					path =
						string.Format("{0}/", path);
				}

				if (path.StartsWith("/") == false)
				{
					path =
						string.Format("/{0}", path);
				}
			}

			return (path);
		}

		private static void InitializeZipFile(Ionic.Zip.ZipFile zipFile)
		{
			zipFile.AlternateEncoding = zipFile.AlternateEncoding;
			zipFile.AlternateEncodingUsage = Ionic.Zip.ZipOption.Default; // Default: [Default]

			zipFile.Strategy = Ionic.Zlib.CompressionStrategy.Default; // Default: [Default]
			zipFile.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Default; // Default: [Default]
			zipFile.CompressionMethod = Ionic.Zip.CompressionMethod.Deflate; // Default: [Deflate]
			zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression; // Default: [Default]

			zipFile.CodecBufferSize = 0; // Default: [0]
			zipFile.BufferSize = 32768; // Default: [32768]
			zipFile.MaxOutputSegmentSize = 0; // Default: [0]
			zipFile.ParallelDeflateMaxBufferPairs = 16; // Default: [16]
			zipFile.ParallelDeflateThreshold = 524288; // Default: [524288]

			zipFile.Comment = null; // Default: [null]
			zipFile.FullScan = false; // Default: [false]
			zipFile.TempFileFolder = null; // Default: [null]
			zipFile.CaseSensitiveRetrieval = false; // Default: [false]
			zipFile.FlattenFoldersOnExtract = false; // Default: [false]
			zipFile.SortEntriesBeforeSaving = false; // Default: [false]

			zipFile.EmitTimesInUnixFormatWhenSaving = false; // Default: [false]
			zipFile.EmitTimesInWindowsFormatWhenSaving = true; // Default: [true]

			zipFile.Password = null; // Default: [null]
			zipFile.Encryption = Ionic.Zip.EncryptionAlgorithm.None; // Default: [None]

			zipFile.ZipErrorAction = Ionic.Zip.ZipErrorAction.Skip; // Default: [Throw]
			zipFile.ExtractExistingFile = Ionic.Zip.ExtractExistingFileAction.OverwriteSilently; // Default: [Throw]
		}

		public static JsonData UploadFile(string preDefinedRootRelativePath, string path)
		{
			JsonData oJsonData = null;

			var varFiles =
				System.Web.HttpContext.Current.Request.Files;

			// **************************************************
			if (varFiles.Count == 0)
			{
				string strErrorMessage =
					"You did not specify file for uploading!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			// **************************************************
			var varFile = varFiles[0];

			if (string.IsNullOrWhiteSpace(varFile.FileName))
			{
				string strErrorMessage =
					"You did not specify file for uploading!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			// **************************************************
			if (varFile.ContentLength == 0)
			{
				string strErrorMessage =
					"The file does not uploaded successfully!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			// **************************************************
			path = FixPath(path);

			string strFileName =
				System.IO.Path.GetFileName(varFile.FileName);

			string strPreDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}",
				strPreDefinedRootRelativePath, path);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			string strRootRelativePathName =
				string.Format("{0}{1}",
				strRootRelativePath, strFileName);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);
			// **************************************************

			// **************************************************
			if (System.IO.File.Exists(strPathName))
			{
				string strErrorMessage =
					"There is a file with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			// **************************************************
			if (System.IO.Directory.Exists(strPathName))
			{
				string strErrorMessage =
					"There is a directory with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			// **************************************************
			try
			{
				varFile.SaveAs(strPathName);

				System.IO.FileInfo oFileInfo =
					new System.IO.FileInfo(strPathName);

				Models.File oFile =
					new Models.File(oFileInfo);

				string strInformationMessage =
					string.Format("The file [{0}] uploaded successfully.", strFileName);

				oJsonData =
					new JsonData()
					{
						Data = oFile,
						MessageText = strInformationMessage,
						State = Enums.JsonResultStates.Success,
					};

				return (oJsonData);
			}
			catch (System.Exception ex)
			{
				string strErrorMessage = ex.Message;

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************
		}

		public static JsonData GetDirectoriesAndFiles
			(string preDefinedRootRelativePath, string path)
		{
			JsonData oJsonData = null;

			path = FixPath(path);

			preDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}",
				preDefinedRootRelativePath, path);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				oJsonData =
					new JsonData()
					{
						MessageText = "Error",
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			ViewModels.PathAndDirectoriesAndFilesAndPathCollectionViewModel oReturnViewModel =
				new ViewModels.PathAndDirectoriesAndFilesAndPathCollectionViewModel();

			oReturnViewModel.Path = path;

			// **************************************************
			Models.PathItem oPathItem = null;

			string[] strPathCollection = path.Split('/');

			string strFullPath = string.Empty;

			for (int intIndex = 0; intIndex <= strPathCollection.Length - 2; intIndex++)
			{
				string strCurrentPath =
					strPathCollection[intIndex];

				strFullPath =
					string.Format("{0}{1}/",
					strFullPath, strCurrentPath);

				if (strCurrentPath == string.Empty)
				{
					strCurrentPath =
						Resources.Captions.Root;
				}

				oPathItem =
					new Models.PathItem()
					{
						Name = strCurrentPath,
						Path = strFullPath,
					};

				oReturnViewModel.PathCollection.Add(oPathItem);
			}
			// **************************************************

			System.IO.DirectoryInfo oDirectoryInfo =
				new System.IO.DirectoryInfo(strPath);

			foreach (System.IO.DirectoryInfo oCurrentDirectoryInfo in oDirectoryInfo.GetDirectories())
			{
				Models.Directory oDirectory =
					new Models.Directory(oCurrentDirectoryInfo);

				oReturnViewModel.Directories.Add(oDirectory);
			}

			foreach (System.IO.FileInfo oCurrentFileInfo in oDirectoryInfo.GetFiles())
			{
				Models.File oFile =
					new Models.File(oCurrentFileInfo);

				oReturnViewModel.Files.Add(oFile);
			}

			oJsonData =
				new JsonData()
				{
					Data = oReturnViewModel,
					State = Enums.JsonResultStates.Success,
				};

			return (oJsonData);
		}

		public static void Download
			(string preDefinedRootRelativePath, string path, string fileName)
		{
			// **************************************************
			path = FixPath(path);

			preDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePathName =
				string.Format("{0}{1}{2}",
				preDefinedRootRelativePath, path, fileName);

			string strPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePathName);

			if (System.IO.File.Exists(strPathName) == false)
			{
				return;
			}
			// **************************************************

			System.IO.Stream oStream = null;

			try
			{
				// Open the file
				oStream =
					new System.IO.FileStream
						(path: strPathName,
						mode: System.IO.FileMode.Open,
						share: System.IO.FileShare.Read,
						access: System.IO.FileAccess.Read);

				// **************************************************
				System.Web.HttpContext.Current.Response.Buffer = false;

				// Setting the unknown [ContentType]
				// will display the saving dialog for the user
				System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";

				// With setting the file name,
				// in the saving dialog, user will see
				// the [strFileName] name instead of [download]!
				System.Web.HttpContext.Current.Response
					.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

				long lngFileLength = oStream.Length;

				// Notify user (client) the total file length
				System.Web.HttpContext.Current.Response
					.AddHeader("Content-Length", lngFileLength.ToString());
				// **************************************************

				// Total bytes that should be read
				long lngDataToRead = lngFileLength;

				if (lngDataToRead == 0)
				{
					System.Web.HttpContext.Current.Response.Flush();

					return;
				}

				// Read the bytes of file
				while (lngDataToRead > 0)
				{
					// The below code is just for testing! So we commented it!
					//System.Threading.Thread.Sleep(1000);

					// Verify that the client is connected or not?
					if (System.Web.HttpContext.Current.Response.IsClientConnected)
					{
						// 8KB
						int intBufferSize = 8 * 1024;

						// Create buffer for reading [intBufferSize] bytes from file
						byte[] bytBuffers =
							new System.Byte[intBufferSize];

						// Read the data and put it in the buffer.
						int intTheBytesThatReallyHasBeenReadFromTheStream =
							oStream.Read(buffer: bytBuffers, offset: 0, count: intBufferSize);

						// Write the data from buffer to the current output stream.
						System.Web.HttpContext.Current.Response.OutputStream.Write
							(buffer: bytBuffers, offset: 0,
							count: intTheBytesThatReallyHasBeenReadFromTheStream);

						// Flush (Send) the data to output
						// (Don't buffer in server's RAM!)
						System.Web.HttpContext.Current.Response.Flush();

						lngDataToRead =
							lngDataToRead - intTheBytesThatReallyHasBeenReadFromTheStream;
					}
					else
					{
						// Prevent infinite loop if user disconnected!
						lngDataToRead = -1;
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (oStream != null)
				{
					//Close the file.
					oStream.Close();
					oStream.Dispose();
					oStream = null;
				}

				System.Web.HttpContext.Current.Response.Close();
			}
		}

		public static JsonData Compress
			(string preDefinedRootRelativePath,
			ViewModels.PathAndDirectoriesAndFilesAndFileNameViewModel viewModel)
		{
			JsonData oJsonData = null;

			// **************************************************
			if (string.IsNullOrWhiteSpace(viewModel.FileName))
			{
				string strErrorMessage =
					"You did not specify file name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			viewModel.FileName = viewModel.FileName.Trim();
			// **************************************************

			viewModel.Path =
				FixPath(viewModel.Path);

			string strPreDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}",
				strPreDefinedRootRelativePath, viewModel.Path);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			string strRootRelativeCompressPath =
				string.Format("{0}{1}",
				strRootRelativePath, viewModel.FileName);

			string strCompressPathName =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativeCompressPath);

			if (System.IO.File.Exists(strCompressPathName))
			{
				string strErrorMessage =
					"There is a file with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			if (System.IO.Directory.Exists(strCompressPathName))
			{
				string strErrorMessage =
					"There is a directory with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			ViewModels.CreatedFileAndDirectoriesAndFilesViewModel oReturnViewModel =
				new ViewModels.CreatedFileAndDirectoriesAndFilesViewModel();

			Ionic.Zip.ZipFile oZipFile = null;

			try
			{
				oZipFile =
					new Ionic.Zip.ZipFile(strCompressPathName);

				InitializeZipFile(oZipFile);

				foreach (Models.File oCurrentFile in viewModel.Files)
				{
					string strCurrentPathName =
						string.Format("{0}\\{1}",
						strPath, oCurrentFile.Name);

					if (System.IO.File.Exists(strCurrentPathName))
					{
						try
						{
							oZipFile.AddFile
								(fileName: strCurrentPathName,
								directoryPathInArchive: string.Empty);

							oZipFile.Save();
						}
						catch (System.Exception ex)
						{
							System.IO.FileInfo oFileInfoThatHasError =
								new System.IO.FileInfo(strCurrentPathName);

							Models.File oFileThatHasError =
								new Models.File(oFileInfoThatHasError);

							oFileThatHasError.Message = ex.Message;

							oReturnViewModel.Files.Add(oFileThatHasError);
						}
					}
				}

				foreach (Models.Directory oCurrentDirectory in viewModel.Directories)
				{
					string strCurrentPath =
						string.Format("{0}\\{1}",
						strPath, oCurrentDirectory.Name);

					if (System.IO.Directory.Exists(strCurrentPath))
					{
						try
						{
							oZipFile.AddDirectory
								(directoryName: strCurrentPath,
								directoryPathInArchive: oCurrentDirectory.Name);

							oZipFile.Save();
						}
						catch (System.Exception ex)
						{
							System.IO.DirectoryInfo oDirectoryInfoThatHasError =
								new System.IO.DirectoryInfo(strCurrentPath);

							Models.Directory oDirectoryThatHasError =
								new Models.Directory(oDirectoryInfoThatHasError);

							oDirectoryThatHasError.Message = ex.Message;

							oReturnViewModel.Directories.Add(oDirectoryThatHasError);
						}
					}
				}

				System.IO.FileInfo oFileInfo =
					new System.IO.FileInfo(strCompressPathName);

				Models.File oFile =
					new Models.File(oFileInfo);

				oReturnViewModel.CreatedFile = oFile;

				string strInformationMessage =
					string.Format("The compressed file [{0}] created successfully.",
					viewModel.FileName);

				oJsonData =
					new JsonData()
					{
						Data = oReturnViewModel,
						MessageText = strInformationMessage,
						State = Enums.JsonResultStates.Success,
					};

				return (oJsonData);
			}
			catch
			{
				string strErrorMessage =
					"Unexpected Error on Creating Compressed File!";

				oJsonData =
					new JsonData()
					{
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			finally
			{
				if (oZipFile != null)
				{
					oZipFile.Dispose();
					oZipFile = null;
				}
			}
		}

		public static JsonData Decompress
			(string preDefinedRootRelativePath,
			ViewModels.PathAndDirectoriesAndFilesViewModel viewModel)
		{
			JsonData oJsonData = null;

			viewModel.Path =
				FixPath(viewModel.Path);

			string strPreDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}",
				strPreDefinedRootRelativePath, viewModel.Path);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			Ionic.Zip.ZipFile oZipFile = null;

			try
			{
				foreach (Models.File oCurrentFile in viewModel.Files)
				{
					string strCurrentPathName =
						string.Format("{0}\\{1}",
						strPath, oCurrentFile.Name);

					if (System.IO.File.Exists(strCurrentPathName) == false)
					{
						continue;
					}

					oZipFile =
						new Ionic.Zip.ZipFile(strCurrentPathName);

					InitializeZipFile(oZipFile);

					try
					{
						oZipFile.ExtractAll
							(strPath, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
					}
					catch
					{
					}
				}

				oJsonData =
					GetDirectoriesAndFiles(preDefinedRootRelativePath, viewModel.Path);

				return (oJsonData);
			}
			catch
			{
				string strErrorMessage =
					"Unexpected Error on Decompressing File(s)!";

				oJsonData =
					new JsonData()
					{
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			finally
			{
				if (oZipFile != null)
				{
					oZipFile.Dispose();
					oZipFile = null;
				}
			}
		}

		public static JsonData CreateDirectory
			(string preDefinedRootRelativePath, string path, string directoryName)
		{
			JsonData oJsonData = null;

			if (string.IsNullOrWhiteSpace(path))
			{
				string strErrorMessage =
					"The path does not specify!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			if (string.IsNullOrWhiteSpace(directoryName))
			{
				string strErrorMessage =
					"You did not specify directory name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			directoryName = directoryName.Trim();

			path = FixPath(path);

			string strPreDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}{2}",
				strPreDefinedRootRelativePath, path, directoryName);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			if (System.IO.File.Exists(strPath))
			{
				string strErrorMessage =
					"There is a file with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			if (System.IO.Directory.Exists(strPath))
			{
				string strErrorMessage =
					"This directory is already exist!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			try
			{
				System.IO.DirectoryInfo oDirectoryInfo =
					System.IO.Directory.CreateDirectory(strPath);

				Models.Directory oDirectory =
					new Models.Directory(oDirectoryInfo);

				string strInformationMessage =
					"The directory created successfully.";

				oJsonData =
					new JsonData()
					{
						Data = oDirectory,
						DisplayMessage = true,
						MessageText = strInformationMessage,
						State = Enums.JsonResultStates.Success,
					};

				return (oJsonData);
			}
			catch
			{
				string strErrorMessage =
					"Unexpected error when creating directory!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			finally
			{
			}
		}

		public static JsonData Rename
			(string preDefinedRootRelativePath, string path, string oldName, string newName)
		{
			JsonData oJsonData = null;

			// **************************************************
			if (string.IsNullOrWhiteSpace(oldName))
			{
				string strErrorMessage =
					"You did not specify old name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			oldName = oldName.Trim();
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(newName))
			{
				string strErrorMessage =
					"You did not specify new name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			newName = newName.Trim();
			// **************************************************

			// **************************************************
			if (string.Compare(oldName, newName, ignoreCase: true) == 0)
			{
				string strErrorMessage =
					"The old name and new name are the same!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			// **************************************************

			path = FixPath(path);

			string strPreDefinedRootRelativePath =
				Utility
				.FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strOldRootRelativePath =
				string.Format("{0}{1}{2}",
				strPreDefinedRootRelativePath, path, oldName);

			string strOldPath =
				System.Web.HttpContext.Current.Server.MapPath(strOldRootRelativePath);

			string strNewRootRelativePath =
				string.Format("{0}{1}{2}",
				strPreDefinedRootRelativePath, path, newName);

			string strNewPath =
				System.Web.HttpContext.Current.Server.MapPath(strNewRootRelativePath);

			if (System.IO.File.Exists(strNewPath))
			{
				string strErrorMessage =
					"There is a file with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			if (System.IO.Directory.Exists(strNewPath))
			{
				string strErrorMessage =
					"There is a directory with this name!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}

			try
			{
				string strInformationMessage = string.Empty;

				if (System.IO.File.Exists(strOldPath))
				{
					System.IO.File.Move(strOldPath, strNewPath);

					strInformationMessage =
						string.Format("The file renamed from [{0}] to [{1}] successfully.",
						oldName, newName);
				}
				else
				{
					if (System.IO.Directory.Exists(strOldPath))
					{
						System.IO.Directory.Move(strOldPath, strNewPath);

						strInformationMessage =
							string.Format("The directory renamed from [{0}] to [{1}] successfully.",
							oldName, newName);
					}
					else
					{
						string strErrorMessage =
							string.Format("There is no directory of file with [{0}] name!",
							oldName);

						oJsonData =
							new JsonData()
							{
								DisplayMessage = true,
								MessageText = strErrorMessage,
								State = Enums.JsonResultStates.Error,
							};

						return (oJsonData);
					}
				}

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strInformationMessage,
						State = Enums.JsonResultStates.Success,
					};

				return (oJsonData);
			}
			catch (System.Exception ex)
			{
				string strErrorMessage = ex.Message;

				if (ex.Message.Contains("Access to the path") &&
					ex.Message.Contains("is denied."))
				{
					strErrorMessage =
						string.Format("Access to the [{0}] is denied.", oldName);
				}

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};

				return (oJsonData);
			}
			finally
			{
			}
		}

		public static string FixPreDefinedRootRelativePath(string preDefinedRootRelativePath)
		{
			while (preDefinedRootRelativePath.Contains("//"))
			{
				preDefinedRootRelativePath =
					preDefinedRootRelativePath.Replace("//", "/");
			}

			if (preDefinedRootRelativePath.EndsWith("/"))
			{
				preDefinedRootRelativePath =
					preDefinedRootRelativePath.Substring
					(0, preDefinedRootRelativePath.Length - 1);
			}

			return (preDefinedRootRelativePath);
		}

		public static JsonData DeleteDirectoriesAndFiles
			(string preDefinedRootRelativePath, ViewModels.PathAndDirectoriesAndFilesViewModel viewModel)
		{
			JsonData oJsonData = null;

			viewModel.Path = FixPath(viewModel.Path);

			preDefinedRootRelativePath =
				FixPreDefinedRootRelativePath(preDefinedRootRelativePath);

			string strRootRelativePath =
				string.Format("{0}{1}",
				preDefinedRootRelativePath, viewModel.Path);

			string strPath =
				System.Web.HttpContext.Current.Server.MapPath(strRootRelativePath);

			if (System.IO.Directory.Exists(strPath) == false)
			{
				oJsonData =
					new JsonData()
					{
						State = Enums.JsonResultStates.Error,
						MessageText = "The current path is not valid!",
					};

				return (oJsonData);
			}

			ViewModels.DirectoriesAndFilesViewModel oReturnViewModel =
				new ViewModels.DirectoriesAndFilesViewModel();

			bool blnHasError = false;

			foreach (Models.File oFile in viewModel.Files)
			{
				string strCurrentPathName =
					string.Format("{0}{1}", strPath, oFile.Name);

				if (System.IO.File.Exists(strCurrentPathName))
				{
					try
					{
						System.IO.File.Delete(strCurrentPathName);
					}
					catch (System.Exception ex)
					{
						blnHasError = true;

						System.IO.FileInfo oFileInfoThatHasError =
							new System.IO.FileInfo(strCurrentPathName);

						Models.File oFileThatHasError =
							new Models.File(oFileInfoThatHasError);

						if (ex.Message.Contains("it is being used by another process"))
						{
							oFileThatHasError.Message =
								"It is being used by another process.";
						}
						else
						{
							oFileThatHasError.Message = ex.Message;
						}

						oReturnViewModel.Files.Add(oFileThatHasError);
					}
				}
			}

			foreach (Models.Directory oDirectory in viewModel.Directories)
			{
				string strCurrentPath =
					string.Format("{0}{1}", strPath, oDirectory.Name);

				if (System.IO.Directory.Exists(strCurrentPath))
				{
					try
					{
						System.IO.Directory.Delete(strCurrentPath, recursive: true);
					}
					catch (System.Exception ex)
					{
						blnHasError = true;

						System.IO.DirectoryInfo oDirectoryInfoThatHasError =
							new System.IO.DirectoryInfo(strCurrentPath);

						Models.Directory oDirectoryThatHasError =
							new Models.Directory(oDirectoryInfoThatHasError);

						if (ex.Message.Contains("it is being used by another process"))
						{
							oDirectoryThatHasError.Message =
								"It is being used by another process.";
						}
						else
						{
							oDirectoryThatHasError.Message = ex.Message;
						}

						oReturnViewModel.Directories.Add(oDirectoryThatHasError);
					}
				}
			}

			if (blnHasError)
			{
				string strErrorMessage =
					"Some selected directories and/or files does not deleted!";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						Data = oReturnViewModel,
						MessageText = strErrorMessage,
						State = Enums.JsonResultStates.Error,
					};
			}
			else
			{
				string strInformationMessage =
					"All selected directories and/or files deleted successfully.";

				oJsonData =
					new JsonData()
					{
						DisplayMessage = true,
						Data = oReturnViewModel,
						MessageText = strInformationMessage,
						State = Enums.JsonResultStates.Success,
					};
			}

			return (oJsonData);
		}
	}
}
