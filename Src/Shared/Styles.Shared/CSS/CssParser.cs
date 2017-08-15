using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Styles
{
	/// <summary>
	/// Class to hold information for a single CSS declaration.
	/// </summary>
	public class CssParserDeclaration
	{
		string _property;
		public string Property
		{
			get { return _property; }
			set
			{
				IsVariable = ((!string.IsNullOrEmpty(value) && value.StartsWith(CssParser.CSSVariable, StringComparison.Ordinal)));
				_property = value;
			}
		}

		string _value;
		public string Value
		{
			get { return _value; }
			set
			{
				if (!string.IsNullOrEmpty(value) && value.StartsWith("var", StringComparison.Ordinal))
				{
					ReferencesVariable = true;
					_value = value.Replace("var(", "").Replace(")", "");
				}
				else
				{
					_value = value;
				}
			}
		}

		public bool IsVariable { get; private set; }

		public bool ReferencesVariable { get; private set; }
	}

	/// <summary>
	/// Class to hold all the CssParserRules and Variables once parsed
	/// </summary>
	public class CssRuleSet{
		
		public Dictionary<string, string> Variables { get; private set; }

		IEnumerable<CssParserRule> rules;
		public IEnumerable<CssParserRule> Rules { 
			get { return rules;}
			set {
				rules = value;

				// Process all the variables
				Variables = new Dictionary<string, string>();
				foreach (var rule in rules)
				{
					// Store each CSS declaration
					foreach (var declaration in rule.Declarations)
					{
						if (declaration.IsVariable)
						{
							Variables[declaration.Property] = declaration.Value;
						}
					}
				}
			}
		}

		public CssRuleSet(IEnumerable<CssParserRule> rules){
			Rules = rules;
		}
	}

	/// <summary>
	/// Class to hold information for single CSS rule.
	/// </summary>
	public class CssParserRule
	{
		public CssParserRule(string media)
		{
			Selectors = new List<string>();
			Declarations = new List<CssParserDeclaration>();
			Media = media;
		}

		public string Media { get; private set; }

		public IEnumerable<string> Selectors { get; set; }

		public IEnumerable<CssParserDeclaration> Declarations { get; set; }
	}

	/// <summary>
	/// Class to parse CSS text into data structures.
	/// </summary>
	public class CssParser : TextParser
	{
		public const string OpenComment = "/*";
		public const string CloseComment = "*/";
		public const string CSSVariable = "--";

		private string _media;

		public CssParser(string media = null)
		{
			_media = media;
		}

		public IEnumerable<CssParserRule> ParseAll(string css)
		{
			int start;

			Reset(css);
			StripAllComments();

			var rules = new List<CssParserRule>();

			while (!EndOfText)
			{
				MovePastWhitespace();

				if (Peek() == '@')
				{
					// Process "at-rule"
					string atRule = ExtractSkippedText(MoveToWhiteSpace).ToLower();
					if (atRule == "@media")
					{
						start = Position;
						MoveTo('{');
						string newMedia = Extract(start, Position).Trim();

						// Parse contents of media block
						string innerBlock = ExtractSkippedText(() => SkipOverBlock('{', '}'));

						// Trim curly braces
						if (innerBlock.StartsWith("{"))
							innerBlock = innerBlock.Remove(0, 1);
						if (innerBlock.EndsWith("}"))
							innerBlock = innerBlock.Substring(0, innerBlock.Length - 1);

						// Parse CSS in block
						CssParser parser = new CssParser(newMedia);
						rules.AddRange(parser.ParseAll(innerBlock));

						continue;
					}
					else
						throw new NotSupportedException(String.Format("{0} rule is unsupported", atRule));
				}

				// Find start of next declaration block
				start = Position;
				MoveTo('{');
				if (EndOfText) // Done if no more
					break;

				// Parse selectors
				string selectors = Extract(start, Position);
				CssParserRule rule = new CssParserRule(_media);
				rule.Selectors = from s in selectors.Split(',')
								 let s2 = s.Trim()
								 where s2.Length > 0
								 select s2;

				// Parse declarations
				MoveAhead();
				start = Position;
				MoveTo('}');
				string properties = Extract(start, Position);
				rule.Declarations = from s in properties.Split(';')
									let s2 = s.Trim()
									where s2.Length > 0
									let x = s2.IndexOf(':')
									select new CssParserDeclaration
									{
										Property = s2.Substring(0, (x < 0) ? 0 : x).TrimEnd(),
										Value = s2.Substring((x < 0) ? 0 : x + 1).TrimStart()
									};

				// Skip over closing curly brace
				MoveAhead();

				// Add rule to results
				rules.Add(rule);
			}
			// Return rules to caller
			return rules;
		}

		/// <summary>
		/// Removes all comments from the current text.
		/// </summary>
		protected void StripAllComments()
		{
			StringBuilder sb = new StringBuilder();

			Reset();
			while (!EndOfText)
			{
				if (IsComment())
				{
					SkipOverComment();
				}
				else if (IsQuote())
				{
					sb.Append(ExtractSkippedText(SkipOverQuote));
				}
				else
				{
					sb.Append(Peek());
					MoveAhead();
				}
			}
			Reset(sb.ToString());
		}

		/// <summary>
		/// Moves to the next occurrence of the specified character, skipping
		/// over quoted values.
		/// </summary>
		/// <param name="c">Character to find</param>
		public new void MoveTo(char c)
		{
			while (Peek() != c && !EndOfText)
			{
				if (IsQuote())
					SkipOverQuote();
				else
					MoveAhead();
			}
		}

		/// <summary>
		/// Moves to the next whitespace character.
		/// </summary>
		private void MoveToWhiteSpace()
		{
			while (!Char.IsWhiteSpace(Peek()) && !EndOfText)
				MoveAhead();
		}

		/// <summary>
		/// Skips over the quoted text that starts at the current position.
		/// </summary>
		protected void SkipOverQuote()
		{
			Debug.Assert(IsQuote());
			char quote = Peek();
			MoveAhead();
			while (Peek() != quote && !EndOfText)
				MoveAhead();
			MoveAhead();
		}

		/// <summary>
		/// Skips over the comment that starts at the current position.
		/// </summary>
		protected void SkipOverComment()
		{
			Debug.Assert(IsComment());
			MoveAhead(OpenComment.Length);
			MoveTo(CloseComment, true);
			MoveAhead(CloseComment.Length);
		}

		/// <summary>
		/// Skips over a block of text bounded by the specified start and end
		/// character. Blocks may be nested, in which case the endChar of
		/// inner blocks is ignored (the entire outer block is returned).
		/// Sets the current position to just after the final end character.
		/// </summary>
		/// <param name="startChar"></param>
		/// <param name="endChar"></param>
		private void SkipOverBlock(char startChar, char endChar)
		{
			Debug.Assert(Peek() == startChar);
			MoveAhead();
			int depth = 1;
			while (depth > 0 && !EndOfText)
			{
				if (IsQuote())
				{
					SkipOverQuote();
				}
				else
				{
					if (Peek() == startChar)
						depth++;
					else if (Peek() == endChar)
						depth--;
					MoveAhead();
				}
			}
		}

		/// <summary>
		/// Calls the specified action and then returns a string of all characters
		/// that the method skipped over.
		/// </summary>
		/// <param name="a">Action to call</param>
		/// <returns></returns>
		protected string ExtractSkippedText(Action a)
		{
			int start = Position;
			a();
			return Extract(start, Position);
		}

		/// <summary>
		/// Indicates if single or double-quoted text begins at the current
		/// location.
		/// </summary>
		protected bool IsQuote()
		{
			return (Peek() == '\'' || Peek() == '"');
		}

		/// <summary>
		/// Indicates if a comment begins at the current location.
		/// </summary>
		protected bool IsComment()
		{
			return IsEqualTo(OpenComment);
		}

		/// <summary>
		/// Determines if text at the current position matches the specified string.
		/// </summary>
		/// <param name="s">String to compare against current position</param>
		protected bool IsEqualTo(string s)
		{
			Debug.Assert(!String.IsNullOrEmpty(s));
			for (int i = 0; i < s.Length; i++)
			{
				if (Peek(i) != s[i])
					return false;
			}
			return true;
		}
	}

	public class TextParser
	{
		private string _text;
		private int _pos;

		public string Text { get { return _text; } }

		public int Position { get { return _pos; } }

		public int Remaining { get { return _text.Length - _pos; } }

		public static char NullChar = (char)0;

		public TextParser()
		{
			Reset(null);
		}

		public TextParser(string text)
		{
			Reset(text);
		}

		/// <summary>
		/// Resets the current position to the start of the current document
		/// </summary>
		public void Reset()
		{
			_pos = 0;
		}

		/// <summary>
		/// Sets the current document and resets the current position to the start of it
		/// </summary>
		/// <param name="html"></param>
		public void Reset(string text)
		{
			_text = (text != null) ? text : String.Empty;
			_pos = 0;
		}

		/// <summary>
		/// Indicates if the current position is at the end of the current document
		/// </summary>
		public bool EndOfText
		{
			get { return (_pos >= _text.Length); }
		}

		/// <summary>
		/// Returns the character at the current position, or a null character if we're
		/// at the end of the document
		/// </summary>
		/// <returns>The character at the current position</returns>
		public char Peek()
		{
			return Peek(0);
		}

		/// <summary>
		/// Returns the character at the specified number of characters beyond the current
		/// position, or a null character if the specified position is at the end of the
		/// document
		/// </summary>
		/// <param name="ahead">The number of characters beyond the current position</param>
		/// <returns>The character at the specified position</returns>
		public char Peek(int ahead)
		{
			int pos = (_pos + ahead);
			if (pos < _text.Length)
				return _text[pos];
			return NullChar;
		}

		/// <summary>
		/// Extracts a substring from the specified position to the end of the text
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public string Extract(int start)
		{
			return Extract(start, _text.Length);
		}

		/// <summary>
		/// Extracts a substring from the specified range of the current text
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public string Extract(int start, int end)
		{
			return _text.Substring(start, end - start);
		}

		/// <summary>
		/// Moves the current position ahead one character
		/// </summary>
		public void MoveAhead()
		{
			MoveAhead(1);
		}

		/// <summary>
		/// Moves the current position ahead the specified number of characters
		/// </summary>
		/// <param name="ahead">The number of characters to move ahead</param>
		public void MoveAhead(int ahead)
		{
			_pos = Math.Min(_pos + ahead, _text.Length);
		}

		/// <summary>
		/// Moves to the next occurrence of the specified string
		/// </summary>
		/// <param name="s">String to find</param>
		/// <param name="ignoreCase">Indicates if case-insensitive comparisons are used</param>
		public void MoveTo(string s, bool ignoreCase = false)
		{
			_pos = _text.IndexOf(s, _pos, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (_pos < 0)
				_pos = _text.Length;
		}

		/// <summary>
		/// Moves to the next occurrence of the specified character
		/// </summary>
		/// <param name="c">Character to find</param>
		public void MoveTo(char c)
		{
			_pos = _text.IndexOf(c, _pos);
			if (_pos < 0)
				_pos = _text.Length;
		}

		/// <summary>
		/// Moves to the next occurrence of any one of the specified
		/// characters
		/// </summary>
		/// <param name="chars">Array of characters to find</param>
		public void MoveTo(char[] chars)
		{
			_pos = _text.IndexOfAny(chars, _pos);
			if (_pos < 0)
				_pos = _text.Length;
		}

		/// <summary>
		/// Moves to the next occurrence of any character that is not one
		/// of the specified characters
		/// </summary>
		/// <param name="chars">Array of characters to move past</param>
		public void MovePast(char[] chars)
		{
			while (IsInArray(Peek(), chars))
				MoveAhead();
		}

		/// <summary>
		/// Determines if the specified character exists in the specified
		/// character array.
		/// </summary>
		/// <param name="c">Character to find</param>
		/// <param name="chars">Character array to search</param>
		/// <returns></returns>
		protected bool IsInArray(char c, char[] chars)
		{
			foreach (char ch in chars)
			{
				if (c == ch)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Moves the current position to the first character that is part of a newline
		/// </summary>
		public void MoveToEndOfLine()
		{
			char c = Peek();
			while (c != '\r' && c != '\n' && !EndOfText)
			{
				MoveAhead();
				c = Peek();
			}
		}

		/// <summary>
		/// Moves the current position to the next character that is not whitespace
		/// </summary>
		public void MovePastWhitespace()
		{
			while (Char.IsWhiteSpace(Peek()))
				MoveAhead();
		}
	}
}

