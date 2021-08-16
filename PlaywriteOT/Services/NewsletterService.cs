using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace PlaywriteOT.Services
{
    public class NewsletterService
    {
        public EmailCampaignsApi apiInstance;

        public NewsletterService() {

            Configuration.Default.ApiKey.Add("api-key", "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");
            apiInstance = new EmailCampaignsApi();

        }


        public void createNewCampaign()
        {

            List<long?> exclusionListIds = new List<long?>();
            exclusionListIds.Add(3);
            List<long?> listIds = new List<long?>();
            listIds.Add(2);
            CreateEmailCampaignRecipients recipients = new CreateEmailCampaignRecipients(exclusionListIds,listIds);

            JObject _params = new JObject();
            _params.Add("SUBJECT", "Newsletter " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.UtcNow.Month));

            string attachmentUrl = null;

            try
            {
                var emailCampaigns = new CreateEmailCampaign("newsTag", new CreateEmailCampaignSender("Amy Wilkes", "playwriteot0@gmail.com"),
                    "Newsletter " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.UtcNow.Month),
                   null , null, 6, XmlConvert.ToString(DateTime.UtcNow.AddSeconds(30), XmlDateTimeSerializationMode.Utc)  , "{{params.subject}}", "playwriteot0@gmail.com", null, recipients, attachmentUrl,
                  false, false);

                CreateModel result = apiInstance.CreateEmailCampaign(emailCampaigns);           
                Console.WriteLine(result.ToJson());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }



            
        }


        public bool sendNews() {





            return true;
        }

    }
}
