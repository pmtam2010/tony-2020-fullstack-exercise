using exercise_project.Models;
using exercise_project.Options;
using exercise_project.YamlHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace exercise_project.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly YamlSettings _yamlSettings;
        private readonly DataKeys _dataKeys;
        private readonly CustomerSkeleton _customerSkeleton;
        private List<Customer> _customers;

        public CustomerRepo(YamlSettings yamlSettings, DataKeys dataKeys, CustomerSkeleton customerSkeleton)
        {
            _customers = new List<Customer>();
            _yamlSettings = yamlSettings;
            _dataKeys = dataKeys;
            _customerSkeleton = customerSkeleton;

            var data = YamlConfigurationFileParser.Parse(_yamlSettings.YamlDataFile);
            foreach (KeyValuePair<string, YamlSequenceNode> entry in data)
            {
                if (entry.Key.Contains(_dataKeys.CustomerKey))
                {
                    foreach (YamlMappingNode node in entry.Value) // customers
                    {
                        Customer customer = new Customer();
                        foreach (var item in node.Children) // each customer
                        {                            
                            var key = item.Key.ToString();
                            var value = item.Value;

                            if (key.Equals(_customerSkeleton.Id))
                            {
                                customer.Id = value.ToString();
                            }
                            else if (key.Equals(_customerSkeleton.Name))
                            {
                                customer.Name = value.ToString();
                            }
                            else if (key.Equals(_customerSkeleton.EmpNum))
                            {
                                customer.EmpNum = value.ToString();
                            }
                            else if (key.Equals(_customerSkeleton.Tags))
                            {
                                var tags = Regex.Replace(value.ToString(), @"[\[\]\s+]", string.Empty).Replace(",", " ");
                                customer.Tags = tags;
                            }
                        }
                        _customers.Add(customer);   
                    }

                }
            }

        }

        public async Task<List<Customer>> GetCustomerData()
        {
            return await Task.Run(() => _customers);
        }
    }
}
