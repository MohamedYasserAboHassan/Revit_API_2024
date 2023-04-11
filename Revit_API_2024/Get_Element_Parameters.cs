using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try_Revit_2024
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class Get_Element_Parameters : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;

            try
            {






                var ref_element = uidoc.Selection.PickObject(ObjectType.Element);

                var element = doc.GetElement(ref_element);

                StringBuilder sb = new StringBuilder();
                foreach (var item in element.EvaluateAllParameterValues())
                {
                    sb.AppendLine(item.Definition.Name + "=" + item.AsValueString(doc));

                }

                TaskDialog.Show("Parameters", sb.ToString());















                return Result.Succeeded;

            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }



        }
    }
}
