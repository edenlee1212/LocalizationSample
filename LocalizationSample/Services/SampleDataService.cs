using System.Collections.Generic;

using LocalizationSample.Models;

namespace LocalizationSample.Services
{
    public static class SampleDataService
    {
        /// <summary>
        /// Provides the material color names and values.
        /// See more detailes in https://materialuicolors.co/
        /// </summary>
        public static IEnumerable<SampleColor> AllColors()
        {
            return new List<SampleColor>()
            {
                new SampleColor()
                {
                    Name = "Red",
                    HexCode = "#F44336",
                    Icon = "SampleData/Red.png"
                },
                new SampleColor()
                {
                    Name = "Pink",
                    HexCode = "#E91E63",
                    Icon = "SampleData/Pink.png"
                },
                new SampleColor()
                {
                    Name = "Purple",
                    HexCode = "#9C27B0",
                    Icon = "SampleData/Purple.png"
                },
                new SampleColor()
                {
                    Name = "Indigo",
                    HexCode = "#3F51B5",
                    Icon = "SampleData/Indigo.png"
                }
            };
        }
    }
}
