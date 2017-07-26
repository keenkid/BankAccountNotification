using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    public interface IFieldFormat
    {
        string Name { get; }

        string Convert(XDocument xdoc, string source, string parameters);
    }
}
