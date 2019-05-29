using System.Collections.Generic;
using System.Web.Optimization;

namespace CcRep
{
    public partial class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

        }
    }

    class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}