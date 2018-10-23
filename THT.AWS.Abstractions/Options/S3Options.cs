namespace THT.AWS.Abstractions.Options
{
    /// <summary>
    /// Options for dealing with S3 storage. Certain options are for actually hitting S3, and some are for localized testing
    /// </summary>
    public class S3Options
    {
        /// <summary>
        /// Aws profile for authenticating with S3
        /// </summary>
        public string AwsProfile { get; set; } = "default";

        /// <summary>
        /// Local file path for when using the local file store
        /// </summary>
        public string LocalPath { get; set; }
    }
}