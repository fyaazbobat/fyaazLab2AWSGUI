using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyaazLab2
{
    //[DynamoDBTable("bookList")]
   public class Book : ViewModel
    {
        public ViewModel mwvm;
        Uri bookUrl;

        public Uri BookUrl
        {
            get
            {
                return bookUrl;
            }

            set
            {
                bookUrl = value;
                RaisePropertyChanged("BookUrl");
            }
        }

        public Book()
        {

        }
    }


}
