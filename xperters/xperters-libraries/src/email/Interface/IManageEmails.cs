using SendGrid;
using System.Threading.Tasks;
using xperters.models;

namespace xperters.email.Interface
{
  public interface IManageEmails
    {
        Task<Response> SendEmailToClient(TemplateModel templateModel);
        Task<Response> SendEmailToFreelancer(TemplateModel templateModel);
        Task<Response> SendEmailToFreeLancerForNegotiation(TemplateModel templateModel);
        Task<Response> CompleteMilestoneEmailNotification(TemplateModel model);
        Task<Response> CompleteJobNotification(TemplateModel templateModel);

    }
}
