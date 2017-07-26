using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;

namespace SunGard.AvantGard.Solution.Ban.Formatting
{
    public class FormatManager
    {
        protected static Dictionary<string, IFieldFormat> __formatters = null;

        static FormatManager()
        {
            if (null == __formatters)
            {
                __formatters = new Dictionary<string, IFieldFormat>();
            }

            FetchAllFormatters();
        }

        private static void FetchAllFormatters()
        {
            Assembly asm = Assembly.GetCallingAssembly();
            var types = from t in asm.GetTypes()
                        where typeof(IFieldFormat).IsAssignableFrom(t)
                        && t.IsClass
                        && null != t.GetProperty("Name")
                        select t;
            foreach (Type t in types)
            {
                IFieldFormat formatter = Activator.CreateInstance(t) as IFieldFormat;
                __formatters[formatter.Name.ToUpper()] = formatter;
            }
        }

        public static string GetFormattedValue(XDocument xdoc, string source, string signature)
        {
            try
            {
                string methodNameList;
                string result;
                ParseMethodSignature(signature, out methodNameList, out result);

                Stack<IFieldFormat> methodStack = PrepareMethod(methodNameList);

                IFieldFormat method = null;
                while (methodStack.Count > 0)
                {
                    method = methodStack.Pop();
                    result = method.Convert(xdoc, source, result);
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        private static Stack<IFieldFormat> PrepareMethod(string methodNameList)
        {
            var methodArray = methodNameList.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            Stack<IFieldFormat> methodStack = new Stack<IFieldFormat>();
            foreach (string methodName in methodArray)
            {
                if (null == __formatters[methodName.ToUpper()])
                {
                    throw new Exception("method name not exists");
                }
                else
                {
                    methodStack.Push(__formatters[methodName.ToUpper()]);
                }
            }
            return methodStack;
        }

        private static void ParseMethodSignature(string signature, out string methodNameList, out string result)
        {
            int leftSquarePos = signature.IndexOf("[");
            int rightSquarePosg = signature.IndexOf("]");

            methodNameList = string.Empty;
            result = string.Empty;

            if (leftSquarePos == -1 && leftSquarePos == rightSquarePosg)
            {
                methodNameList = signature;
                result = string.Empty;
            }
            else if (leftSquarePos != -1 && rightSquarePosg > leftSquarePos)
            {
                result = signature.Substring(leftSquarePos + 1, rightSquarePosg - leftSquarePos - 1);
                methodNameList = signature.Substring(0, leftSquarePos);
            }
            else
            {
                throw new Exception("method signature is invalid.");
            }
        }
    }
}
