namespace CcRep.Helpers.BreadCrumbs
{
    public static class CrumbsProvider
    {
        public static Crumbs ForDirectories()
        {
            return new Crumbs {
                new Crumb {Name = "Администрирование" },
                new Crumb {Name = "Справочники" },
            };
        }
    }
}