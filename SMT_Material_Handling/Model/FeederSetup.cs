using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SMT_Material_Handling.Model
{
    class FeederSetup
    {
        public int Position { get; set; }
        public string PartNumber { get; set; }
        public ICommand Command { get; set; }

        public FeederSetup (int position,string partnumber,ICommand command)
        {
            Position = position;
            PartNumber = partnumber;
            Command = command;
        }
    }
}
