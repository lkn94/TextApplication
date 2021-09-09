using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellApplication.Services
{
    public class KeysplitterService
    {
        public List<String> SplitKeys(string combined)
        {
            if (String.IsNullOrEmpty(combined))
            {
                return new List<String>();
            }

            String[] keys = combined.Split(".");

            return keys.ToList();
        }
    }
}
