using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelStrategy.Model
{
    public class SignalData
    {
        public SignalData(int rowNumb, string signalType, string bsy, string bpy1, string bpy2, double level, double lyft, double cancelSignal, DateTime now, string name)
        {
            RowNumber = rowNumb;
            SignalType = signalType;
            DateBsy = bsy;
            DateBpy1 = bpy1;
            DateBpy2 = bpy2;
            Level = level;
            Lyft = lyft;
            CancelSignal = cancelSignal;
            TimeNow = now;
            NameSecurity = name;
        }
        public int RowNumber { get; set; }

        public string SignalType { get; set; }

        public string DateBsy { get; set; }

        public string DateBpy1 { get; set; }

        public string DateBpy2 { get; set; }

        public double Level { get; set; }

        public double Lyft { get; set; }

        public double CancelSignal { get; set; }

        public DateTime TimeNow { get; set; }

        public string NameSecurity { get; set; }
    }
}
