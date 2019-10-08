// Copyright 2007-2018 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.RabbitMqTransport.Configurators
{
    using System;
    using System.Net.Security;
    using System.Security.Authentication;
    using System.Security.Cryptography.X509Certificates;
    using Metadata;
    using RabbitMQ.Client;
    using Util;


    class ConfigurationHostSettings :
        RabbitMqHostSettings
    {
        readonly Lazy<Uri> _hostAddress;

        public ConfigurationHostSettings()
        {
            var defaultOptions = new SslOption();
            SslProtocol = defaultOptions.Version;
            AcceptablePolicyErrors = defaultOptions.AcceptablePolicyErrors | SslPolicyErrors.RemoteCertificateChainErrors;

            PublisherConfirmation = true;

            ClientProvidedName = HostMetadataCache.Host.ProcessName;
            
            _hostAddress = new Lazy<Uri>(FormatHostAddress);
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public string VirtualHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ushort Heartbeat { get; set; }
        public bool Ssl { get; set; }
        public SslProtocols SslProtocol { get; set; }
        public string SslServerName { get; set; }
        public SslPolicyErrors AcceptablePolicyErrors { get; set; }
        public string ClientCertificatePath { get; set; }
        public string ClientCertificatePassphrase { get; set; }
        public X509Certificate ClientCertificate { get; set; }
        public bool UseClientCertificateAsAuthenticationIdentity { get; set; }
        public LocalCertificateSelectionCallback CertificateSelectionCallback { get; set; }
        public RemoteCertificateValidationCallback CertificateValidationCallback { get; set; }
        public string[] ClusterMembers { get; set; }
        public IRabbitMqEndpointResolver HostNameSelector { get; set; }
        public string ClientProvidedName { get; set; }
        public bool PublisherConfirmation { get; set; }
        public Uri HostAddress => _hostAddress.Value;
        public ushort RequestedChannelMax { get; set; }

        Uri FormatHostAddress()
        {
            var builder = new UriBuilder
            {
                Scheme = "rabbitmq",
                Host = Host,
                Port = Port == 5672 ? 0 : Port,
                Path = string.IsNullOrWhiteSpace(VirtualHost) || VirtualHost == "/"
                    ? "/"
                    : $"/{VirtualHost.Trim('/')}"
            };

            return builder.Uri;
        }

    }
}