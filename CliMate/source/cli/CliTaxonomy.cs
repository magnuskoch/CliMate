using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliMate.interfaces.tokens;
using CliMate.Factories;
using System.Diagnostics;
using CliMate.enums;

namespace CliMate.source.cli {
	public class CliTaxonomy : ICliTaxonomy {

		static CliTaxonomy() {

		}

		private ICliObjectProvider objectProvider;
		private Factory factory;

		public CliTaxonomy(ICliObjectProvider objectProvider, Factory factory) {
			this.objectProvider = objectProvider;
			this.factory = factory;
		}

		public ICliCommand GetCommand(IList<IToken> tokens) {
			ICliCommand command = factory.Create<ICliCommand>();
			ICliObject level = objectProvider.GetCliObject();

			int l = tokens.Count;
			
			for(int i=0; i<l; i++) {
				IToken token = tokens[i];
				ICliObject child;
				if(TryGetCliObject(token, level, out child)) {
					AssignObjectToCommand(command, child);
					level = child;
				} else {
					break;
				}
			}
			return command;
		}

		private void AssignObjectToCommand(ICliCommand command, ICliObject _object) {

			if(_object.type == CliObjectType.Object) {
				command.object_ = _object;
			} else if(_object.type == CliObjectType.Method) {
				Debug.Assert(command.method == null, "Command method already assigned !");
				command.method = _object;
			} else if(_object.type == CliObjectType.Value) {
				command.args.Add(_object);
			} else {
				throw new ArgumentException(string.Format(
					"{0} is not supported", _object.type));
			} 
		}

		private bool TryGetCliObject(IToken token, ICliObject _object, out ICliObject result) {
			List<ICliObject> match = _object.children.
				Where(child => child.alias.Count( alias => alias == token.value) > 0).
				ToList();
			Debug.Assert(match.Count < 2, "Token matched multiple children in taxonomy !");

			result = match.Count == 1 ? match[0] : null;
			return result != null;
		}

		private ICliObject GetObject(IToken token) {
			return null;	
		}
	}
}
