using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SystemFolder
    {
        private const string DATA = "data";

        private const string RESPONSE = "response";

        private const string RESPONSE_TEMP = "response.temp";

        private const string SOURCE = "source";

        private const string BACKUP = "backup";

        private const string ERROR = "error";

        private const string RUNTIME = "runtime";

        public static readonly string DataFolder = string.Empty;

        public static readonly string ResponseFolder = string.Empty;

        public static readonly string ResponseTempFolder = string.Empty;

        public static readonly string SourceFolder = string.Empty;

        public static readonly string BackupFolder = string.Empty;

        public static readonly string ErrorFolder = string.Empty;

        public static readonly string RuntimeFolder = string.Empty;

        public static readonly string BanksLocation = "Banks";

        static SystemFolder()
        {
            string bin = AppDomain.CurrentDomain.BaseDirectory;
            string parent = Directory.GetParent(bin).Parent.FullName;

            DataFolder = CheckExistOrCreate(parent, DATA);
            ResponseFolder = CheckExistOrCreate(DataFolder, RESPONSE);
            ResponseTempFolder = CheckExistOrCreate(DataFolder, RESPONSE_TEMP);
            SourceFolder = CheckExistOrCreate(DataFolder, SOURCE);
            BackupFolder = CheckExistOrCreate(DataFolder, BACKUP);
            ErrorFolder = CheckExistOrCreate(DataFolder, ERROR);
            RuntimeFolder = CheckExistOrCreate(DataFolder, RUNTIME);
        }

        private static string CheckExistOrCreate(string parentFolder, string folder)
        {
            try
            {
                var dir = Path.Combine(parentFolder, folder);
                if (!Directory.Exists(dir))
                {
                    return Directory.CreateDirectory(dir).FullName;
                }
                return dir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
