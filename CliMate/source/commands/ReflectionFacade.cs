using CliMate.interfaces;
using CliMate.source.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.Factories {
	public class ReflectionFacade : IReflectionFacade {

		// We don't want to keep objects alive by internal references.
		private Dictionary<string,WeakReference<ICliMateModule>> exposedModules = 
			new Dictionary<string, WeakReference<ICliMateModule>>();

		public Type GetSingleImplementor<T>() {
			List<Type> types = GetMultipleImplementors<T>();

			if (types.Count != 1) {
				string implementors = types.Count == 0 ? "None" : String.Join(",", types);
				throw new ArgumentException( string.Format(
						"{0} should be implemented by excactly one class ! It is implemented by {1}", 
						typeof(T), implementors
					));
			}

			return types[0];	
		}

		private List<Type> GetMultipleImplementors<T>() {

			Type type = typeof(T);
			List<Type> types = GetClientAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => type.IsAssignableFrom(p))
				.ToList();
			return types;
		}

		public T Instantiate<T>(Type type) where T : class {
			var a = Activator.CreateInstance(type);
			if(!(a is T)) {
				throw new ArgumentException(string.Format(
				"Instantiation failed ! {0} is not {1}", type, typeof(T)
					));
			}
			return a as T;
		}

		private Assembly[] GetClientAssemblies() {
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			assemblies = assemblies.Where(a => a.GetName().Name != "CliMate").ToArray();
			return assemblies;
		}
		
		void IReflectionFacade.Register(ICliMateModule command) {
			WeakReference<ICliMateModule> conflict = exposedModules.Get(command.name, null);
			if(conflict != null) {
				throw new ArgumentException(String.Format(
					"A command is already registered under [{0}]", command.name		
				));
			}

			exposedModules[command.name] = new WeakReference<ICliMateModule>( command );
		}

		void IReflectionFacade.Deregister(ICliMateModule module) {
			Debug.Assert(exposedModules.ContainsKey(module.name),
				"Tried to deregister root, but it was never registered in the first place");
			exposedModules.Remove(module.name);
		}

		public bool TryGetModule(string name, out ICliMateModule module) {
			WeakReference<ICliMateModule> weakReference = exposedModules.Get(name, null);
			if (weakReference == null) {
				module = null;
                return false;
			} else {
				return exposedModules.Get(name).TryGetTarget(out module);
			}
		}
	}
}
