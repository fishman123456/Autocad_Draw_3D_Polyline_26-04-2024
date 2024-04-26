using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    // создаём 3д полилинию
    public class ClassDraw3dPline
    {
        [CommandMethod("U84Draw3dPline")]
        public void Draw3dPline()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Win1 win1 = new Win1();
            win1.Show();
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    doc.Editor.WriteMessage("рисуем полилинию");
                    BlockTable bt;
                    bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                    BlockTableRecord btr;
                    btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                    // слои пробуем добавить
                    LayerTableRecord ltr = new LayerTableRecord();
                    LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId,OpenMode.ForRead);
                    lt.UpgradeOpen();
                    ltr.Name = "dsfds";
                    ObjectId ltId = lt.Add(ltr);
                    tr.AddNewlyCreatedDBObject(ltr, true);
                    db.Clayer = ltId;
                    
                    // координаты, потом будем передавать списком
                    Point3dCollection pointUser3d = new Point3dCollection();
                    pointUser3d.Add(new Point3d(1000, 0, 0));
                    pointUser3d.Add(new Point3d(0, 100, 0));
                    pointUser3d.Add(new Point3d(0, 0, 500));
                    pointUser3d.Add(new Point3d(500, 0, 0));
                    var p1 = new Polyline3d(Poly3dType.SimplePoly, pointUser3d, false);
                    var p2 = new Polyline3d(Poly3dType.SimplePoly, pointUser3d, false);
                    //tr.AddNewlyCreatedDBObject(p1,true);
                    btr.AppendEntity(p1);
                    btr.AppendEntity(p2);
                    btr.Database.TransactionManager.TopTransaction.AddNewlyCreatedDBObject(p1, true);
                    btr.Database.TransactionManager.TopTransaction.AddNewlyCreatedDBObject(p2, true);
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
