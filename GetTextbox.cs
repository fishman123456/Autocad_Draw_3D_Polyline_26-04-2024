using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    public  class GetTextbox
    {
        public string stringsLay = string.Empty;
        public string stringsCoor = string.Empty;
        public void StrToList(string strLay, string strCoor)
        {
            if (strLay != string.Empty)
            {
                // разделитель строк
                string[] separator = new string[] {"\n","\r" };
            // разделяем строку на подстроки по "\n","\r"
            string[] subs = strLay.Split(separator, StringSplitOptions.RemoveEmptyEntries);    
            }
        }
    }
}
