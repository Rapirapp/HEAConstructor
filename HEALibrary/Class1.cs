using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Windows.Forms; 

namespace HEALibrary
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class MainGate
    {
        private Dictionary<string, KeyValuePair<string, ParameterInfo[]>> gates = new Dictionary<string,KeyValuePair<string,ParameterInfo[]>>();
        private Dictionary<string, Assembly> _assambly = new Dictionary<string, Assembly>();
        public void InitializeMainGate()
        {
            List<string> fileLines = new List<string>();
            Assembly assembly;
            List<MethodInfo> s = null;
            
            foreach (string fileName in Directory.GetFiles("c:\\temp\\Modules\\", "*.dll"))
            {
                fileLines.Add(fileName);
            }
                       
            foreach (string fileName in fileLines)
            {
                assembly = Assembly.LoadFrom(fileName);
                s = assembly.GetTypes()[0].GetMethods().ToList();
                _assambly.Add(assembly.GetTypes()[0].Name, assembly); 
                foreach (MethodInfo item in s)
                {
                    if (item.Name != "ToString" && item.Name != "Equals" && item.Name != "GetHashCode" && item.Name != "GetType")
                    {
                        gates.Add(item.Name, new KeyValuePair<string, ParameterInfo[]>(assembly.GetTypes()[0].Name, item.GetParameters()));   
                    }
                }
            }
        }

        public string TestMainGate(string str, string arg)
        {
            try
            {
                
                Assembly assembly;
                Type type = null;
                List<MethodInfo> s = null;
                object instanceOfMyType = null;
                object result = null;
                string strResult = "";
                if (gates.ContainsKey(str))
                {
                    ParameterInfo[] par = gates[str].Value;
                    assembly = _assambly[gates[str].Key];
                    type = assembly.GetTypes()[0];
                    instanceOfMyType = Activator.CreateInstance(type);
                    object[] parametersArray = new object[] { str };
                    result = assembly.GetTypes()[0].GetMethod(str).Invoke(instanceOfMyType, parametersArray);
                    strResult = "Test succesed: " + str + "DM:" + " Dinamic method say:" + result.ToString();
                }
                else
                {
                    strResult = "Function not found!";
                }
                return strResult; 
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public Dictionary<string, KeyValuePair<string, ParameterInfo[]>> Gates
        {
            get
            {
                return gates;
            }
        }

    }
}
