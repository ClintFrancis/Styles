using System;
using System.Collections.Generic;

namespace Styles.Text
{
    public abstract class StyleManagerBase : IStyleManager
    {
        protected Dictionary<object, IManagedStyle> _views;
        protected ITextStyle _instance;

        public bool AutoUpdate { get; set; } = true;

        List<CssTag> _customTags;
        public List<CssTag> CustomTags
        {
            get
            {
                return _customTags;
            }
            set
            {
                _customTags = new List<CssTag>(value);
                foreach (var item in _views.Values)
                {
                    item.CustomTags = _customTags;
                    if (AutoUpdate && !item.AutoUpdate) item.UpdateDisplay();
                }
            }
        }

        protected void Init(ITextStyle instance)
        {
            _instance = instance;
            _views = new Dictionary<object, IManagedStyle>();
            _instance.StylesChanged += TextStyle_Instance_StylesChanged;
        }

        public T Create<T>(string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false)
        {
            var tags = customTags ?? CustomTags;
            var target = _instance.Create<T>(styleID, text, tags, useExistingStyles);
            _instance.SetBaseStyle(styleID, ref tags);

            var viewStyle = CreateViewStyle(target, styleID, text, tags, useExistingStyles, enableHtmlEditing);
            viewStyle.AutoUpdate = AutoUpdate;
            _views.Add(target, viewStyle);

            return target;
        }

        public IManagedStyle Add<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool enableHtmlEditing = false)
        {
            var tags = customTags ?? CustomTags;

            // Set the base style for the field
            _instance.SetBaseStyle(styleID, ref tags);

            var viewStyle = CreateViewStyle(target, styleID, text, tags, useExistingStyles, enableHtmlEditing);
            viewStyle.AutoUpdate = AutoUpdate;

            _views.Add(target, viewStyle);

            return viewStyle;
        }

        protected abstract IManagedStyle CreateViewStyle<T>(T target, string styleID, string text, List<CssTag> customTags, bool useExistingStyles = true, bool enableHtmlEditing = false);

        public void UpdateText<T>(T target, string text)
        {
            var viewStyle = _views[target];
            if (viewStyle == null)
                return;

            viewStyle.Text = text;
        }

        /// <summary>
        /// Updates the styling and display of all registered text containers
        /// </summary>
        public void UpdateAll()
        {
            // Update the displays after so they change all at once
            foreach (var item in _views.Values)
            {
                item.SetDirty(true);
            }
        }

        /// <summary>
        /// Updates the frames of any text containers with line heights smaller than the fonts default
        /// </summary>
        public void UpdateFrames()
        {
            // Update the frames of any linespaced itemss
            foreach (var item in _views.Values)
                item.UpdateFrame();
        }

        /// <summary>
        /// Releases all resource used by the <see cref="Occur.TextStyles.iOS.StyleManager"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Occur.TextStyles.iOS.StyleManager"/>. The
        /// <see cref="Dispose"/> method leaves the <see cref="Occur.TextStyles.iOS.StyleManager"/> in an unusable state.
        /// After calling <see cref="Dispose"/>, you must release all references to the
        /// <see cref="Occur.TextStyles.iOS.StyleManager"/> so the garbage collector can reclaim the memory that the
        /// <see cref="Occur.TextStyles.iOS.StyleManager"/> was occupying.</remarks>
        public void Dispose()
        {
            foreach (var item in _views.Values)
                item.Dispose();

            _views.Clear();
            _views = null;
            _customTags = null;

            _instance.StylesChanged -= TextStyle_Instance_StylesChanged;
        }

        void TextStyle_Instance_StylesChanged(object sender, EventArgs e)
        {
            UpdateAll();
        }
    }
}

