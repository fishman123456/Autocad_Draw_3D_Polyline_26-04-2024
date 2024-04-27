using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Visibility = System.Windows.Visibility;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    // создаём 3д полилинию
    public class ClassDraw3dPline
    {
        [CommandMethod("U84Draw3dPline")]
        public void Draw3dPline()
        {
            if (CountWin.Count == 0)
            {
                Win1 win1 = new Win1();
                win1.Show();
                CountWin.Count++;
            }
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            // блокируем документ
            using (DocumentLock doclock = Application.DocumentManager.MdiActiveDocument.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {

                        doc.Editor.WriteMessage("рисуем полилинию");
                        BlockTable bt;
                        bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                        BlockTableRecord btr = new BlockTableRecord();
                        btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                        // слои пробуем добавить

                        LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                        if (lt.Has(GetTextbox.stringsLay.ToString()))
                        {
                            doc.Editor.WriteMessage("рисуем полилинию");
                            MessageBox.Show("Такой слой уже есть");
                        }
                        else
                        {
                            LayerTableRecord ltr = new LayerTableRecord();
                            ltr.Name = GetTextbox.stringsLay.ToString();
                            lt.UpgradeOpen();
                            ObjectId ltId = lt.Add(ltr);
                            tr.AddNewlyCreatedDBObject(ltr, true);
                            db.Clayer = ltId;
                        }
                        // координаты, потом будем передавать списком
                        Point3dCollection pointUser3d = new Point3dCollection();
                        pointUser3d.Add(new Point3d(1000, 0, 0));
                        pointUser3d.Add(new Point3d(0, 100, 0));
                        pointUser3d.Add(new Point3d(0, 0, 500));
                        pointUser3d.Add(new Point3d(500, 0, 0));
                        var p1 = new Polyline3d(Poly3dType.SimplePoly, pointUser3d, false);
                        btr.AppendEntity(p1);
                        btr.Database.TransactionManager.TopTransaction.AddNewlyCreatedDBObject(p1, true);
                    }
                    catch (System.Exception ex)
                    {
                        doc.Editor.WriteMessage("Error " + ex.Message);
                        tr.Abort();
                    }
                    tr.Commit();
                }
            }
        }
    }
}
