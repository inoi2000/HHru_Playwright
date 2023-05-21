using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeadHunterBrowserClient
{
    public class HiddenPassword
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _text;

        public HiddenPassword(string text)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Get() => _text;
    }
}
