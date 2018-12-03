using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring_0.After
{
    public class MapPrinterDebug : IMapPrinter
    {
        public void Print(Map map)
        {
            Debug.WriteLine("");
            Debug.WriteLine("");
            if (map.IsAnyLive == false)
            {
                Debug.WriteLine("All dead!");
                return;
            }
            IEnumerable<Field> livingFields = map.Fields.Where(x => x.Live);
            int minX = livingFields.Min(x => x.X);
            int maxX = livingFields.Max(x => x.X);
            int minY = livingFields.Min(x => x.Y);
            int maxY = livingFields.Max(x => x.Y);

            foreach(var row in map.Fields.GroupBy(x => x.Y).OrderBy(x => x.Key))
            {
                if (row.Key < minY || row.Key > maxY)
                {
                    continue;
                }
                Debug.WriteLine(PrintRow(row, minX, maxX));
            }
        }

        private string PrintRow(IEnumerable<Field> row, int minX, int maxX)
        {
            StringBuilder sb = new StringBuilder();
            for (int column = minX; column <= maxX; ++column)
            {
                Field field = row.FirstOrDefault(x => x.X == column);
                sb.Append($"{PrintField(field)} ");
            }
            return sb.ToString();
        }

        private string PrintField(Field field)
        {
            if (field != null)
            {
                char ch = field.Live ? '*' : '.';
                 return $"({ch})";
            }
            else
            {
                // is death, cuz doesnt exist
                return "(.)";
            }
        }
    }
}
