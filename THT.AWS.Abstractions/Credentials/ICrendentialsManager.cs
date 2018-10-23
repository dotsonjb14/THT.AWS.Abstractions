using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace THT.AWS.Abstractions.Credentials
{
    public interface ICrendentialsManager
    {
        AWSCredentials GetCredentials(string profile);
    }
}
