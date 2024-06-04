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
        GetTextbox textbox1 = new GetTextbox();

        [CommandMethod("U84Draw3dPline")]
        public void Draw3dPline()
        {

            if (CountWin.Count == 0)
            {
                Win1 win1 = new Win1();
                win1.Show();
                CountWin.Count++;
            }
            // проверка по дате
            CheckDateWork.CheckDate();
            // пробуем запустить функцию добавления в списки
            textbox1.listAll(GetTextbox.stringsLay, GetTextbox.stringsCoorStart,
                GetTextbox.stringsCoorN, GetTextbox.stringsCoorEnd);
            // переменная для елементов спска 
            int countListN = 0;
            int countListStart = 0;
            int countListEnd = 0;
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

                        // слои пробуем добавить через цикл
                        foreach (string itemLayer in GetTextbox.layList)
                        {
                            LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForRead);

                            if (lt.Has(itemLayer.ToString()))
                            {

                                MessageBox.Show("Такой слой уже есть!" + "/n" + itemLayer.ToString());
                            }
                            else
                            {
                                doc.Editor.WriteMessage("рисуем полилинию");
                                LayerTableRecord ltr = new LayerTableRecord();
                                ltr.Name = itemLayer.ToString().Trim();
                                lt.UpgradeOpen();
                                ObjectId ltId = lt.Add(ltr);
                                tr.AddNewlyCreatedDBObject(ltr, true);
                                db.Clayer = ltId;
                            }
                            // координаты, потом будем передавать списком
                            Point3dCollection pointUser3d = new Point3dCollection();
                            // передаем первую точку в координаты
                            if (GetTextbox.coorListStart[countListStart].ToString() != "")
                            {
                                string[] start = new[] { "" };
                                start = GetTextbox.coorListStart[countListStart].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                var Xstart = double.Parse(start[0]);
                                var Ystart = double.Parse(start[1]);
                                var Zstart = double.Parse(start[2]);
                                pointUser3d.Add(new Point3d(Xstart, Ystart, Zstart));
                                countListStart++;
                            }

                            // передаём координаты из текстбокса coorListN 02-05-2024

                            foreach (var itemCoor in GetTextbox.coorListN)
                            {
                                string[] words = new[] { "" };
                                words = itemCoor.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                var x = double.Parse(words[0]);
                                var y = double.Parse(words[1]);
                                var z = double.Parse(words[2]);
                                pointUser3d.Add(new Point3d(x, y, z));
                                countListN++;
                            }

                            // передаем заключительную точку в координаты
                            if (GetTextbox.coorListEnd[countListEnd].ToString() != "")
                            {
                                string[] end = new[] { "" };
                                end = GetTextbox.coorListEnd[countListEnd].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                var Xend = double.Parse(end[0]);
                                var Yend = double.Parse(end[1]);
                                var Zend = double.Parse(end[2]);
                                pointUser3d.Add(new Point3d(Xend, Yend, Zend));
                                countListEnd++;
                            }
                            //pointUser3d.Add(new Point3d(1000, 0, 0));
                            //pointUser3d.Add(new Point3d(0, 100, 0));
                            //pointUser3d.Add(new Point3d(0, 0, 500));
                            //pointUser3d.Add(new Point3d(500, 0, 0));

                            var p1 = new Polyline3d(Poly3dType.SimplePoly, pointUser3d, false);
                            btr.AppendEntity(p1);
                            btr.Database.TransactionManager.TopTransaction.AddNewlyCreatedDBObject(p1, true);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        doc.Editor.WriteMessage("Error " + ex.Message);
                        tr.Abort();
                    }
                    tr.Commit();
                }
                MessageBox.Show("Добавлено" + countListStart + "обьектов");
                countListN = 0;
                countListStart = 0;
                countListEnd = 0;
                // очищаем списки
                GetTextbox.layList = new List<string>();
                GetTextbox.coorListStart = new List<string>();
                GetTextbox.coorListN = new List<string>();
                GetTextbox.coorListEnd = new List<string>();
    }
        }
    }
}
