//using BackofficeService1;
//using Microsoft.Extensions.Options;
//using Mofid.eWallet.BO.Tbs.TBSServices;
//using Mofid.eWallet.Domain.Configurations;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using static BackofficeService1.CustomerClubExternalServiceClient;

//namespace Mofid.eWallet.BO.Tbs.TBSServices
//{
//    class CustomerRemainTBSService : TBSServiceClientBase<ICustomerClubExternalService>
//    {
//        public bool Initialized { get; set; } = false;
//        public CustomerRemainTBSService(IOptions<BackofficeConfiguration> config) : base(config.Value) { }

//        private CustomerClubExternalServiceClient _instance;
//        public CustomerClubExternalServiceClient Instance
//        {
//            get
//            {
//                if (!Initialized)
//                {
//                    _instance = new CustomerClubExternalServiceClient(EndpointConfiguration.BasicHttpBinding_ICustomerClubExternalService1);
//                    Build(_instance);
//                    Initialized = true;
//                }
//                return _instance;
//            }
//        }
//        protected override string Url { get => "/TadbirPardaz.CustomerClubExternalService"; }
//    }

//}
