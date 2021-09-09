using Mofid.eWallet.Domain.Configurations;
using System;
using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Mofid.eWallet.BO.Tbs
{
	[ExcludeFromCodeCoverage]

	class FillHeader : IClientMessageInspector, IEndpointBehavior
	{
		private string address;
		private BackofficeConfiguration _config;
		public FillHeader(string address)
		{
			this.address = address;
		}

		public FillHeader(BackofficeConfiguration config)
		{
			_config = config;
		}
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			var d = new BackofficeService2.CustomHeader();
			d.UserName = _config.Username;
			d.Password = _config.Password;

			const string headerName = "CustomHeaderMessage";
			const string headerNamespace = "TadbirPardaz.TBS/PrincipalHeader";
			var header = d;
			var customHeader = MessageHeader.CreateHeader(headerName,
														  headerNamespace, header);
			request.Headers.Add(customHeader);
			return request;
		}

		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
		}



		public void Validate(ServiceEndpoint endpoint)
		{
		}

		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.ClientMessageInspectors.Add(this);
		}
	}
}
