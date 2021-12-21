using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System.IO;
using Amazon.Extensions.NETCore.Setup;
using Amazon.DynamoDBv2.Model;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Amazon.DynamoDBv2.DocumentModel;
using System.Configuration;
using System.Windows;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace fyaazLab2
{
    public class DDBOperations
    {

        public AmazonDynamoDBClient client;
       // BasicAWSCredentials credentials;
        public static string tableName = "userTable1";
        Table userTable;
        public  DDBOperations()
        {
            Trace.WriteLine("Creating DB Instance!");
            var builder = new ConfigurationBuilder().
                AddJsonFile("AppSettings.json");

            var jsonData = builder.Build();
            var accessKeyID = jsonData.GetSection("AccesskeyID").Value;
            var secretKey = jsonData.GetSection("Secretaccesskey").Value;

            var credentials = new BasicAWSCredentials(accessKeyID, secretKey); 
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast2);
            labTest();

        }

        async void labTest()
        {
            await CreateTable();
            await AddUsers();
        }

        public async Task CreateTable()
        {
            CreateTableRequest request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>
                {
                    new AttributeDefinition
                    {
                        AttributeName="Email",
                        AttributeType="S"
                    },
                    new AttributeDefinition
                    {
                        AttributeName="Password",
                        AttributeType="S"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName="Email",
                        KeyType="HASH"
                    },
                    new KeySchemaElement
                    {
                        AttributeName="Password",
                        KeyType="RANGE"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 1
                }
            };

            try
            {
                var response = await client.CreateTableAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK) MessageBox.Show("Users table created"); ;
                Trace.WriteLine("Users table created");

            }
            catch (Exception e)
            {
                Trace.WriteLine("Failed to create table: " + e.Message);
            }
        }
        
        public async Task AddUsers()
        {
            try
            {
                userTable = Table.LoadTable(client, tableName, true);
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error adding users to table: " + e.Message);
                return;
            }

            Document user = new Document();
            user["Email"] = "fyaazbobat@hotmail.com";
            user["Password"] = "fyaaz";
            user["books"] = "[[1,https://comp306bucket4lab2.s3.amazonaws.com/AWS+Certified+Solutions+Architect+Study+Guide%2C+2nd+Edition+-+PDF+Room.pdf]]";
            await userTable.PutItemAsync(user);

            user = new Document();
            user["Email"] = "test@test.com";
            user["Password"] = "test";
            user["books"] = "[[1,https://comp306bucket4lab2.s3.amazonaws.com/Docker-Complete-Step-by-Step.pdf]]";
            await userTable.PutItemAsync(user);

            user = new Document();
            user["Email"] = "admin@admin.com";
            user["Password"] = "admin";
            user["books"] = "[[1,https://comp306bucket4lab2.s3.amazonaws.com/Maddie+Stigler+(auth.)+-++Beginning+Serverless+Computing_+Developing+with+Amazon+Web+Services%2C+Microsoft+Azure%2C+and+Google+Cloud-Apress+(2018).pdf]]";
            await userTable.PutItemAsync(user);

            Trace.WriteLine("Users Inserted: 3");
        }
    }
}

