using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackingApp.Library
{
    public interface IVoiceProvider
    {
        Task<string> ListenAsync();
        string Listen();
        void Speak(string text);
    }
}
