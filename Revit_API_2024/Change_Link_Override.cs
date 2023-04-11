using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Try_Revit_2024
{
    [TransactionAttribute(TransactionMode.Manual)]
    public class Change_Link_Override : IExternalCommand

    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            Document doc = uidoc.Document;

            try
            {



                using (Transaction tns = new Transaction(doc, "Change Link Override"))
                {
                    tns.Start();

                    var links = new FilteredElementCollector(doc).OfClass(typeof(RevitLinkType)).ToElementIds();



                    RevitLinkGraphicsSettings revit_link = new RevitLinkGraphicsSettings();
                    revit_link.LinkVisibilityType = LinkVisibility.ByHostView;


                    View Current_view = doc.ActiveView;


                    foreach (var item in links)
                    {

                        ElementId id = new ElementId(item.Value);
                        Current_view.SetLinkOverrides(id, revit_link);



                    }

                    tns.Commit();


                    TaskDialog.Show("Override Links", "All Links are overrides");

                }







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
