using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using THT.AWS.Abstractions.Options;

namespace THT.AWS.Abstractions.S3
{
    public class S3FileWrapper : IFileWrapper
    {
        private readonly S3Options _options;

        public S3FileWrapper(IOptions<S3Options> options)
        {
            _options = options.Value;
        }

        private AWSCredentials creds
        {
            get
            {
                var chain = new CredentialProfileStoreChain();
                AWSCredentials _creds;
                if (chain.TryGetAWSCredentials(_options.AwsProfile, out _creds))
                {
                    return _creds;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task DeleteAsync(string bucketName, string key)
        {
            using (var s3 = new AmazonS3Client(creds, Amazon.RegionEndpoint.USWest2))
            {
                await s3.DeleteObjectAsync(bucketName, key);
            }
        }

        public async Task<bool> ExistsAsync(string bucketName, string key)
        {
            using (var s3 = new AmazonS3Client(creds, Amazon.RegionEndpoint.USWest2))
            {
                try
                {
                    using (var obj = await s3.GetObjectAsync(bucketName, key)) {
                        return true;
                    }
                }
                catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchKey")
                {
                    return false;
                }
            }
        }

        public async Task<byte[]> ReadAsync(string bucketName, string key)
        {
            using (var s3 = new AmazonS3Client(creds, Amazon.RegionEndpoint.USWest2))
            using (var obj = await s3.GetObjectAsync(bucketName, key))
            using (var stream = obj.ResponseStream)
            using (var mem = new MemoryStream())
            {
                stream.CopyTo(mem);
                var retval = mem.ToArray();

                if(obj.Metadata.Keys.Contains("hash"))
                {
                    using (var md5 = MD5.Create())
                    {
                        var hashBytes = md5.ComputeHash(retval);
                        var hash = Convert.ToBase64String(hashBytes);

                        if(hash != obj.Metadata["hash"])
                        {
                            throw new Exception("Read failed hash check");
                        }
                    }
                }

                return retval;
            }
        }

        public async Task WriteAsync(string bucketName, string key, byte[] data)
        {
            var hash = "";

            using (var md5 = MD5.Create())
            {
                var hashBytes = md5.ComputeHash(data);
                hash = Convert.ToBase64String(hashBytes);
            }

            using (var s3 = new AmazonS3Client(creds, Amazon.RegionEndpoint.USWest2))
            {
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = key,
                    InputStream = new MemoryStream(data),
                    MD5Digest = hash
                };

                request.Metadata.Add("hash", hash);

                await s3.PutObjectAsync(request);
            }
        }
    }
}
