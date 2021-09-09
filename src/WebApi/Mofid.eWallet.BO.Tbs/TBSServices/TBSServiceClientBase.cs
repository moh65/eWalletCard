using Mofid.eWallet.Domain.Configurations;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

namespace Mofid.eWallet.BO.Tbs.TBSServices
{
	[ExcludeFromCodeCoverage]

	abstract class TBSServiceClientBase<T> where T : class
	{
		protected abstract string Url { get; }

		private BackofficeConfiguration _config;

		public TBSServiceClientBase(BackofficeConfiguration config)
		{
			_config = config;
		}
		protected TBSServiceClientBase<T> Build(ClientBase<T> serviceClient)
		{
			serviceClient.Endpoint.EndpointBehaviors.Add(new FillHeader(_config));
			serviceClient.Endpoint.Address = new EndpointAddress(_config.Url + Url);
			return this;
		}


	}
}
