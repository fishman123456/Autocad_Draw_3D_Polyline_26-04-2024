
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    public class ChangePolylineVertex
    {
        [CommandMethod("ChangePolyline")]
        public void ChangePolyline()
        {
            // Запрос у пользователя выбрать полилинию
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            PromptEntityOptions peo = new PromptEntityOptions("\nВыберите полилинию: ");
            peo.SetRejectMessage("\nЭто не полилиния.");
            peo.AddAllowedClass(typeof(Polyline3d), true);
            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK) return;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // Открытие выбранной полилинии
                Polyline3d pline = tr.GetObject(per.ObjectId, OpenMode.ForWrite) as Polyline3d;

                // Запрос у пользователя выбрать номер вершины для изменения координат
                PromptIntegerOptions pio = new PromptIntegerOptions("\nВведите номер вершины для изменения координат: ");
                pio.LowerLimit = 0;
                //pio.UpperLimit = ((decimal)pline.StartPoint.Z);
                PromptIntegerResult pir = ed.GetInteger(pio);

                if (pir.Status != PromptStatus.OK) return;

                // Запрос у пользователя ввести новые координаты вершины
                PromptPointOptions ppo = new PromptPointOptions("\nВведите новые координаты вершины: ");
                PromptPointResult ppr = ed.GetPoint(ppo);

                if (ppr.Status != PromptStatus.OK) return;

                // Установка новых координат вершины
               // object value = pline.SetPointAt(pir.Value, new Point3d(ppr.Value.X, ppr.Value.Y, ppr.Value.Z));

                tr.Commit();
            }
        }
    }
}