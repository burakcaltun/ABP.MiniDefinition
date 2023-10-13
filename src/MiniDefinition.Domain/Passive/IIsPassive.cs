using MiniDefinition.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDefinition.Passive
{
    public interface IIsPassive
    {
        public YesOrNoEnum? IsPassive { get; set; }
    }
}
