using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Resources
{
    public class OtchetRecord
    {

        private Guid id = Guid.NewGuid();
        public Guid GetGuid()
        { return id; }

        private string ispolnitel;
        public string GetIspolnitel()
        { return ispolnitel; }
        public void SetIspolnitel(string value)
        { ispolnitel = value; }

        private int countRKK;
        public int GetCountRKK()
        { return countRKK; }
        public void SetCountRKK()
        { countRKK = countRKK + 1; }

        private int countOBR;
        public int GetCountOBR()
        { return countOBR; }
        public void SetCountOBR()
        { countOBR = countOBR + 1; }

        private int countRKK_OBR;
        public int GetCountRKK_OBR()
        { return countRKK_OBR; }
        public void SetCountRKK_OBR()
        { countRKK_OBR = countRKK_OBR + 1; }

        private int exFile;
        public int GetExFile()
        { return exFile; }
        public void SetExFile(int value)
        { exFile = value; }
    }

}

