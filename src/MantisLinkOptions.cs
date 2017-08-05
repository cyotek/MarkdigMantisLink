// Copyright (c) 2017 Cyotek Ltd.
// http://www.cyotek.com/blog/writing-custom-markdig-extensions
// Licensed under the MIT License. See LICENSE.txt for the full text.

using System;

namespace MarkdigMantisLink
{
  public class MantisLinkOptions
  {
    #region Fields

    private bool _openInNewWindow;

    private string _url;

    #endregion

    #region Constructors

    public MantisLinkOptions()
    {
      _openInNewWindow = true;
    }

    public MantisLinkOptions(string url)
      : this()
    {
      _url = url;
    }

    public MantisLinkOptions(Uri uri)
      : this()
    {
      if (uri == null)
      {
        throw new ArgumentNullException(nameof(uri));
      }

      _url = uri.OriginalString;
    }

    #endregion

    #region Properties

    public bool OpenInNewWindow
    {
      get { return _openInNewWindow; }
      set { _openInNewWindow = value; }
    }

    public string Url
    {
      get { return _url; }
      set { _url = value; }
    }

    #endregion
  }
}
