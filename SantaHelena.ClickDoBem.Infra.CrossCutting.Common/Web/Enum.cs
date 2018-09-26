using System;
using System.ComponentModel;

namespace SantaHelena.ClickDoBem.Infra.CrossCutting.Common.Web
{

    /// <summary>
    /// Enumeração de Verbos Http
    /// </summary>
    [Flags]
    public enum HttpVerbs
    {
        [Description("GET")]
        Get = 1,
        [Description("POST")]
        Post = 2,
        [Description("PUT")]
        Put = 4,
        [Description("DELETE")]
        Delete = 8,
        [Description("HEAD")]
        Head = 16,
        [Description("PATCH")]
        Patch = 32,
        [Description("OPTIONS")]
        Options = 64
    }

}
