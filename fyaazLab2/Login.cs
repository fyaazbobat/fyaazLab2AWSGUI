using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;


namespace fyaazLab2
{
    public class Login : ViewModel, INotifyPropertyChanged
    {
        public MainModel mwvm;
        PdfList blvm;
        public relay LoginTap { get; set; }
        public Login()
        {
            LoginTap = new relay(this.login);
        }


        async void login()
        {
            if (String.IsNullOrEmpty(email) && String.IsNullOrEmpty(email))
                return;

            GetItemRequest request = new GetItemRequest
            {
                TableName = DDBOperations.tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"Email", new AttributeValue { S=Email } },
                    {"Password", new AttributeValue { S=Password} }
                }
            };

            var response = await mwvm.db.client.GetItemAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Item.Count > 0)
                {
                    StatusText = "Logging In...";
                    blvm = new PdfList();
                    blvm.mwvm = mwvm;
                    blvm.email = email;
                    mwvm.viewModel = blvm;
                    blvm.loadBookList();
                }
                else
                {
                    StatusText = "Invalid Credentials";
                }
            }
        }

        string email, password, statusText;
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string StatusText
        {
            get
            {
                return statusText;
            }

            set
            {
                statusText = value;
                RaisePropertyChanged("StatusText");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }


    }

}
