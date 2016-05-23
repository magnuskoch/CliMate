using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CliMate.interfaces;

namespace CliMate.source {
	public class FileSystemAutoCompleter : IAutoCompletionProvider<string> {

		public IList<string> GetAutoCompletions(string input) {
			input = input ?? string.Empty;
			input = input.Trim();

			string[] split = input.Split( Path.DirectorySeparatorChar);
			string matchedDirectory = string.Empty;
			string trailing = string.Empty;
			DirectoryInfo lastExistingDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()); 

			// Find the last "deepest" existing directory in input
			for(int i=0; i<split.Length; i++) {
				string token = split[i].Trim();
	 			DirectoryInfo directory = null;
				string potentialMatchedDirectory = string.Empty;

				if(!string.IsNullOrEmpty(token)) {
					potentialMatchedDirectory = Path.Combine(matchedDirectory, split[i]);
					Debug.Assert(!string.IsNullOrEmpty(matchedDirectory), string.Format("Matched directory was null or empty !"));
					directory = new DirectoryInfo(potentialMatchedDirectory);
				}

				if(directory != null && directory.Exists) {
					lastExistingDirectory = directory;
					// We store the matchedDirectory directly as a string to preserve the actual user input, we we use at
					// the end of the method to build the auto completions. The user string might provide a (strange) path  
					// the a file. We want to preserve this path, so the user wont see his input changing on auto complete.
					matchedDirectory = potentialMatchedDirectory;
				} else {
					trailing = string.Join(Path.DirectorySeparatorChar.ToString(), split.Skip(i).ToArray());	
					break;
				}
			}

			// Return all the items (files and directories) that match the trailing part of input;
			var autoCompletions = new List<string>();
		
			string searchPattern = string.IsNullOrEmpty(trailing) ? "" : trailing.ToLower();

			// Seems like the search pattern in GetFiles/GetDirectories is case sensitive in mono.
			autoCompletions.AddRange( lastExistingDirectory.GetFiles().Select( file => file.Name ) );	
			autoCompletions.AddRange( lastExistingDirectory.GetDirectories().Select( dir => dir.Name + Path.DirectorySeparatorChar) );	

			// So let's do manual filtering
			autoCompletions = autoCompletions.Where( item => item.ToLower().Contains(searchPattern) ).ToList();

			// Add the location (directory) of the auto completions
			autoCompletions = autoCompletions.Select( c => Path.Combine( matchedDirectory, c ) ).ToList();
			return autoCompletions;
		}	

	}	
}	
