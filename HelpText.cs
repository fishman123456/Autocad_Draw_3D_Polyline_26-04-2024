using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocad_Draw_3D_Polyline_26_04_2024
{
    public static class HelpText
    {
         static StringBuilder strHelp = new StringBuilder();

        public static StringBuilder textHelp()
        {
            strHelp.AppendLine("программа для создания 3d полилиний");
            strHelp.AppendLine("первый тексбокс - слой");
            strHelp.AppendLine("второй тексбокс - первая точка");
            strHelp.AppendLine("третий тексбокс - промежуточные точки (может быть пустым");
            strHelp.AppendLine("второй тексбокс - заключительная точка");
            return strHelp;
        }

    }
}
