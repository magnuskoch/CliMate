using CliMate.Factories;
using CliMate.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CliMate {
	public abstract class CliMateCommand : ICliMateModule {

		public static IReflectionFacade reflectionFacade { get; set; }
		public abstract string name { get; }

		private List<Action> desctructorRoutines = new List<Action>();
		
		public CliMateCommand() {
			Type t = this.GetType();
			List<Attribute> attrs = System.Attribute.GetCustomAttributes(t).ToList();
			reflectionFacade.Register(this);
			desctructorRoutines.Add(() => reflectionFacade.Deregister(this));
			
		}

		// Deregister should be called by the client. So just to be sure.
		~CliMateCommand() {
			desctructorRoutines.ForEach(r => r());
		}



		public abstract string GetManual();

		public void Deregister() {
			desctructorRoutines.ForEach(r => r());
			desctructorRoutines.Clear();
		}
	}
}
