namespace Utilities.ResourceManagement
{
    public readonly struct ResourcePath
    {
        public readonly string PathToResource;

        public ResourcePath(string pathToResource)
        {
            PathToResource = pathToResource;
        }
    }
}