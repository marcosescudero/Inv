[assembly: Xamarin.Forms.Dependency(typeof(Inv.Droid.Implementations.PathService))]
namespace Inv.Droid.Implementations
{
    using Interfaces;
    using System;
    using System.IO;

    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Inventory.db3");
        }
    }
}
