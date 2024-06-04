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
       public static string stringsLay = string.Empty;
       public static string stringsCoorStart = string.Empty;
       public static string stringsCoorN = string.Empty;
       public static string stringsCoorEnd = string.Empty;
        // список разбитых данных берем из массива
        public static List<string> layList = new List<string>();
        public static List<string> coorListStart = new List<string>();
        public static List<string> coorListN = new List<string>();
        public static List<string> coorListEnd = new List<string>();
        // // массив для хранения данных полученных из textbox
        static string[] massLay = new string[] { };
        static string[] massCoorStart = new string[] { };
        static string[] massCoorN = new string[] { };
        static string[] massCoorEnd = new string[] { };
        // функция для разделения по переносам
        public void listAll(string strLay, string strCoorStart,string strCoorN,string strCoorEnd)
        {
            if (stringsLay != string.Empty)
            {
                // разделитель строк
                string[] separator = new string[] { "\n", "\r" };
                // разделяем строку на подстроки по "\n","\r"
                massLay = strLay.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                 massCoorStart = strCoorStart.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                massCoorN = strCoorN.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                massCoorEnd = strCoorEnd.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            }
            foreach(string item in massLay)
            {
                layList.Add(item);
            }
            foreach (string item in massCoorStart)
            {
                coorListStart.Add(item);
            }
            foreach (string item in massCoorN)
            {
                coorListN.Add(item);
            }
            foreach (string item in massCoorEnd)
            {
                coorListEnd.Add(item);
            }
            
        }

    }
}
