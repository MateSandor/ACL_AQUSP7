using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ACL_AQUSP7.DataModels
{
    public class Resource
    {
        public string ResourceName { get; set; }

        public Resource(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}
