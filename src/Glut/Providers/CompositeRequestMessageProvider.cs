using Glut.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;


namespace Glut.Providers
{
    public class CompositeRequestMessageProvider : IRequestMessageProvider
    {
        private readonly IRequestMessageProvider[] _messageProviders;

        public CompositeRequestMessageProvider(params IRequestMessageProvider[] messageProviders)
        {
            _messageProviders = messageProviders ?? new IRequestMessageProvider[0];

        }

        public CompositeRequestMessageProvider(IEnumerable<IRequestMessageProvider> messageProviders)
        {
            if (messageProviders == null)
            {
                throw new ArgumentNullException(nameof(messageProviders));
            }
            _messageProviders = messageProviders.ToArray();
        }

        public IEnumerable<HttpRequestMessage> Get()
        {
            foreach(var provider in _messageProviders)
            {
                foreach(var message in provider.Get())
                {
                    yield return message;
                }
            }
        }

        public IEnumerable<IRequestMessageProvider> RequestMessageProviders => _messageProviders;
    }
}

