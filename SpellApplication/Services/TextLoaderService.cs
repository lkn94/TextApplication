using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellApplication.Services
{
    public class TextLoaderService
    {
        public List<String> LoadText(string space, string variable)
        {
            List<String> keys = new KeysplitterService().SplitKeys(space);
            var result = GetTextFromDatabase(keys, (variable != null && variable != ""));

            if (variable != null && variable != "")
            {
                result[4] = result[4].Replace("VPLACEHOLDER", variable);
            }

            return result;
        }

        public List<String> GetTextFromDatabase(List<string> space, bool variable)
        {
            if (space.Count < 3)
            {
                return null;
            }

            if (space.Count == 3)
            {
                space.Add("de");
            }

            var connection = new MySqlConnection("server=IP;user=USERNAME;password=PASSWORD;database=DATABASE");
            connection.Open();
            MySqlCommand command = null;

            if (variable == false)
            {
                command = new MySqlCommand(String.Format("select text from spells where space='{0}' and gadget='{1}' and state='{2}' and lang='{3}' and variable='0' order by rand() limit 1;", space[0], space[1], space[2], space[3]), connection);
            } else
            {
                command = new MySqlCommand(String.Format("select text from spells where space='{0}' and gadget='{1}' and state='{2}' and lang='{3}' and variable='1' order by rand() limit 1;", space[0], space[1], space[2], space[3]), connection);
            }
            
            var reader = command.ExecuteReaderAsync();
            var result = reader.Result;
            result.Read();

            var value = result.GetValue(0).ToString();

            space.Add(value);

            return space;
        }
    }
}
