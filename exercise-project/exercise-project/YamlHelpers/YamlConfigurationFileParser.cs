using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using exercise_project.Models;
using Microsoft.Extensions.Configuration;
using YamlDotNet.RepresentationModel;

namespace exercise_project.YamlHelpers
{
    public class YamlConfigurationFileParser
    {
        private readonly static IDictionary<string, YamlSequenceNode> _data = new SortedDictionary<string, YamlSequenceNode>(StringComparer.OrdinalIgnoreCase);
        public static IDictionary<string, YamlSequenceNode> Parse(string fileName)
        {
            _data.Clear();

            // https://dotnetfiddle.net/rrR2Bb
            var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            using (var reader = new StreamReader(path))
            {
                var yaml = new YamlStream();
                yaml.Load(reader);

                if (yaml.Documents.Any())
                {
                    var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
                    foreach (var entry in mapping.Children)
                    {
                        var value = ((YamlScalarNode)entry.Key).Value;
                        var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode(value)];
                        if (!_data.ContainsKey(value))
                        {
                            _data.Add(value, items);
                        }                            
                    }
                }
            }
            return _data;
        }        
    }

}
