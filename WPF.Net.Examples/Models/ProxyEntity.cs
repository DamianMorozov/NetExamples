// ReSharper disable IdentifierTypo

// ReSharper disable UnusedMember.Global
namespace WPF.Net.Examples.Models
{
    internal class ProxyEntity
    {
        #region Public fields and properties

        public bool UseDefaultCreds { get; set; }
        public string Host { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructor and destructor

        public ProxyEntity()
        {
            UseDefaultCreds = default;
            Host = @"http://somedomain.com:8080";
            Domain = default;
            Username = default;
            Password = default;
        }

        public ProxyEntity(bool useDefaultCreds, string host, string domain, string username, string password)
        {
            Setup(useDefaultCreds, host, domain, username, password);
        }

        #endregion

        #region Public and private methods

        public void Setup(bool useDefaultCreds, string host, string domain, string username, string password)
        {
            Host = host;
            UseDefaultCreds = useDefaultCreds;
            Domain = domain;
            Username = username;
            Password = password;
        }

        #endregion
    }
}
