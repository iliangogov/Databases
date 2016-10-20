using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace HomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load("../../students.xsl");
            xslt.Transform("../../students.xml", "../../students.html");
        }
    }
}
