using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.Library
{
    public interface IVoiceProvider
    {
        string Listen();
        void Speak(string text);
    }
}
