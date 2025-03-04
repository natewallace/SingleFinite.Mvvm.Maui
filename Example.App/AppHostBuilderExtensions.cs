using Example.App.Views;
using System.Reflection;
using SingleFinite.Mvvm;

namespace Example.App;

public static class AppHostBuilderExtensions
{
    public static AppHostBuilder AddExampleViews(this AppHostBuilder builder) =>
        builder.AddViews(views => views.Scan(Assembly.GetAssembly(typeof(MainWindow))));
}
