using CliMate;
using CliMate.interfaces;
using System;

namespace Demo.Commands {
	public class Articalntelligence : ICliMateModule {

		public string name {
			get {
				return "skynet";
			}
		}

		public void Run(params string[] args) {
			throw new NotImplementedException();
		}

		[CLI("bringToLife")]
		public string BringToLife(
            [CLI("archEnemy")] string archEnemy,
            [CLI("treatment")] string treatment) {
			return string.Format( "Let's find {0} and give him {1} !", archEnemy, treatment);
		}

		[CLI("deathByMagnet")]
		public string Kill() {
			return "The world is saved !";
		}

		[CLI("inspectHardware")]
		public string InspectHardware() {
			return "It is a beautiful machinery !";
		}

		public string GetManual() {
			return "available commands: bringToLife";
		}
	}
}
