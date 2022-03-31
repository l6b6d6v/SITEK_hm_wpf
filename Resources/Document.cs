using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Resources
{

    internal class Document
    {
        private Guid id = Guid.NewGuid();
        public Guid GetGuid()
        { return id; }


        private string ruk;
        public string GetRuk()
        {
            if (ruk is null)
                ruk = "NULL";

            return ruk;
        }
        public void SetRuk(string value)
        { ruk = value; }

        private string otv;
        public string GetOtv()
        {
            if (otv is null)
                otv = "NULL";

            return otv;
        }
        public void SetOtv(string value)
        { otv = value; }

        private int exFile;

        public int GetExFile() 
        { return exFile; }
        public void SetExFile(int value)
        { exFile = value; }
    }

}
