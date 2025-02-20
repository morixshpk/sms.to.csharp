using sms.to.csharp;
using sms.to.csharp.Models;

namespace sms.to.csharp_Tests
{
    [TestClass]
    public sealed class Tests
    {
        [TestInitialize]
        public void Init()
        {
            Manager.Init(new InitConfigs
            {
                ApiKey = "your_api_key",
                ApiUrl = "http://localhost:8085",
                SenderId = "your_sender_id"
            });
        }

        [TestMethod]
        public void Test_001_SendSingleSMS_ShouldSucceed()
        {
            var response = Manager.SendSMS(new SingleSMSRequest
            {
                To = "+1234567890",
                Message = "Hello, World!"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.MessageId);
            Assert.IsNotNull(response.EstimatedSmsCost);
        }

        [TestMethod]
        public void Test_002_SendSingleSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.SendSMS(new SingleSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_003_SendCampaignSMS_ShouldSucceed()
        {
            var response = Manager.SendCampaignSMS(new CamapignSMSRequest
            {
                To = new List<string> { "+1234567890", "+0987654321" },
                Message = "Hello, World!"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsTrue(!string.IsNullOrEmpty(response.CampaignId), "Campaign Id is null or empty");

        }

        [TestMethod]
        public void Test_004_SendCamaignSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.SendCampaignSMS(new CamapignSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_005_SendPersonalizedSMS_ShouldSucceed()
        {
            var response = Manager.SendPersonalizedSMS(new PersonalizedSMSRequest
            {
                Messages = new List<Messagee>
                {
                    new Messagee
                    {
                        To = "+1234567890",
                        Message = "Hello, World!"
                    },
                    new Messagee
                    {
                        To = "+0987654321",
                        Message = "Hello, World!"
                    }
                }
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.CampaignId);
        }

        [TestMethod]
        public void Test_006_SendPersonalizedSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.SendPersonalizedSMS(new PersonalizedSMSRequest
            {
                Messages = new List<Messagee>
                {
                    new Messagee
                    {
                        Message = "Hello, World!"
                    },
                    new Messagee
                    {
                        To = "+0987654321",
                        Message = "Hello, World!"
                    }
                }
            }));
        }

        [TestMethod]
        public void Test_007_SendFlashSMS_ShouldSucceed()
        {
            var response = Manager.SendFlashSMS(new SingleSMSRequest
            {
                To = "+1234567890",
                Message = "Hello, World!"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.MessageId);
        }

        [TestMethod]
        public void Test_008_SendFlashSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.SendFlashSMS(new SingleSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_009_ScheduleSMS_ShouldSucceed()
        {
            var response = Manager.ScheduleSMS(new ScheduleSMSRequest
            {
                Messages = new List<Messagee>
                {
                     new Messagee
                     {
                         To = "+1234567890",
                         Message = "Hello, World!",
                     }
                }
                ,
                ScheduledFor = DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"),
                Timezone = "UTC"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
            Assert.IsTrue(string.IsNullOrEmpty(response.CampaignId));
        }

        [TestMethod]
        public void Test_010_ScheduleSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.ScheduleSMS(new ScheduleSMSRequest
            {
                Messages = new List<Messagee>
                {
                     new Messagee
                     {
                        Message = "Hello, World!",
                     }
                }
            }));
        }

        [TestMethod]
        public void Test_011_EstimateSingleSMS_ShouldSucceed()
        {
            var response = Manager.EstimateSingleSms(new SingleSMSRequest
            {
                To = "+1234567890",
                Message = "Hello, World!"
            });

            Assert.IsNotNull(response);
            Assert.IsTrue(response.EstimatedCost > 0);
        }

        [TestMethod]
        public void Test_012_EstimateSingleSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.EstimateSingleSms(new SingleSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_013_EstimateCampaignSMS_ShouldSucceed()
        {
            var response = Manager.EstimateCampaignSms(new CamapignSMSRequest
            {
                To = new List<string> { "+1234567890", "+0987654321" },
                Message = "Hello, World!"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.EstimatedCost > 0);
        }

        [TestMethod]
        public void Test_014_EstimateCampaignSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.EstimateCampaignSms(new CamapignSMSRequest
            {
                Message = "Hello, World!"
            }));
        }

        [TestMethod]
        public void Test_015_EstimatePersonalizedSMS_ShouldSucceed()
        {
            var response = Manager.EstimatePersonalizedSms(new PersonalizedSMSRequest
            {
                Messages = new List<Messagee>
                 {
                      new Messagee
                      {
                          To = "+1234567890",
                          Message = "Hello, World!"
                      },
                },
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.EstimatedCost > 0);
        }

        [TestMethod]
        public void Test_016_EstimatePersonalizedSMS_ShouldFail()
        {
            Assert.ThrowsException<Exception>(() => Manager.EstimatePersonalizedSms(new PersonalizedSMSRequest
            {
                Messages = new List<Messagee>
                {
                  new Messagee
                  {
                      To = "+1234567890",
                  },
                },
            }));
        }
    }
}
