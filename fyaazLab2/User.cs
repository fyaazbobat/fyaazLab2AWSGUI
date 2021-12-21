using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fyaazLab2
{
    [DynamoDBTable("userTable1")]
    public class User
    {
        [DynamoDBHashKey]
        public string Email { get; set; }
        public List<string> books;

    }
}
