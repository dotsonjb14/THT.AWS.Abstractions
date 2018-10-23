using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace THT.AWS.Abstractions.Credentials
{
    public class CrendentialsManager : ICrendentialsManager
    {
        public AWSCredentials GetCredentials(string profile)
        {
            var chain = new CredentialProfileStoreChain();
            if (chain.TryGetAWSCredentials(profile, out AWSCredentials _creds))
            {
                return _creds;
            }
            else
            {
                return null;
            }
        }
    }
}
