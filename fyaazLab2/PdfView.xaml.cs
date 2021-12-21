using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace fyaazLab2
{
    /// <summary>
    /// Interaction logic for PdfView.xaml
    /// </summary>
    /// 
    public partial class PdfView : Window
    {
        
        public string email = "User";
        DynamoDBContext context;
        public PdfView()
        {
            InitializeComponent();
        }

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

        private void RaisePropertyChanged(string v)
        {
            throw new NotImplementedException();
        }
    }
}
