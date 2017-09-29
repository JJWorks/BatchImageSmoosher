using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrushingLogic.LogicalCrusher
{
    public class JPGCrusher : BaseCrusher
    {
        public JPGCrusher(string LocationofEXE, string WorkDirectory)
            :base (WorkDirectory)
        {
            EXELocation = LocationofEXE;
            BaseArgs = "-copy none -optimize -progressive \"{0}\" \"{1}\"";
        }

        private string _temp1;
        private string _temp2;
        private string _temp3;

        public override string EXELocation
        {
            set { _temp1 = value; }
            get { return _temp1; }
        }

        public override string BaseArgs
        {
            set { _temp2 = value; }
            get { return _temp2; }
        }

        public override string WorkingFolder
        {
            set { _temp3 = value; }
            get { return _temp3; }
        }
    }
}
