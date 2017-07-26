using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SunGard.AvantGard.Solution.Ban.Common
{
    [ServiceContract(Namespace = "wilmar")]
    public interface IOnlineCompany
    {
        [OperationContract]
        string RefreshOnlineCompanyList(string companyCodes);
    }
}
