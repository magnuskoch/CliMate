
namespace CliMate.interfaces {
	public interface IHistory {
		void StartSession();
		void Add(string item);
		string GetPrevious();
		string GetNext();
	}	
}	
