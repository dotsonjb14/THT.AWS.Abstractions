using System.Threading.Tasks;

namespace THT.AWS.Abstractions.S3
{
    public interface IFileWrapper
    {
        /// <summary>
        /// Read the data from a file in S3
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns>A byte array of the data</returns>
        Task<byte[]> ReadAsync(string bucketName, string key);

        /// <summary>
        /// Writes a file to an S3 bucket (overwriting it if it's already there)
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task WriteAsync(string bucketName, string key, byte[] data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task DeleteAsync(string bucketName, string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string bucketName, string key);
    }
}
