using Microsoft.AspNetCore.Http;

namespace xperters.fileutilities.Interfaces
{
    public interface IAttachmentHandler<T>
    {
        void ConvertFromWebFormToDto(T dto, IFormFileCollection formFiles);
        void StoreAttachmentsToBlob(T dto);       
        
    }
}