using Amazon.DynamoDBv2.DataModel;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fyaazLab2
{
   public class PdfList : ViewModel
    {
        public MainModel mwvm;
        public string email = "User";
        DynamoDBContext context;
        public RelayCommand PdfCommand { get; set; }

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

        public PdfList()
        {
            PdfCommand = new RelayCommand(this.showPdf);
        }

        void showPdf()
        {
           MessageBox.Show("Openning Book for user: " + email);

            PdfView brv = new PdfView();
            brv.Show();
        }

        public async void loadBookList()
        {
            context = new DynamoDBContext(mwvm.db.client);
            try
            {
                User self = new User { Email = email };
                User um = await context.LoadAsync<User>(self.Email);

                MessageBox.Show("Retreived User Data: " + um);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to fetch user data: " + e.Message);
            }
        }
    }
}

