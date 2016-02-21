using CliMate;
using CliMate.interfaces;
using System;

namespace Demo.Commands {
	public class Articalntelligence : CliMateModule {

		public override string name {
			get {
				return "skynet";
			}
		}

		public void Run(params string[] args) {
			throw new NotImplementedException();
		}

		[CliMateExposed("bringToLife")]
		public string BringToLife(
            [CliMateExposed("archEnemy")]
			string archEnemy) {
			return string.Format( "Let's find {0} !", archEnemy);
		}

		public override string GetManual() {
			return "available commands: bringToLife";
		}
	}
}
