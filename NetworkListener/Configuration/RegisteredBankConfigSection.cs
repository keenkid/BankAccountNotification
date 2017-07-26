//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Configuration;

//namespace SunGard.AvantGard.Solution.BankAccountNotification.NetworkListener.Configuration
//{
//    public class RegisteredBankConfigSection : ConfigurationSection
//    {
//        private static object obj = new object();

//        protected RegisteredBankConfigSection() { }

//        private static RegisteredBankConfigSection __instance = null;

//        [ConfigurationProperty("", IsDefaultCollection = true)]
//        public BankCollection RegBanks
//        {
//            get { return (BankCollection)this[""]; }
//        }

//        public static RegisteredBankConfigSection RegisteredBanks
//        {
//            get
//            {
//                lock (obj)
//                {
//                    if (null == __instance)
//                    {
//                        string asmLocation = typeof(RegisteredBankConfigSection).Assembly.Location;
//                        var config = ConfigurationManager.OpenExeConfiguration(asmLocation);
//                        __instance = config.GetSection("RegisteredBanks") as RegisteredBankConfigSection;
//                    }
//                    return __instance;
//                }
//            }
//        }

//        public BankCollection Banks
//        {
//            get
//            {
//                return RegisteredBanks.RegBanks;
//            }
//        }
//    }

//    /// <summary>
//    /// 银行
//    /// </summary>
//    public class Bank : ConfigurationElement
//    {
//        [ConfigurationProperty("bankCode", IsKey = true, IsRequired = true)]
//        public string BankCode
//        {
//            get { return (string)this["bankCode"]; }
//            private set { this["bankCode"] = value; }
//        }

//        [ConfigurationProperty("bankName", IsKey = false, IsRequired = true)]
//        public string BankName
//        {
//            get { return (string)this["bankName"]; }
//            set { this["bankName"] = value; }
//        }

//        [ConfigurationProperty("corpID", IsKey = false, IsRequired = true)]
//        public string CorpID
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["corpID"]); }
//            set { this["corpID"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("userID", IsKey = false, IsRequired = true)]
//        public string UserID
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["userID"]); }
//            set { this["userID"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("password", IsKey = false, IsRequired = true)]
//        public string Password
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["password"]); }
//            set { this["password"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("certID", IsKey = false, IsRequired = false)]
//        public string CertID
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["certID"]); }
//            set { this["certID"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("certPassword", IsKey = false, IsRequired = false)]
//        public string CertPassword
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["certPassword"]); }
//            set { this["certPassword"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("supportBankCodes", IsKey = false, IsRequired = true)]
//        private string SupportBankCode
//        {
//            get { return (string)this["supportBankCodes"]; }
//        }

//        public string[] SupportBankCodes
//        {
//            get
//            {
//                if (string.IsNullOrEmpty(SupportBankCode))
//                {
//                    return BankCode.Split(',');
//                }
//                else
//                {
//                    return SupportBankCode.Split(',');
//                }
//            }
//        }

//        [ConfigurationProperty("BankServices", IsKey = false, IsRequired = false)]
//        public BankServiceNode BankSrv
//        {
//            get { return (BankServiceNode)this["BankServices"]; }
//        }

//        [ConfigurationProperty("TransactionList", IsKey = false, IsRequired = true)]
//        public TransactionListNode TransListNode
//        {
//            get { return (TransactionListNode)this["TransactionList"]; }
//        }

//        [ConfigurationProperty("transInterval", IsKey = false, IsRequired = false)]
//        public string TransInterval
//        {
//            get { return (string)this["transInterval"]; }
//        }

//        [ConfigurationProperty("transactionObservers", IsKey = false, IsRequired = false)]
//        public TransactionObserverElementCollection TransactionObservers
//        {
//            get
//            {
//                return base["transactionObservers"] as TransactionObserverElementCollection;
//            }
//        }

//        [ConfigurationProperty("logger", IsKey = false, IsRequired = false)]
//        public LoggerElement Logger
//        {
//            get
//            {
//                return base["logger"] as LoggerElement;
//            }
//        }

//        protected override void PostDeserialize()
//        {
//            base.PostDeserialize();

//            TransListNode.Bank = this;
//        }

//        public override bool IsReadOnly()
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// 银行集合
//    /// </summary>
//    public class BankCollection : ConfigurationElementCollection
//    {
//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new Bank();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((Bank)element).BankCode;
//        }

//        protected override string ElementName
//        {
//            get { return "Bank"; }
//        }

//        public override ConfigurationElementCollectionType CollectionType
//        {
//            get { return ConfigurationElementCollectionType.BasicMap; }
//        }

//        public new Bank this[string bankCode]
//        {
//            get { return (Bank)BaseGet(bankCode); }
//        }

//        public Bank this[int index]
//        {
//            get { return (Bank)BaseGet(index); }
//        }
//    }

//    /// <summary>
//    /// 银行服务
//    /// </summary>
//    public class BankServiceNode : ConfigurationElement
//    {
//        [ConfigurationProperty("signService", IsRequired = false, IsKey = false)]
//        public SignService SignSrv
//        {
//            get { return (SignService)this["signService"]; }
//        }

//        [ConfigurationProperty("service", IsRequired = true, IsKey = false)]
//        public Service Srv
//        {
//            get { return (Service)this["service"]; }
//        }

//        [ConfigurationProperty("verifyService", IsRequired = false, IsKey = false)]
//        public VerifySignService VerifySrv
//        {
//            get { return (VerifySignService)this["verifyService"]; }
//        }
//    }

//    /// <summary>
//    /// 签名服务
//    /// </summary>
//    public class SignService : ConfigurationElement
//    {
//        [ConfigurationProperty("type", IsRequired = true, IsKey = false)]
//        public string ServiceType
//        {
//            get { return (string)this["type"]; }
//        }

//        [ConfigurationProperty("ip", IsRequired = true, IsKey = false)]
//        public string IP
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["ip"]); }
//            set { this["ip"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("port", IsRequired = true, IsKey = false)]
//        public string Port
//        {
//            get { return (string)this["port"]; }
//            set { this["port"] = value; }
//        }

//        [ConfigurationProperty("timeout", IsRequired = true, IsKey = false)]
//        public string Timeout
//        {
//            get { return (string)this["timeout"]; }
//            set { this["timeout"] = value; }
//        }

//        [ConfigurationProperty("path", IsRequired = false, IsKey = false)]
//        public string Path
//        {
//            get
//            {
//                return base["path"] as string;
//            }
//            set
//            {
//                base["path"] = value;
//            }
//        }

//        public override bool IsReadOnly()
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// 发送请求服务

//    /// </summary>
//    public class Service : ConfigurationElement
//    {
//        [ConfigurationProperty("type", IsRequired = true, IsKey = false)]
//        public string ServiceType
//        {
//            get { return (string)this["type"]; }
//        }

//        [ConfigurationProperty("ip", IsRequired = true, IsKey = false)]
//        public string IP
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["ip"]); }
//            set { this["ip"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("port", IsRequired = true, IsKey = false)]
//        public string Port
//        {
//            get { return (string)this["port"]; }
//            set { this["port"] = value; }
//        }

//        [ConfigurationProperty("timeout", IsRequired = true, IsKey = false)]
//        public string Timeout
//        {
//            get { return (string)this["timeout"]; }
//            set { this["timeout"] = value; }
//        }

//        [ConfigurationProperty("path", IsRequired = false, IsKey = false)]
//        public string Path
//        {
//            get
//            {
//                return base["path"] as string;
//            }
//            set
//            {
//                base["path"] = value;
//            }
//        }

//        public override bool IsReadOnly()
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// 验证签名服务
//    /// </summary>
//    public class VerifySignService : ConfigurationElement
//    {
//        [ConfigurationProperty("type", IsRequired = true, IsKey = false)]
//        public string ServiceType
//        {
//            get { return (string)this["type"]; }
//        }

//        [ConfigurationProperty("ip", IsRequired = true, IsKey = false)]
//        public string IP
//        {
//            get { return this.GetDecryptedPropertyValue((string)this["ip"]); }
//            set { this["ip"] = this.GetEncryptedPropertyValue(value); }
//        }

//        [ConfigurationProperty("port", IsRequired = true, IsKey = false)]
//        public string Port
//        {
//            get { return (string)this["port"]; }
//            set { this["port"] = value; }
//        }

//        [ConfigurationProperty("timeout", IsRequired = true, IsKey = false)]
//        public string Timeout
//        {
//            get { return (string)this["timeout"]; }
//            set { this["timeout"] = value; }
//        }

//        [ConfigurationProperty("path", IsRequired = false, IsKey = false)]
//        public string Path
//        {
//            get
//            {
//                return base["path"] as string;
//            }
//            set
//            {
//                base["path"] = value;
//            }
//        }

//        public override bool IsReadOnly()
//        {
//            return false;
//        }
//    }

//    /// <summary>
//    /// TransactionList 节点
//    /// </summary>
//    public class TransactionListNode : ConfigurationElement
//    {
//        [ConfigurationProperty("", IsDefaultCollection = true)]
//        public TransactionCollection TransCollection
//        {
//            get { return (TransactionCollection)base[""]; }
//        }

//        public Bank Bank
//        {
//            set
//            {
//                TransCollection.Bank = value;
//            }
//        }
//    }

//    /// <summary>
//    /// 交易类

//    /// </summary>
//    public class Transaction : ConfigurationElement
//    {
//        private Bank _bank;

//        [ConfigurationProperty("transCode", IsKey = true, IsRequired = true)]
//        public string TransCode
//        {
//            get { return (string)this["transCode"]; }
//        }

//        [ConfigurationProperty("transName", IsKey = false, IsRequired = false)]
//        public string TransName
//        {
//            get { return (string)this["transName"]; }
//        }

//        [ConfigurationProperty("transInterval", IsKey = false, IsRequired = false)]
//        public string TransInterval
//        {
//            get
//            {
//                var tmp = this["transInterval"] as string;

//                if (string.IsNullOrEmpty(tmp))
//                {
//                    tmp = _bank.TransInterval;
//                }

//                return tmp;
//            }
//        }

//        [ConfigurationProperty("request", IsRequired = true)]
//        public RequestNode RequestTrans
//        {
//            get { return (RequestNode)this["request"]; }
//        }

//        [ConfigurationProperty("response", IsRequired = true)]
//        public ResponseNode ResponseTrans
//        {
//            get { return (ResponseNode)this["response"]; }
//        }

//        [ConfigurationProperty("transactionObservers", IsKey = false, IsRequired = false)]
//        public TransactionObserverElementCollection TransactionObservers
//        {
//            get
//            {
//                return base["transactionObservers"] as TransactionObserverElementCollection;
//            }
//        }

//        [ConfigurationProperty("logger", IsKey = false, IsRequired = false)]
//        public LoggerElement Logger
//        {
//            get
//            {
//                return base["logger"] as LoggerElement;
//            }
//        }

//        public Bank Bank
//        {
//            set
//            {
//                _bank = value;
//            }
//        }
//    }

//    /// <summary>
//    /// 交易类集合

//    /// </summary>
//    public class TransactionCollection : ConfigurationElementCollection
//    {
//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new Transaction();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((Transaction)element).TransCode;
//        }

//        protected override string ElementName
//        {
//            get { return "transaction"; }
//        }

//        public override ConfigurationElementCollectionType CollectionType
//        {
//            get { return ConfigurationElementCollectionType.BasicMap; }
//        }

//        public new Transaction this[string transCode]
//        {
//            get { return (Transaction)BaseGet(transCode); }
//        }

//        public Transaction this[int index]
//        {
//            get { return (Transaction)BaseGet(index); }
//        }

//        public Bank Bank
//        {
//            set
//            {
//                foreach (Transaction tx in this)
//                {
//                    tx.Bank = value;
//                }
//            }
//        }
//    }

//    /// <summary>
//    /// request 信息节点
//    /// </summary>
//    public class RequestNode : ConfigurationElement
//    {
//        [ConfigurationProperty("", IsDefaultCollection = true)]
//        public VariantCollection VarCollection
//        {
//            get { return (VariantCollection)base[""]; }
//        }

//        [ConfigurationProperty("templateFile", IsRequired = true, IsKey = false)]
//        public string TemplateFilePath
//        {
//            get { return (string)this["templateFile"]; }
//        }

//        [ConfigurationProperty("multiNodeTag", IsRequired = true, IsKey = false)]
//        public string MultiNodeTag
//        {
//            get { return (string)this["multiNodeTag"]; }
//        }

//    }

//    /// <summary>
//    /// response 信息节点
//    /// </summary>
//    public class ResponseNode : ConfigurationElement
//    {
//        [ConfigurationProperty("", IsDefaultCollection = true)]
//        public VariantCollection VarCollection
//        {
//            get { return (VariantCollection)base[""]; }
//        }

//        [ConfigurationProperty("responseFileName", IsRequired = true, IsKey = false)]
//        public string ResponseFileName
//        {
//            get { return (string)this["responseFileName"]; }
//        }

//        [ConfigurationProperty("templateFile", IsRequired = true, IsKey = false)]
//        public string TemplateFile
//        {
//            get { return (string)this["templateFile"]; }
//        }


//        [ConfigurationProperty("multiNodeTag", IsRequired = true, IsKey = false)]
//        public string MultiNodeTag
//        {
//            get { return (string)this["multiNodeTag"]; }
//        }
//    }

//    /// <summary>
//    /// var字段 类定义

//    /// </summary>
//    public class VariantField : ConfigurationElement
//    {
//        /// <summary>
//        /// 描述字段
//        /// </summary>
//        [ConfigurationProperty("desc", IsKey = false, IsRequired = true)]
//        public string Desc
//        { get { return (string)this["desc"]; } }

//        /// <summary>
//        /// 请求xml中对应的字段
//        /// </summary>
//        [ConfigurationProperty("tag", IsKey = false, IsRequired = true)]
//        public string Tag
//        { get { return (string)this["tag"]; } }

//        /// <summary>
//        /// 模板中待替换的字段

//        /// </summary>
//        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
//        public string Name
//        { get { return (string)this["name"]; } }

//        /// <summary>
//        /// 对字段值做适当的修改

//        /// </summary>
//        [ConfigurationProperty("type", IsKey = false, IsRequired = false)]
//        public string Type
//        { get { return (string)this["type"]; } }

//        [ConfigurationProperty("path", IsKey = false, IsRequired = false)]
//        public string Path
//        { get { return (string)this["path"]; } }
//    }

//    /// <summary>
//    /// var字段 集合定义
//    /// </summary>
//    public class VariantCollection : ConfigurationElementCollection
//    {
//        protected override ConfigurationElement CreateNewElement()
//        {
//            return new VariantField();
//        }

//        protected override object GetElementKey(ConfigurationElement element)
//        {
//            return ((VariantField)element).Name;
//        }

//        protected override string ElementName
//        {
//            get { return "var"; }
//        }

//        public override ConfigurationElementCollectionType CollectionType
//        {
//            get { return ConfigurationElementCollectionType.BasicMap; }
//        }

//        public new VariantField this[string name]
//        {
//            get { return (VariantField)BaseGet(name); }
//        }

//        public VariantField this[int index]
//        {
//            get { return (VariantField)BaseGet(index); }
//        }
//    }
//}
