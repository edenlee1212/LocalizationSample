using LocalizationSample.Services;

namespace LocalizationSample.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Get the localized string.
        /// </summary>
        /// <param name="name">The name of string to be localized</param>
        /// <returns>Localized string</returns>
        public static string Localize(this string name)
        {
            return LocalizationService.Instance.GetResource(name);
        }
    }
}
