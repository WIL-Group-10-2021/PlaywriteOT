using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using PlaywriteOT.Models;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace PlaywriteOT.Services
{
    public class NewsletterService //SendInBlue Service
    {
        public EmailCampaignsApi apiInstance;

        public NewsletterService()
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            { 
                Configuration.Default.ApiKey.Add("api-key", "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");
            }
           
            apiInstance = new EmailCampaignsApi();
        }

        public GetEmailCampaigns DashboardInfo()
        {
            string type = "classic";
            string status = "sent";
            string startDate = "2020-08-22T09:43:51.970+05:30";
            string endDate = "2020-12-24T16:03:51.000+05:30";
            long? offset = 0;

            try
            {
                GetEmailCampaigns result = apiInstance.GetEmailCampaigns(type, status, null, null, 1000, offset);
                Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
                return result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                return null;

            }
        }

        public bool CreateSmtpEmail(string name, string email, string phonNum, string mssg)
        {
            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender Email = new SendSmtpEmailSender("PlaywriteOT", "playwriteot0@gmail.com");                                                   //Sender Details
            
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(email ,name);                           //Receiver Details
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            SendSmtpEmailCc CcData = new SendSmtpEmailCc(email, name);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            Cc.Add(CcData);

            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo("playwriteot0@gmail.com", "Amy");

            long? TemplateId = 15;
    
            JObject Params = new()
            {
                { "name", name },
                { "message", mssg },
                { "pN", phonNum}
            };

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, Cc, null, null, null, ReplyTo, null, null
                    ,TemplateId, Params, null, null);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                var reu = result.ToJson();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CreateNewCampaign(string headingText, string bodyText, string attachmentUrl)
        {
            // headingText = "Test for Attachments";
            // bodyText = "Test body where is the attc";
            // attachmentUrl = "https://res.cloudinary.com/playwriteot/image/upload/v1630614158/TestPDF_oxpqwt.pdf";

            if (string.IsNullOrEmpty(headingText))
                return false;
            if (string.IsNullOrEmpty(bodyText))
                return false;
            if (string.IsNullOrEmpty(attachmentUrl))
                return false;

            List<long?> exclusionListIds = new List<long?>();
            exclusionListIds.Add(3);
            List<long?> listIds = new List<long?>();
            listIds.Add(2);
            CreateEmailCampaignRecipients recipients = new CreateEmailCampaignRecipients(exclusionListIds, listIds);

            JObject _params = new()
            {
                { "SUBJECT", "Newsletter " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.UtcNow.Month) },
                { "bodytext", bodyText },
                { "headingText", headingText }
            };

            try
            {
                var emailCampaigns = new CreateEmailCampaign("newsTag", 
                                    new CreateEmailCampaignSender("PlaywriteOT", "playwriteot0@gmail.com"),
                                    "Newsletter " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.UtcNow.Month),
                                    null, null, 6, XmlConvert.ToString(DateTime.UtcNow.AddSeconds(30), XmlDateTimeSerializationMode.Utc),
                                    "Newsletter " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.UtcNow.Month), 
                                    "playwriteot0@gmail.com",
                                    null, recipients, attachmentUrl, false, false, null, null, null, _params);

                // var emailCampaigns = new CreateEmailCampaign(tag, sender, CampaignName, htmlContent, htmlUrl, templateId, scheduledAt,
                // subject, replyTo, toField, recipients, attachmentUrl, inlineImageActivation, mirrorActive, footer, header, utmCampaign, _params);

                CreateModel result = apiInstance.CreateEmailCampaign(emailCampaigns);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            /*          string tag = "myTag";
                        string sender_Name = "John Doe";
                        string sender_Email = "playwriteot0@gmail.com";
                        CreateEmailCampaignSender sender = new CreateEmailCampaignSender(sender_Name, sender_Email);
                        string name = "My First Campaign";
                        string htmlContent = null;
                        string htmlUrl = null;
                        long? templateId = 1;
                        string scheduledAt = "2021-12-24T16:03:51.000+05:30";
                        string subject = "My {{params.subject}}";
                        string replyTo = "replyto@domain.com";
                        string toField = "{{contact.FIRSTNAME}} {{contact.LASTNAME}}";
                        List<long?> exclusionListIds = new List<long?>();
                        exclusionListIds.Add(3);
                        List<long?> listIds = new List<long?>();
                        listIds.Add(2);
                        CreateEmailCampaignRecipients recipients = new CreateEmailCampaignRecipients(exclusionListIds, listIds);
                        string attachmentUrl = "https://attachment.domain.com/myAttachmentFromUrl.jpg";
                        bool? inlineImageActivation = false;
                        bool? mirrorActive = false;
                        string footer = "If you wish to unsubscribe from our newsletter, click {here}";
                        string header = "If you are not able to see this mail, click {here}";
                        string utmCampaign = "My utm campaign value";
                        JObject _params = new JObject();
                        _params.Add("PARAMETER", "My param value");
                        _params.Add("ADDRESS", "Seattle, WA");
                        _params.Add("SUBJECT", "New Subject");
                        try
                        {
                            var emailCampaigns = new CreateEmailCampaign(tag, sender, name, htmlContent, htmlUrl, templateId, scheduledAt, subject, replyTo, toField, recipients, attachmentUrl, inlineImageActivation, mirrorActive, footer, header, utmCampaign, _params);
                            CreateModel result = apiInstance.CreateEmailCampaign(emailCampaigns);

                            Console.WriteLine(result.ToJson());
                            return true;
                            // Console.ReadLine();
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                            return false;
                            //Console.ReadLine();
                        }
            */

        }

        public bool UploadNewsletter()
        {
            return true;
        }
    }
}