using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.Library.Interfaces
{
    public interface IStringSettings
    {
        string this[string settingName] { get; }
    }
}
