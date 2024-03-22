using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_WTBG
{
    [Transaction(TransactionMode.Manual)]
    class Plugin : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //MainWindow wpf = new MainWindow();//实例化主窗口类
            UIApplication uiApp = commandData.Application;
            UIDocument uidoc = uiApp.ActiveUIDocument;
            Document doc = uidoc.Document;
            SysCache.Instance.ModelName = doc.Title;
            MainWindow.Instance.ShowDialog();//展示界面

            return Result.Succeeded;
        }
    }
}
