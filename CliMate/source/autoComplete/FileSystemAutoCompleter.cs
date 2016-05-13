using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CliMate.source {
	public class FileSystemAutoCompleter {

		public IList<string> GetAutoCompletions(string input) {
			input = input.Trim();

			string[] split = input.Split( Path.DirectorySeparatorChar);
			string matchedDirectory = string.Empty;
			string trailing = string.Empty;
			DirectoryInfo lastExistingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()); 

			// Find the last "deepest" existing directory in input
			for(int i=0; i<split.Length; i++) {
				string token = split[i].Trim();
				DirectoryInfo directory = null;

				if(!string.IsNullOrEmpty(token)) {
					matchedDirectory = Path.Combine(matchedDirectory, split[i]);
					Debug.Assert(!string.IsNullOrEmpty(matchedDirectory), string.Format("Matched directory was null or empty !"));
					directory = new DirectoryInfo(matchedDirectory);
				}

				if(directory != null && directory.Exists) {
					lastExistingDirectory = directory;
				} else {
					trailing = string.Join(Path.DirectorySeparatorChar.ToString(), split.Skip(i).ToArray());	
					break;
				}
			}

			// Return all the items (files and directories) that match the trailing part of input;
			var autoCompletions = new List<string>();
		
			string searchPattern = string.IsNullOrEmpty(trailing) ? "" : trailing.ToLower();

			// Seems lije the search pattern in GetFiles/GetDirectories is case sensitive in mono.
			autoCompletions.AddRange( lastExistingDirectory.GetFiles().Select( file => file.Name ) );	
			autoCompletions.AddRange( lastExistingDirectory.GetDirectories().Select( dir => dir.Name ) );	

			// So let's do manual filtering
			autoCompletions = autoCompletions.Where( item => item.ToLower().Contains(searchPattern) ).ToList();


			return autoCompletions;
		}	

	}	
}	
