using System;
using System.Collections.Generic;

namespace Styles.Text
{
    public interface IManagedStyle : IDisposable
    {
        string StyleID { get; set; }

        string Text { get; set; }

        List<CssTag> CustomTags { get; set; }

        bool ContainsHtml { get; }

        bool EnableHtmlEditing { get; set; }

        bool IsDirty { get; set; }

        bool AutoUpdate { get; set; }

        void UpdateFrame();

        void UpdateDisplay();
    }
}
