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
        /// <param name="region">Region for the request (by default uses global region)</param>
        /// <returns>A byte array of the data</returns>
        Task<byte[]> ReadAsync(string bucketName, string key, string region = null);

        /// <summary>
        /// Writes a file to an S3 bucket (overwriting it if it's already there)
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="region">Region for the request (by default uses global region)</param>
        /// <returns></returns>
        Task WriteAsync(string bucketName, string key, byte[] data, string region = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="region">Region for the request (by default uses global region)</param>
        /// <returns></returns>
        Task DeleteAsync(string bucketName, string key, string region = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="key"></param>
        /// <param name="region">Region for the request (by default uses global region)</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string bucketName, string key, string region = null);
    }
}
