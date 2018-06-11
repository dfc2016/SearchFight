using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace SearchFight.SearchRunners
{
    public class Configuration
    {
        [XmlArrayItem("SearchRunner")]
        public List<SerializableSearchRunner> SearchRunners { get; set; }

        public static Configuration LoadConfiguration()
        {
            try
            {
                using (var stream = File.OpenRead("Configuration.xml"))
                {
                    try
                    {
                        var serializer = new XmlSerializer(typeof(Configuration));
                        return (Configuration)serializer.Deserialize(stream);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new ConfigurationException("The configuration file is invalid. " + ex.Message, ex);
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new ConfigurationException("Unauthorized exception trying to access cofiguration file.", ex);
            }
            catch (FileNotFoundException ex)
            {
                throw new ConfigurationException("Could not find configuration file.", ex);
            }
            catch (IOException ex)
            {
                throw new ConfigurationException("An error occurred when reading configuration file.", ex);
            }
        }
    }

}
