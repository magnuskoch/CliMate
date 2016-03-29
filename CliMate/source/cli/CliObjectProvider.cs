using CliMate.enums;
using CliMate.Factories;
using CliMate.interfaces.cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CliMate.source.cli {
	public class CliObjectProvider : ICliObjectProvider {

		private Factory factory;
		private ICliObject cliObject;

		public CliObjectProvider(Factory factory, object application) {
			this.factory = factory;	
			Analyze(application);
		}

		private void Analyze(object application) {

			ICliObject root = factory.Create<ICliObject>();
			root.data = application;
			root.type = CliObjectType.Object;
			root.name = "root";
			root.manual = "No manual implemented for root object";
			root.children = GetChildren(root);
			this.cliObject = root;
		}

		public ICliObject GetCliObject() {
			Debug.Assert(cliObject != null, "Tried to get cliObject, but no analysis has been done yet !");
			return cliObject; 	
		}

		private List<ICliObject> GetChildren(ICliObject cliObject) {
			var children = new List<ICliObject>();

			if(cliObject.type == CliObjectType.Object) {
				children.AddRange( GetObjects(cliObject) );
				children.AddRange( GetMethods(cliObject) );
			} else if(cliObject.type == CliObjectType.Method) {
				children.AddRange( GetValues(cliObject));	
			}

			foreach(ICliObject child in children) {
				child.children = GetChildren(child);
			}

			return children;
		}

		private List<ICliObject> GetObjects(ICliObject cliObject) {
			Debug.Assert(cliObject.type == CliObjectType.Object, "Only objects can have object children");
			var children = new List<ICliObject>();

			FieldInfo[] fieldInfos = cliObject.data.GetType().GetFields();
			foreach(FieldInfo fieldInfo in fieldInfos) {
				List<CliAvailable> cliAvailables = fieldInfo.GetCustomAttributes<CliAvailable>().ToList();
				foreach (CliAvailable cliAvailable in cliAvailables) {
					ICliObject child = GetCliObject(
						CliObjectType.Object, 
						fieldInfo.Name, 
						cliAvailable, fieldInfo.GetValue(cliObject.data));
					children.Add(child);
				}
			}

			PropertyInfo[] propertyInfos = cliObject.data.GetType().GetProperties();
			foreach(PropertyInfo propertyInfo in propertyInfos) {
				List<CliAvailable> cliAvailables = propertyInfo.GetCustomAttributes<CliAvailable>().ToList();
				foreach (CliAvailable cliAvailable in cliAvailables) {
					ICliObject child = GetCliObject(
						CliObjectType.Object, 
						propertyInfo.Name, 
						cliAvailable, propertyInfo.GetValue(cliObject.data));
					children.Add(child);
				}
			}

			return children;
		}

		private List<ICliObject> GetMethods(ICliObject cliObject) {
			Debug.Assert(cliObject.type == CliObjectType.Object, "Only objects can have method children");

			var children = new List<ICliObject>();
			MethodInfo[] methodInfos = cliObject.data.GetType().GetMethods();

			foreach (MethodInfo methodInfo in methodInfos) {
				List<CliAvailable> cliAvailables = methodInfo.GetCustomAttributes<CliAvailable>().ToList();
				foreach (CliAvailable cliAvailable in cliAvailables) {
					ICliObject child = GetCliObject(
						CliObjectType.Method, 
						methodInfo.Name, 
						cliAvailable, methodInfo);
					children.Add(child);
				}
			}
			return children;
		}
		
		private List<ICliObject> GetValues(ICliObject cliObject) {
			Debug.Assert(cliObject.type == CliObjectType.Method, "Only methods can have value children");
			Debug.Assert(cliObject.data is MethodInfo, "Expecting MethodInfo in data field");
			var children = new List<ICliObject>();

			ParameterInfo[] parameters = (cliObject.data as MethodInfo).GetParameters();
			foreach(ParameterInfo parameter in parameters) {
				
				List<CliAvailable> cliAvailables = parameter.GetCustomAttributes<CliAvailable>().ToList();
				foreach (CliAvailable cliAvailable in cliAvailables) {
					ICliObject child = GetCliObject(
						CliObjectType.Value, 
						parameter.Name, 
						cliAvailable, null);
					children.Add(child);
				}
			}


			return children;
		}

		private ICliObject GetCliObject(CliObjectType type, string name, CliAvailable attribute, object data) {

			ICliObject cliObject = factory.Create<ICliObject>();
			cliObject.type = type;
			cliObject.name = name;
			cliObject.alias = new List<string> { attribute.name };
			cliObject.manual = attribute.GetManual();
			cliObject.data = data;

			return cliObject;
		}
	}
}
