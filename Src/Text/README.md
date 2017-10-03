# TextStyle details

TextStyle allows you to effortlessly create, manage and style text in Android and iOS using HTML & CSS style sheets. 

 * Create rich HTML text displays with full CSS styling
 * Style new or existing text displays (UILabel, UITextView, UITextField)
 * Manage and display style changes on existing fields
 * Change text styles at runtime
 * Light-weight and simple to use

---

**Create and style new text displays**
```csharp
// Load the CSS file
var style = File.ReadAllText ("StyleOne.css");
TextStyle.Instance.SetCSS (style);

// Create a heading using the "h1" css style
var headingLabel = TextStyle.Create<UILabel> ("h1", "Behold");
headingLabel.Frame = new CGRect (20f, 40f, 300, 40f);
Add (headingLabel);

// Create a subheading using the "h2" css style
var subheadingLabel = TextStyle.Create<UILabel> ("h2", "The power of TextStyle");
subheadingLabel.Frame = new CGRect (20f, 80f, 300, 40f);
Add (subheadingLabel);
```

**Style and manage existing text displays**
```csharp
// Load the CSS file
var style = File.ReadAllText ("StyleOne.css");
TextStyle.Instance.SetCSS (style);

// Create a StyleManager to handle any CSS changes automatically
var styleManager = new StyleManager ();
styleManager.Add (headingLabel, "h1");
styleManager.Add (subheadingLabel, "h2");
```

**Alter CSS Styles on the fly**
```csharp
// Create a subheading using the "h2" css style with a custom tag
var subtitle = TextStyle.Create<UILabel> ("h2", "This is a <spot>subtitle</spot>", new List<CssTagStyle> () {
	new CssTagStyle ("spot"){ CSS = "spot{color:" + UIColor.Red.ToHex () + "}" }
});
subtitle.Frame = new CGRect (20f, 40f, 30f, 40f);
Add (subtitle);
```