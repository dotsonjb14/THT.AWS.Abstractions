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
        /// Whether or not to actually validate hashes for S3 (note, only works when actually using S3)
        /// </summary>
        public bool ValidateHashes { get; set; } = true;

        /// <summary>
        /// Local file path for when using the local file store
        /// </summary>
        public string LocalPath { get; set; }
    }
}