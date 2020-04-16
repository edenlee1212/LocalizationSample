using System;
using System.Resources;

using Tizen.Wearable.CircularUI.Forms.Renderer;

using Xamarin.Forms;

// This improves lookup performance for the first resource to load.
// For more details, see https://docs.microsoft.com/dotnet/api/system.resources.neutralresourceslanguageattribute.
[assembly: NeutralResourcesLanguage("en")]

namespace LocalizationSample
{
    class TizenApplication : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            using (var app = new TizenApplication())
            {
                Forms.Init(app);
                FormsCircularUI.Init();
                app.Run(args);
            }
        }
    }
}
