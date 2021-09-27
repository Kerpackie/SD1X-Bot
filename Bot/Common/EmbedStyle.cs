using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Common
{
    public enum EmbedStyle
    {
        /// <summary>
        /// A style to indicate a success.
        /// </summary>
        Success,

        /// <summary>
        /// A style to indicate an error.
        /// </summary>
        Error,

        /// <summary>
        /// A style to indicate an informative state.
        /// </summary>
        Information,

        /// <summary>
        /// A style to indicate that the embed will contain an image.
        /// </summary>
        Image,
    }
}
