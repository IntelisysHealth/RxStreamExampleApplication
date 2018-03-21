using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using Newtonsoft.Json;
using  RestSharp;
using RxStreamExampleApplication.Dtos;

namespace RxStreamExampleApplication
{
    public partial class Form1 : Form
    {
        //List to capture any errors along the way 
        private List<string> ErrorList;

        public Form1()
        {
            ErrorList = new List<string>();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Prepopulate some default values 
            ResetGuids();
            txtCustom1.Text = "abc";
            txtCustom2.Text = "123";

            txtCoPay1.Text = "";
            txtCoPay2.Text = "";

            txtDoctorZip.Text = "90210";
            txtPatientZip.Text = "77429";

            txtQty1.Text = "30";
            txtQty2.Text = "60";
        }


        private void ResetGuids()
        {
            txtPresNo1.Text = Guid.NewGuid().ToString();
            txtPresNo2.Text = Guid.NewGuid().ToString();
            txtClientId.Text = Guid.NewGuid().ToString();
        }



        //get the closest Estimate detail
        private void button1_Click(object sender, EventArgs e)
        {
            //reset the error list 
            ErrorList = new List<string>();
            
            //reset the grid view 
            dataGridView1.DataSource = "";

            //disable buttons
            button1.Enabled = false;
            btnTest.Enabled = false;

            //make the magic happen
            ClosestEstimate();
            
            //reset the Guids so we don't send duplicate requests 
            ResetGuids();
        }

        //get the estimate
        private void btnTest_Click(object sender, EventArgs e)
        {
            //reset the error list 
            ErrorList = new List<string>();
            
            //reset the grid view 
            dataGridView1.DataSource = "";

            //disable buttons
            button1.Enabled = false;
            btnTest.Enabled = false;
            GetEstimateSummary();

            //reset the Guids so we don't send duplicate requests 
            ResetGuids();
        }

        private async void ClosestEstimate()
        {
            //Get the config values
            txtOutput.Text = "processing...";
            string url = ConfigurationSettings.AppSettings["url"].ToString();
            string tenantId = ConfigurationSettings.AppSettings["tenantId"].ToString();
            string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString();

            //get the methods you are going to call
            var client = new RestClient(url);
            var request = new RestRequest("Prescription/ClosestEstimate", Method.POST);

            //Add the keys to the request header
            request.AddHeader("TenantId", tenantId);
            request.AddHeader("ApiKey", apiKey);
            
            //build the json object to send to RxStream
            var prescription = BuildNewPrescription();

            //Check to see if there were errors building the Json object 
            if (ErrorList.Count > 0)
            {
                txtOutput.Text = string.Format("The ({0}) error(s) occured.\r\n ", ErrorList.Count);
                for (int i = 0; i < ErrorList.Count ; i++)
                {
                    txtOutput.Text = txtOutput.Text + (i + 1).ToString() + " - " + ErrorList[i] + "\r\n";
                }
                
                btnTest.Enabled = true;
                button1.Enabled = true;
                return;
            }

            //Add the Json body
            var json = JsonConvert.SerializeObject(prescription);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            txtOutput.Text = "Sending...";


            //Call RxStream
            var restResponse = await client.ExecuteTaskAsync(request);
            
            //try to make the call and report any errors
            try
            {
                //Deserialize object
                var responseList = JsonConvert.DeserializeObject<List<ClosestEstimateDto>>(restResponse.Content);
                dataGridView1.DataSource = responseList;

                txtOutput.Text = restResponse.Content;
            }
            catch (Exception e)
            {
               // txtOutput.Text = e.ToString(); // this really doesn't add value to the error
                txtOutput.Text = restResponse.Content;
            }
            
            btnTest.Enabled = true;
            button1.Enabled = true;
        }


        private async void GetEstimateSummary()
        {
            //Get the config values
            txtOutput.Text = string.Empty;
            string url = ConfigurationSettings.AppSettings["url"].ToString();
            string tenantId = ConfigurationSettings.AppSettings["tenantId"].ToString();
            string apiKey = ConfigurationSettings.AppSettings["apiKey"].ToString();

            //Add the keys to the request header
            var client = new RestClient(url);
            var request = new RestRequest("Prescription/Estimate", Method.POST);

            //build the json object to send to RxStream
            request.AddHeader("TenantId", tenantId);
            request.AddHeader("ApiKey", apiKey);

            //build the json object to send to RxStream
            var prescription = BuildNewPrescription();

            //Check to see if there were errors building the Json object 
            if (ErrorList.Count > 0)
            {
                txtOutput.Text = string.Format("The ({0}) error(s) occured.\r\n ", ErrorList.Count);
                for (int i = 0; i < ErrorList.Count; i++)
                {
                    txtOutput.Text = txtOutput.Text + (i + 1).ToString() + " - " + ErrorList[i] + "\r\n";
                }

                btnTest.Enabled = true;
                button1.Enabled = true;
                return;
            }

            //Add the Json body
            var json = JsonConvert.SerializeObject(prescription);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            txtOutput.Text = "sending...";

            //get the response from RxStream
            var restResponse = await client.ExecuteTaskAsync(request);
            
            //try to make the call and report any errors
            try
            {
                //Deserialize object
                var responseList = JsonConvert.DeserializeObject<List<EstimateDto>>(restResponse.Content);
                dataGridView1.DataSource = responseList;

                txtOutput.Text = restResponse.Content;
            }
            catch (Exception e)
            {
                // txtOutput.Text = e.ToString(); // this really doesn't add value to the error
                txtOutput.Text = restResponse.Content;
            }
            
            btnTest.Enabled = true;
            button1.Enabled = true;
        }
        
        private PrescriptionDto BuildNewPrescription()
        {
            //put together the header
            var p = new PrescriptionDto
            {
                ClientId = txtClientId.Text,
                CustomInput1 = txtCustom1.Text,
                CustomInput2 = txtCustom2.Text,
                PatZip = txtPatientZip.Text,
                DocZip = txtDoctorZip.Text,
                PharmacyId = txtNpiList.Text
            };


            //add the first line item  w/ required fields 
            PrescriptionItemDto itemOne = new PrescriptionItemDto
            {
                Ndc = txtNdc1.Text,
                PrescriptionNumber =  txtPresNo1.Text,   
            };


            //Valdate additional params for item one
            bool canParse = false;
            int qtyOne;

            canParse = int.TryParse(txtQty1.Text, out qtyOne);
            if (canParse)
            {
                itemOne.Quantity = qtyOne;
            }
            else
            {
                ErrorList.Add("Qty 1 not a valid int");
            }

            decimal coInsOne;
            canParse = decimal.TryParse(txtCoIns1.Text, out coInsOne);
            if (canParse )
            {
                itemOne.CoInsurance = coInsOne;
            }
            else
            {
                if (txtCoIns1.Text.Length > 0)
                    ErrorList.Add("Co Ins 1 not a decimal");
            }

            decimal coAmtOne;
            canParse = decimal.TryParse(txtCoPay1.Text, out coAmtOne);
            if (canParse)
            {
                itemOne.CoPayAmount = coAmtOne;
            }
            else
            {
                if(txtCoPay1.Text.Length > 0)
                    ErrorList.Add("Co Pmt 1 not a decimal");
            }

            int refillsOne;
            canParse = int.TryParse(txtRefills1.Text, out refillsOne);
            if (canParse)
            {
                itemOne.NumberOfRefills = refillsOne;
            }
            else
            {
                if(txtRefills1.Text.Length > 0)
                    ErrorList.Add("Refills 1 not a valid int");
            }

            
            //Add first line item 
            p.LineItems.Add(itemOne);



            //Create the second line item
            PrescriptionItemDto itemTwo =new PrescriptionItemDto
            {
                Ndc = txtNdc2.Text,
                PrescriptionNumber = txtPresNo2.Text
            };

            //Add aditional params to item two 
            int qtyTwo;

            canParse = int.TryParse(txtQty2.Text, out qtyTwo);
            if (canParse)
            {
                itemTwo.Quantity = qtyTwo;
            }
            else
            {
                ErrorList.Add("Qty 2 not a valid int");
            }

            decimal coInsTwo;
            canParse = decimal.TryParse(txtCoIns2.Text, out coInsTwo);
            if (canParse)
            {
                itemTwo.CoInsurance = coInsTwo;
            }
            else
            {
                if(txtCoIns2.Text.Length > 0)
                    ErrorList.Add("Co Ins 2 not a decimal");
            }

            decimal coAmtTwo;
            canParse = decimal.TryParse(txtCoPay2.Text, out coAmtTwo);
            if (canParse)
            {
                itemTwo.CoPayAmount = coAmtTwo;
            }
            else
            {
                if(txtCoPay2.Text.Length > 0)
                    ErrorList.Add("Co Pmt 2 not a decimal");
            }

            int refillsTwo;
            canParse = int.TryParse(txtRefills2.Text, out refillsTwo);
            if (canParse)
            {
                itemTwo.NumberOfRefills = refillsTwo;
            }
            else
            {
                if(txtRefills2.Text.Length > 0)
                    ErrorList.Add("Refills 1 not a valid int");
            }

            //Add Line item two 
            p.LineItems.Add(itemTwo);

            return p;
        }
    }
}
