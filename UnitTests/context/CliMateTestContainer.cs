using CliMate.context;
using SimpleInjector;

namespace Tests {
	public static class CliMateTestContainer {
		public static Container Create() {
			return CliMateContainer.Create(null);
		}
	}	
}	
