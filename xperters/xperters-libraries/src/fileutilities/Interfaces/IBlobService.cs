using xperters.domain;
using xperters.enums;
using xperters.fileutilities.Blob;

namespace xperters.fileutilities.Interfaces
{
    public interface IBlobService
    {
        string AddToBlobForJobDto(Enums.FileFor eUploadFileFor, JobAttachmentDto jobAttachmentDto);
        string AddToBlobForJobBidDto(Enums.FileFor eUploadFileFor, JobBidAttachmentDto jobBidAttachmentDto);
        string AddToBlobForMilestoneDto(Enums.FileFor eUploadFileFor, MilestoneAttachmentDto milestoneAttachmentDto);


        BlobFileResult File(Enums.FileFor eFileFor, string fileName);
        byte[] GetBytesFromBlobStorage(Enums.FileFor eFileFor, string fullPath);
        string GetContainerName(Enums.FileFor eFileFor);
    }
}