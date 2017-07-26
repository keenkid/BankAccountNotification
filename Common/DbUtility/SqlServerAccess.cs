using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    public class SqlServerAccess : IDbAccess
    {
        string _connectionString = string.Empty;

        private const string SELECT_ACCOUNTNO = "SELECT CB.AccountNo FROM vrpCorpBank CB INNER JOIN trsBUNIT BU ON CB.OwningBUnit = BU.BUnit AND BU.IsActive = -1 WHERE CB.CashPool='REC-CNY' AND CB.IsActive = -1 AND CB.OwningBUnit IN ({0}) AND CB.BSBNo IN ({1})";

        public SqlServerAccess()
        {
            //_connectionString = ConnectionStringProvider.Instance.ConnectionString;
            _connectionString = @"Server=AP-CHN-LP150074\SQLEXPRESS;Database=DOTCOM82;Integrated Security=SSPI";
        }

        private DataTable QueryData(string selectCommandText)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(selectCommandText))
            {
                return dt;
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(selectCommandText, _connectionString))
            {
                sda.Fill(dt);
                return dt;
            }
        }

        public List<string> GetOnlineAccounts()
        {
            try
            {
                string companyCodes = OnlineCompanyCodeProvider.Instance.GetOnlineCompanyCode();
                string bankCode = OnlineBanksProvider.Instance.GetOnlineBankCode();

                if (string.IsNullOrEmpty(companyCodes) || string.IsNullOrEmpty(bankCode))
                {
                    throw new Exception("There is no online company or online bank.");
                }

                DataTable dt = QueryData(string.Format(SELECT_ACCOUNTNO, companyCodes, bankCode));

                List<string> accounts = new List<string>();

                foreach (DataRow item in dt.Rows)
                {
                    accounts.Add(item["AccountNo"].ToString());
                }
                return accounts;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
