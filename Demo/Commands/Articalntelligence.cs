﻿using CliMate;
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
            [CLI("archEnemy")]
			string archEnemy) {
			return string.Format( "Let's find {0} !", archEnemy);
		}

		public string GetManual() {
			return "available commands: bringToLife";
		}
	}
}
