using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {

            ArrayList arryList1 = new ArrayList();
            arryList1.Add(1);
            arryList1.Add("Two");
            arryList1.Add(3);
            arryList1.Add(4.5);

            IList arryList2 = new ArrayList()
{
    100, 200
};

            //Adding entire collection using ArrayList.AdRange() method.
            ////Note: IList does not contain AddRange() method.
            arryList1.AddRange(arryList2);
        }
    }
}
