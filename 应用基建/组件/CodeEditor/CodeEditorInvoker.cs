using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace dotNetLab.Common
{
    public class CodeEditerInvoker : Invoker
    {
        MethodInfo PrepareRuntimeMethod;
        String ThisDirectoryPath = null;
       
        readonly string CODE_EDIT_DLL_NAME = @"C:\Users\Public\Documents\Visual Studio 2008\shikii.dotNetLab.Stimulus.dll";

        readonly string VS2008 = @"C:\Users\Public\Documents\Visual Studio 2008";
        readonly string QWhale_Common_dll = @"C:\Users\Public\Documents\Visual Studio 2008\QWhale.Common.dll";
        readonly string QWhale_Editor_dll = @"C:\Users\Public\Documents\Visual Studio 2008\QWhale.Editor.dll";
        readonly string QWhale_Syntax_dll = @"C:\Users\Public\Documents\Visual Studio 2008\QWhale.Syntax.dll";
        readonly string QWhale_Syntax_Parsers_dll = @"C:\Users\Public\Documents\Visual Studio 2008\QWhale.Syntax.Parsers.dll";
        readonly string QWhale_Syntax_Schemes_dll = @"C:\Users\Public\Documents\Visual Studio 2008\QWhale.Syntax.Schemes.dll";
        public CodeEditerInvoker()
        {

            if (!Directory.Exists(VS2008))
            {
                Directory.CreateDirectory(VS2008);
            }
            CheckNeccesoryDllFiles();
            ThisDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath);
            Assembly assembly = Assembly.LoadFrom(CODE_EDIT_DLL_NAME);   //Assembly.LoadFile(CODE_EDIT_DLL_NAME);
            this.Host = assembly.CreateInstance("shikii.Code.CodeEdtiorEx");

        }

        void CheckNeccesoryDllFiles()
        {
            //if (!File.Exists(CODE_EDIT_DLL_NAME))
            //{
            //    AddRef(CODE_EDIT_DLL_NAME, UI.shikii_dotNetLab_Stimulus);
            //}
            //if (!File.Exists(QWhale_Common_dll))
            //{
            //    AddRef(QWhale_Common_dll, UI.QWhale_Common);

            //}
            //if (!File.Exists(QWhale_Editor_dll))
            //{
            //    AddRef(QWhale_Editor_dll, UI.QWhale_Editor);
            //}
            //if (!File.Exists(QWhale_Syntax_dll))
            //{
            //    AddRef(QWhale_Syntax_dll, UI.QWhale_Syntax);
            //}
            //if (!File.Exists(QWhale_Syntax_Parsers_dll))
            //{
            //    AddRef(QWhale_Syntax_Parsers_dll, UI.QWhale_Syntax_Parsers);
            //}
            //if (!File.Exists(QWhale_Syntax_Schemes_dll))
            //{
            //    AddRef(QWhale_Syntax_Schemes_dll, UI.QWhale_Syntax_Schemes);
            //}
        }
        public Control CodeEditControl
        {
            get
            {
                return Host as Control;
            }
        }
        protected override void GetMembers()
        {
            this.type = Host.GetType();
            PrepareRuntimeMethod = type.GetMethod("ForcePrepareRuntime");

        }

       

        public Object ForcePrepareRuntime(string code, String FullClassName)
        {
            return PrepareRuntimeMethod.Invoke(Host, new object[] { code, FullClassName });
        }
        public static Control GetInstance(out CodeEditerInvoker invoker)
        {
              invoker = new CodeEditerInvoker();
            Control codeEditor = invoker.CodeEditControl;
            codeEditor.Dock = DockStyle.Fill;
            return codeEditor;
        }
         
    }
}
