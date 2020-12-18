
namespace xperters.fileio
{
    public interface IHandleFiles
    {
        string GetCurrentDirectory();
        bool CheckFilePathExists(string path, string file);
        string GetContentRootPath();
    }
}
