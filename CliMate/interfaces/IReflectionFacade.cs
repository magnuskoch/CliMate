using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.interfaces {
	public interface IReflectionFacade {
		Type GetSingleImplementor<T>();
		T Instantiate<T>(Type appType) where T : class;
		void Register(ICliMateModule command);
		void Deregister(ICliMateModule command);
		bool TryGetModule(string name, out ICliMateModule module);
    }
}
