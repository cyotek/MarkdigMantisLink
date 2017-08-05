// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using System.Diagnostics;
using Markdig.Helpers;
using Markdig.Syntax.Inlines;

namespace MarkdigMantisLink
{
  [DebuggerDisplay("#{" + nameof(IssueNumber) + "}")]
  public class MantisLink : LeafInline
  {
    #region Fields

    private StringSlice _issueNumber;

    #endregion

    #region Properties

    public StringSlice IssueNumber
    {
      get { return _issueNumber; }
      set { _issueNumber = value; }
    }

    #endregion
  }
}
