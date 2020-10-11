// ReSharper disable IdentifierTypo

// ReSharper disable UnusedMember.Global
namespace WPF.Net.Examples.Models
{
    public class Proxy
    {
        #region Public fields and properties

        public bool UseDefaultCreds { get; set; }
        public string Host { get; set; }
        public string Domain { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructor and destructor

        public Proxy()
        {
            UseDefaultCreds = default;
            Host = @"http://somedomain.com:8080";
            Domain = default;
            Username = default;
            Password = default;
        }

        public Proxy(bool useDefaultCreds, string host, string domain, string username, string password)
        {
            Setup(useDefaultCreds, host, domain, username, password);
        }

        #endregion

        #region Public methods

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
