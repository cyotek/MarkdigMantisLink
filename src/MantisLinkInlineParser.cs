// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using Markdig.Helpers;
using Markdig.Parsers;

namespace MarkdigMantisLink
{
  public class MantisLinkInlineParser : InlineParser
  {
    #region Constants

    private static readonly char[] _openingCharacters =
    {
      '#'
    };

    #endregion

    #region Constructors

    public MantisLinkInlineParser()
    {
      this.OpeningCharacters = _openingCharacters;
    }

    #endregion

    #region Methods

    public override bool Match(InlineProcessor processor, ref StringSlice slice)
    {
      bool matchFound;
      char previous;

      matchFound = false;

      previous = slice.PeekCharExtra(-1);

      if (previous.IsWhiteSpaceOrZero() || previous == '(' || previous == '[')
      {
        char current;
        int start;
        int end;

        slice.NextChar(); // skip the # starting character

        current = slice.CurrentChar;
        start = slice.Start;
        end = start;

        // read as many digits as we can find, incrementing the slice length as we go
        while (current.IsDigit())
        {
          end = slice.Start;
          current = slice.NextChar();
        }

        // once we've ran out of digits, check to see what the next character is
        // to make sure this is a valid issue and nothing something random like #001Alpha
        if (current.IsWhiteSpaceOrZero() || current == ')' || current == ']')
        {
          int inlineStart;

          inlineStart = processor.GetSourcePosition(slice.Start, out int line, out int column); 

          // and if we got here, then we've got a valid reference, so create our AST node
          // and assign it to the processor
          processor.Inline = new MantisLink
                             {
                               Span =
                               {
                                 Start = inlineStart,
                                 End = inlineStart + (end - start) + 1 // add one to the length to account for the # starting character
                               },
                               Line = line,
                               Column = column,
                               IssueNumber = new StringSlice(slice.Text, start, end)
                             };

          matchFound = true;
        }
      }

      return matchFound;
    }

    #endregion
  }
}
