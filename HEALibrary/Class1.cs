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
        public MainGate()
        {
            
        }

        public string TestMainGate(string str)
        {
            try
            {
                List<string> fileLines = new List<string>();
                Assembly assembly;
                Type type = null;
                List<MethodInfo> s = null;
                object instanceOfMyType = null;
                object result = null;
                string strResult = "";

                int i = 0;

                foreach (string fileName in Directory.GetFiles("c:\\temp\\Modules\\", "*.dll"))
                {
                    fileLines.Add(fileName);
                }
                i = 0;
                foreach (string fileName in fileLines)
                {
                    assembly = Assembly.LoadFrom(fileName);
                    type = assembly.GetTypes()[0];
                    instanceOfMyType = Activator.CreateInstance(type);
                    s = assembly.GetTypes()[0].GetMethods().ToList();
                    foreach (MethodInfo item in s)
                    {
                        //mf.Text += item.Name.ToString();
                        if (item.Name != "ToString" && item.Name != "Equals" && item.Name != "GetHashCode" && item.Name != "GetType")
                        {

                            ParameterInfo[] parameters = item.GetParameters();
                            object[] parametersArray = new object[] { str };
                            result = item.Invoke(instanceOfMyType, parametersArray);
                        }
                    }
                    strResult += " Dinamic method say:" + result.ToString();
                }

                //MainForm mf = new MainForm();
                //mf.Show();
                //hj

                return "Test succesed: " + str + "DM:" + strResult; 
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
