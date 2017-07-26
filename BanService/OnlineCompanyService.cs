using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunGard.AvantGard.Solution.Ban.Common;
using System.Text.RegularExpressions;
using SunGard.AvantGard.Solution.Ban.RfcInterface;

namespace SunGard.AvantGard.Solution.Ban.BanService
{
    public class OnlineCompanyService : IOnlineCompany
    {
        public string RefreshOnlineCompanyList(string companyCodes)
        {
            try
            {
                if (CompanyCodesValidation(companyCodes))
                {
                    OnlineCompanyCodeProvider.Instance.FlushOnlineCompanyCode(companyCodes);

                    RfcFactory.OnlineAccounts = OnlineAccountsProvider.Instance.GetOnlineAccounts();

                    return string.Empty;
                }
                else
                {
                    return "Part of company codes not invalid.";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private bool CompanyCodesValidation(string companyCodes)
        {
            string regstr = "^('[A-Z0-9]+?',)+$";
            companyCodes = companyCodes + ",";
            if (Regex.IsMatch(companyCodes, regstr))
            {
                return true;
            }
            return false;
        }
    }
}
