using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using xperters.configurations;
using xperters.configurations.Settings.Email;
using xperters.email.Interface;
using xperters.email.TemplateHelper;
using xperters.models;

namespace xperters.email
{
    public class EmailManager : IManageEmails
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public EmailManager(AppConfig appConfig, ILoggerFactory loggerFactory)
        {
            _emailSettings = appConfig.EmailSettings;
            _logger = loggerFactory.CreateLogger<EmailManager>();
            _loggerFactory = loggerFactory;
        }

        public async Task<Response> SendEmailToFreelancer(TemplateModel templateModel)
        {
            _logger.LogDebug($"Bid accepted by customer notification send to freelancer call function;");
            string template = "Templates.AcceptBidTemplate";
            RazorParser renderer = new RazorParser(typeof(EmailManager).Assembly,_loggerFactory );
            var body = renderer.UsingTemplateFromEmbedded(template, templateModel);
            _logger.LogDebug($"Body of template is:{body}");
            var response =await  SendEmailAsync(templateModel.To, "Bid accepted by customer", body);
            //if (response.StatusCode==System.Net.HttpStatusCode.Accepted)
            //{
            //    _logger.LogDebug($"Bid accepted by customer notification send to freelancer;");
            //}
            //else {
            //    _logger.LogDebug($"Bid accepted by customer notification send to freelancer some exception;");
            //}
            return response;
        }


        public async Task<Response> SendEmailToClient(TemplateModel templateModel)
        {
            _logger.LogDebug($"Bid submit by freelancer notification send to client call function;");
            string template = "Templates.SubmitBidTemplate";
            RazorParser renderer = new RazorParser(typeof(EmailManager).Assembly, _loggerFactory);
            _logger.LogDebug($"RazorParser renderer: {renderer};");
            var body = renderer.UsingTemplateFromEmbedded(template, templateModel);
            _logger.LogDebug($"body of templete: {body};");
      
            var  response= await SendEmailAsync(templateModel.To, "Request to hire freelancer", body);
            //if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            //{
            //    _logger.LogDebug($"Request to hire freelancer notification send to customer;");
            //}
            //else {
            //    _logger.LogDebug($"Request to hire freelancer notification send to customer some exception;");
            //}
            return response;
        }

       public async Task<Response> CompleteMilestoneEmailNotification(TemplateModel templateModel)
        {
            string template = "Templates.CompleteMilestoneTemplate";
            string date = DateTime.Now.ToString();
            RazorParser renderer = new RazorParser(typeof(EmailManager).Assembly, _loggerFactory);
            var body = renderer.UsingTemplateFromEmbedded(template, templateModel);
            var response = await SendEmailAsync(templateModel.To, "Request for Milestone Approval", body);
            //if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            //{
            //    _logger.LogDebug($"Request for Milestone Approval by freelancer notification send to customer;");
            //}
            //else {
            //    _logger.LogDebug($"Request for Milestone Approval by freelancer notification send to customer some exception;");
            //}
            return response;
        }

        private async Task<Response> SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Response response = null;
                var client = new SendGridClient(_emailSettings.ApiKey);
 
                var from = new EmailAddress(_emailSettings.SentFromEmail);
                var to = new EmailAddress(email);
                var plainTextContent = "Bid for your job";
                var htmlContent = message;

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                 response =await client.SendEmailAsync(msg);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

            public async Task<Response> SendEmailToFreeLancerForNegotiation(TemplateModel templateModel)
        {
            string template = "Templates.BidNegotiateTemplate";
            RazorParser renderer = new RazorParser(typeof(EmailManager).Assembly, _loggerFactory);
            var body = renderer.UsingTemplateFromEmbedded(template, templateModel);
            var response =await SendEmailAsync(templateModel.To, "Request for bid amendment", body);
            //if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            //{
            //    _logger.LogDebug($"Request for bid amendment by customer notification send to freelancer;");
            //}
            //else {
            //    _logger.LogDebug($"Request for bid amendment by customer notification send to freelancer some exception;");
            //}
        
            return response;
        }

        public async Task<Response> CompleteJobNotification(TemplateModel templateModel)
        {
            string template = "Templates.CompleteAllMilestonesTemplate";
            string date = DateTime.Now.ToString();
            RazorParser renderer = new RazorParser(typeof(EmailManager).Assembly, _loggerFactory);
            var body = renderer.UsingTemplateFromEmbedded(template, templateModel);
            var response =await SendEmailAsync(templateModel.To, "Job completed by freelancer", body);
            //if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            //{
            //    _logger.LogDebug($"Job completed by freelancer by freelancer notification send to customer;");
            //}
            //else {
            //    _logger.LogDebug($"Job completed by freelancer by freelancer notification send to customer some exception;");
            //}

            return response;
        }
    }
}
   
