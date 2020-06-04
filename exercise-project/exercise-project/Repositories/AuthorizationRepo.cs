using exercise_project.Models;
using exercise_project.Options;
using exercise_project.YamlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace DALexercise_project.Repositories
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly YamlSettings _yamlSettings;
        private readonly DataKeys _dataKeys;
        private readonly AuthSkeleton _authSkeleton;
        private List<Auth> _auths;

        public AuthenticationRepo(YamlSettings yamlSettings, DataKeys dataKeys, AuthSkeleton authSkeleton)
        {
            _yamlSettings = yamlSettings;
            _dataKeys = dataKeys;
            _authSkeleton = authSkeleton;
            _auths = new List<Auth>();

            var data = YamlConfigurationFileParser.Parse(_yamlSettings.YamlDataFile);
            foreach (KeyValuePair<string, YamlSequenceNode> entry in data)
            {
                if (entry.Key.Contains(_dataKeys.AuthKey))
                {
                    foreach (YamlMappingNode node in entry.Value) // Auths
                    {
                        Auth auth = new Auth();
                        foreach (var item in node.Children) // each auth
                        {
                            var key = item.Key.ToString();
                            var value = item.Value;

                            if (key.Equals(_authSkeleton.UserName))
                            {
                                auth.UserName = value.ToString();
                            }
                            else if (key.Equals(_authSkeleton.Password))
                            {
                                auth.Password = value.ToString();
                            }                            
                        }
                        _auths.Add(auth);
                    }

                }
            }
        }

        public async Task<bool> Login(string username, string password)
        {
            return await Task.Run(() => _auths.Any(a => a.UserName.Equals(username) && a.Password.Equals(password)));
        }
    }
}
