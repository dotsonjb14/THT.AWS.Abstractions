using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using THT.AWS.Abstractions.Options;

namespace THT.AWS.Abstractions.S3
{
    public class LocalFileWrapper : IFileWrapper
    {
        private readonly S3Options _options;

        public LocalFileWrapper(IOptions<S3Options> options)
        {
            _options = options.Value;
        }

        public Task DeleteAsync(string bucketName, string key)
        {
            return Task.Run(() =>
            {
                var path = GetPath(bucketName, key);
                File.Delete(path);
            });
        }

        public Task<bool> ExistsAsync(string bucketName, string key)
        {
            var path = GetPath(bucketName, key);
            return Task.FromResult(File.Exists(path));
        }

        public Task<byte[]> ReadAsync(string bucketName, string key)
        {
            var path = GetPath(bucketName, key);
            return Task.FromResult(File.ReadAllBytes(path));
        }

        public Task WriteAsync(string bucketName, string key, byte[] data)
        {
            return Task.Run(() =>
            {
                var path = GetPath(bucketName, key);
                File.WriteAllBytes(path, data);
            });
        }

        private string GetPath(string bucketName, string key)
        {
            return Path.Combine(_options.LocalPath, bucketName, key);
        }
    }
}
