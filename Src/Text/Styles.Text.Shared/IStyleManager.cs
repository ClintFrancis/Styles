using System;
using System.Collections.Generic;

namespace Styles.Text
{
    public interface IStyleManager : IDisposable
    {
        T Create<T>(string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false);
        IManagedStyle Add<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false);
        void UpdateText<T>(T target, string text);
        void UpdateAll();
        void UpdateFrames();
        bool AutoUpdate { get; set; }
        List<CssTag> CustomTags { get; set; }
    }
}
