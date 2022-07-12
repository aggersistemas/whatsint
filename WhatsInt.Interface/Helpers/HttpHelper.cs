namespace WhatsInt.Interface.Helpers
{
    public static class HttpHelper
    {
        public static Uri ToUri(this string uri)
        {
            return new Uri(uri);
        }
    }
}
