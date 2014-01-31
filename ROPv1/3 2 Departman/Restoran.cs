using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1
{
    [Serializable]
    public class Restoran
    {
        private string departmanAdi;
        private string departmanMenusu;
        private string departmanEkrani;
        private string departmanDeposu;

        public string DepartmanAdi
        {
            get { return departmanAdi; }
            set { departmanAdi = value; }
        }

        public string DepartmanMenusu
        {
            get { return departmanMenusu; }
            set { departmanMenusu = value; }
        }

        public string DepartmanEkrani
        {
            get { return departmanEkrani; }
            set { departmanEkrani = value; }
        }

        public string DepartmanDeposu
        {
            get { return departmanDeposu; }
            set { departmanDeposu = value; }
        }
    }
}
