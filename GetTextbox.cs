using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
   
    public  class GetTextbox
    {
        // 

        // строка для хранения данных полученных из textbox
        public static string stringsLay = "тест";
        public static string stringsCoor = string.Empty;
        // список разбитых данных берем из массива
        public  List<string> layList = new List<string>();
        public  List<string> coorList = new List<string>();
        // // массив для хранения данных полученных из textbox
        public string[] massLay = new string[] { };
        public string[] massCoor = new string[] { };
        // функция для разделения по переносам

        public  List<string> StrToList(string strLay, string strCoor)
        {
            
            if (strLay != string.Empty)
            {
                // разделитель строк
                string[] separator = new string[] { "\n", "\r" };
                // разделяем строку на подстроки по "\n","\r"
                massLay = strLay.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                 massCoor = strCoor.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach(string item in massLay)
            {
                layList.Add(item);
            }
            foreach (string item in massCoor)
            {
                coorList.Add(item);
            }
            return coorList;
        }
    }
}
