using System.IO;
using Microsoft.AspNetCore.Http;
using xperters.tests.common.Base;

namespace xperters.unit.tests
{
    public abstract class BaseUnitTests: BaseTests
    {

        protected FormFileCollection CreateFormFileCollection()
        {
            var stream = File.OpenRead(FileName);
            var formFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = ContentType,
                ContentDisposition = ContentDisposition
            };

            return new FormFileCollection { formFile };
        }
    }
}
