namespace Oversteer.Tests.Mock
{
    using CloudinaryDotNet;

    public class CloudinaryMock
    {
        private const string Name = "dpo3vbxnl";
        private const string Api = "713733769727835";
        private const string Key = "AxitLgW4XE_LhDgvwBWGRSsSjv0";

        public static Cloudinary Instance
        {
            get
            {
                var cloudinary = new Account(Name, Api, Key);

                return new Cloudinary(cloudinary);
            }
        }
    }
}
